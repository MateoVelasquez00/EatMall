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
                            M.Ruta AS RutaInicio,
                            ISNULL(CC.Id,0) AS IdCC,
                            ISNULL(L.Id,0) AS IdLocal
                        FROM Usuario U
                        INNER JOIN RolUsuario RU
                            ON RU.IdUsuario = U.Id
                        INNER JOIN MenuRol MR
                            ON RU.IdRol = MR.IdRol
                        INNER JOIN Menu M
                            ON MR.IdMenu = M.Id
                        LEFT JOIN CentroComercial CC
                            ON CC.IdAdminCC = U.Id
                        LEFT JOIN Local L
                            ON L.IdDueñoLocal = U.Id
                        WHERE U.Email=@Email
                        AND U.Contraseña=@Clave
                        AND (
                        (@EsFunc=1 AND RU.IdRol IN (1,2))
                        OR
                        (@EsFunc=0 AND RU.IdRol IN (3,4,5,6))
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
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                IdCC = Convert.ToInt32(dr["IdCC"]),
                                IdLocal = Convert.ToInt32(dr["IdLocal"])
                            };
                        }
                    }
                }
            }
            return oUsuario;
        }
    }
}