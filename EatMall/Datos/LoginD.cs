
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using EatMall.Logica;
using EatMall.Modelo;

namespace EatMall.Datos
{
	public class LoginD
	{
		public UsuarioLogin MtLogin(UsuarioLogin oDatosSesion, bool esFuncionario)
		{
			UsuarioLogin oUsuario = null;
			using (SqlConnection cn = ConexionDB.MtAbrirConexion())
			{
				cn.Open();

				string consulta = @"
						SELECT TOP 1 
							U.Id, 
							U.Nombre, 
							U.Email, 
							U.Estado,
							RU.IdRol, 
							M.Ruta AS RutaInicio 
						FROM Usuario U
						INNER JOIN RolUsuario RU ON RU.IdUsuario = U.Id
						INNER JOIN MenuRol MR ON RU.IdRol = MR.IdRol 
						INNER JOIN Menu M ON MR.IdMenu = M.Id       
						WHERE U.Email = @Email AND U.Contraseña = @Clave  
						AND (
							(@EsFunc = 1 AND RU.IdRol BETWEEN 1 AND 4) 
							OR 
							(@EsFunc = 0 AND RU.IdRol = 5)
						)
						ORDER BY RU.IdRol ASC";

				using (SqlCommand cmd = new SqlCommand(consulta, cn))
				{
					cmd.Parameters.AddWithValue("@Email", oDatosSesion.Email);
					cmd.Parameters.AddWithValue("@Clave", oDatosSesion.Contraseña);
					cmd.Parameters.AddWithValue("@EsFunc", esFuncionario ? 1 : 0);

					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						if (dr.Read())
						{
							oUsuario = new UsuarioLogin()
							{
								Id = Convert.ToInt32(dr["Id"]),
								Nombre = dr["Nombre"].ToString(),
								IdRol = Convert.ToInt32(dr["IdRol"]),
								UrlInicio = dr["RutaInicio"].ToString(),
								Estado = Convert.ToBoolean(dr["Estado"])
                            };
						}
					}
                    // Si no encontró en Usuario Y es modo administrador
                    // busca en CajeroLocal
                    if (oUsuario == null && esFuncionario)
                    {
                        string consultaCajero = @"
    SELECT CL.Id, CL.Gmail, CL.Estado, CL.IdLocal, L.Nombre AS NombreLocal
    FROM CajeroLocal CL
    INNER JOIN Local L ON CL.IdLocal = L.Id
    WHERE CL.Gmail = @Email AND CL.Contraseña = @Clave AND CL.Estado = 1";

                        using (SqlCommand cmdCajero = new SqlCommand(consultaCajero, cn))
                        {
                            cmdCajero.Parameters.AddWithValue("@Email", oDatosSesion.Email);
                            cmdCajero.Parameters.AddWithValue("@Clave", oDatosSesion.Contraseña);

                            using (SqlDataReader drCajero = cmdCajero.ExecuteReader())
                            {
                                if (drCajero.Read())
                                {
                                    // Guardamos el cajero en Session aparte
                                    HttpContext.Current.Session["Cajero"] = new Cajero
                                    {
                                        Id = Convert.ToInt32(drCajero["Id"]),
                                        Gmail = drCajero["Gmail"].ToString(),
                                        Estado = Convert.ToBoolean(drCajero["Estado"]),
                                        IdLocal = Convert.ToInt32(drCajero["IdLocal"]),
                                        NombreLocal = drCajero["NombreLocal"].ToString()
                                    };


                                    oUsuario = new UsuarioLogin()
                                    {
                                        Id = Convert.ToInt32(drCajero["Id"]),
                                        Nombre = drCajero["Gmail"].ToString(),
                                        IdRol = 0, // 0 = cajero, no exis
                                        UrlInicio = "~/Vista/Cajero/Panel.aspx",
                                        Estado = Convert.ToBoolean(drCajero["Estado"])
                                    };
                                }
                            }
                        }
                    }
                }
                return oUsuario;
            }
			}
			
		}
	}
