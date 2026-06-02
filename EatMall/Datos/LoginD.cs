
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
							RU.IdRol, 
							M.Ruta AS RutaInicio 
						FROM Usuario U
						INNER JOIN RolUsuario RU ON RU.IdUsuario = U.Id
						INNER JOIN MenuRol MR ON RU.IdRol = MR.IdRol 
						INNER JOIN Menu M ON MR.IdMenu = M.Id       
						WHERE U.Email = @Email AND U.Contraseña = @Clave  

						AND (
							(@EsFunc = 1 AND RU.IdRol BETWEEN 1 AND 4) OR 
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
								UrlInicio = dr["RutaInicio"].ToString()
							};
						}
					}
				}
			}
			return oUsuario;
		}
	}
}