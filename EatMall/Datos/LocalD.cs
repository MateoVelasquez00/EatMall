using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EatMall.Datos
{
    public class LocalD
    {
        public List<Local> MtListarLocales(int IdPlazoleta)
        {
            List<Local> listaL = new List<Local>();

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string query = @"SELECT 
                                l.Id, 
                                l.Nombre, 
                                l.Descripcion, 
                                l.Telefono, 
                                l.Email, 
                                l.Imagen, 
                                l.Estado, 
                                p.Nombre AS NombrePlazoleta,
                                ISNULL(
                                    (SELECT AVG(CAST(c.Puntaje AS FLOAT)) 
                                     FROM dbo.Calificacion c 
                                     WHERE c.IdLocal = l.Id), 
                                0) AS Promedio
                                
                                FROM dbo.Local l
                                INNER JOIN dbo.Plazoleta p 
                                    ON l.IdPlazoleta = p.Id
                                WHERE l.IdPlazoleta = @IdPlazoleta";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@IdPlazoleta", IdPlazoleta);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaL.Add(new Local()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Email = dr["Email"].ToString(),
                                Imagen = dr["Imagen"].ToString(),
                                Estado = dr["Estado"].ToString(),
                                Promedio = Convert.ToDouble(dr["Promedio"]),

                                Plazoleta = new Plazoleta()
                                {
                                    Nombre = dr["NombrePlazoleta"].ToString()
                                }
                            });
                        }
                    }
                }
            }

            return listaL;
        }

        public Local ObtenerLocalPorId(int idLocal)
        {
            Local local = null;

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string consulta = @"SELECT 
                            L.Id,
                            L.Imagen,
                            L.Nombre,
                            CAST(L.Descripcion AS NVARCHAR(MAX)) AS Descripcion,
                            CAST(L.Telefono AS NVARCHAR(50)) AS Telefono,
                            L.Email,
                            L.Estado,
                            HL.Dia,
                            HL.HorarioApertura,
                            HL.HorarioCierre,
                            ISNULL(AVG(CAST(C.Puntaje AS FLOAT)), 0) AS PromedioCalificacionLocal
                            FROM Local L
                            LEFT JOIN HorarioLocal HL ON HL.IdLocal = L.Id
                            LEFT JOIN Calificacion C ON C.IdLocal = L.Id
                            WHERE L.Id = @IdLocal
                            GROUP BY 
                            L.Id,
                            L.Imagen,
                            L.Nombre,
                            CAST(L.Descripcion AS NVARCHAR(MAX)),
                            CAST(L.Telefono AS NVARCHAR(50)),
                            L.Email,
                            L.Estado,
                            HL.Dia,
                            HL.HorarioApertura,
                            HL.HorarioCierre";

                SqlCommand cmd = new SqlCommand(consulta, cn);
                cmd.Parameters.AddWithValue("@IdLocal", idLocal);
                SqlDataReader dr = cmd.ExecuteReader();

                //Es una lista de strings vacía para ir guardando los horarios del local
                List<string> horarios = new List<string>();

                while (dr.Read())
                {
                    if (local == null)
                    {
                        local = new Local()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            Imagen = dr["Imagen"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Email = dr["Email"].ToString(),
                            Estado = dr["Estado"].ToString(),
                            Calificacion = new Calificacion()
                            {
                                Puntaje = Convert.ToDecimal(dr["PromedioCalificacionLocal"])
                            }
                        };
                    }

                    if (dr["Dia"] != DBNull.Value)
                    {
                        horarios.Add(
                            dr["Dia"].ToString() + ": " +
                            dr["HorarioApertura"].ToString() + " - " +
                            dr["HorarioCierre"].ToString()
                        );
                    }
                }

                if (local != null)
                    //"si hay horarios únalos en un string, si no muestra 'Horario no disponible
                    local.HorarioLocal = horarios.Count > 0 ? string.Join("<br/>", horarios) : "Horario no disponible";
            }

            return local;
        }
    }
}