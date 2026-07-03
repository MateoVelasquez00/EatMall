using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
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
                    L.IdPlazoleta,
                    L.IdDueñoLocal,
                    L.NumeroLocal,
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
                    L.IdPlazoleta,
                    L.IdDueñoLocal,
                    L.NumeroLocal,
                    HL.Dia,
                    HL.HorarioApertura,
                    HL.HorarioCierre";

                SqlCommand cmd = new SqlCommand(consulta, cn);
                cmd.Parameters.AddWithValue("@IdLocal", idLocal);
                SqlDataReader dr = cmd.ExecuteReader();

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
                            IdPlazoleta = Convert.ToInt32(dr["IdPlazoleta"]),
                            IdDueñoLocal = Convert.ToInt32(dr["IdDueñoLocal"]),
                            NumeroLocal = Convert.ToInt32(dr["NumeroLocal"]),
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
                    local.HorarioLocal = horarios.Count > 0 ? string.Join("<br/>", horarios) : "Horario no disponible";
            }

            return local;
        }
        public DataTable MtListarTodosLocales(int idCC)
        {
            DataTable dt = new DataTable();

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
                            l.NumeroLocal,

                            Pl.Nombre AS NombrePlazoleta,
                            CC.Nombre AS NombreCentroComercial,
                            DL.Nombre AS NombreDueño

                        FROM dbo.Local l
                        INNER JOIN Plazoleta Pl
                            ON Pl.Id = l.IdPlazoleta
                        INNER JOIN CentroComercial CC
                            ON CC.Id = Pl.IdCentroComercial
                        INNER JOIN Usuario DL
                            ON DL.Id = l.IdDueñoLocal

                        WHERE CC.Id = @IdCC";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@IdCC", idCC);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void MtCambiarEstadoLocal(int idLocal, string nuevoEstado)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string query = "UPDATE dbo.Local SET Estado = @Estado WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@Id", idLocal);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void MtCrearLocal(Local local)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = @"INSERT INTO dbo.Local 
                        (Nombre, Descripcion, Telefono, Email, Imagen, Estado, IdPlazoleta, IdDueñoLocal, NumeroLocal)
                        VALUES 
                        (@Nombre, @Descripcion, @Telefono, @Email, @Imagen, @Estado, @IdPlazoleta, @IdDueñoLocal, @NumeroLocal)";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", local.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", local.Descripcion);
                    cmd.Parameters.AddWithValue("@Telefono", local.Telefono);
                    cmd.Parameters.AddWithValue("@Email", local.Email);
                    cmd.Parameters.AddWithValue("@Imagen", local.Imagen);
                    cmd.Parameters.AddWithValue("@Estado", local.Estado);
                    cmd.Parameters.AddWithValue("@IdPlazoleta", local.IdPlazoleta);
                    cmd.Parameters.AddWithValue("@IdDueñoLocal", local.IdDueñoLocal);
                    cmd.Parameters.AddWithValue("@NumeroLocal", local.NumeroLocal);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public bool MtActualizarLocal(Local local)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = @"UPDATE dbo.Local SET
                        Nombre = @Nombre,
                        Descripcion = @Descripcion,
                        Telefono = @Telefono,
                        Email = @Email,
                        Imagen = @Imagen,
                        Estado = @Estado,
                        IdPlazoleta = @IdPlazoleta,
                        IdDueñoLocal = @IdDueñoLocal,
                        NumeroLocal = @NumeroLocal
                        WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", local.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", local.Descripcion);
                    cmd.Parameters.AddWithValue("@Telefono", local.Telefono);
                    cmd.Parameters.AddWithValue("@Email", local.Email);
                    cmd.Parameters.AddWithValue("@Imagen", local.Imagen);
                    cmd.Parameters.AddWithValue("@Estado", local.Estado);
                    cmd.Parameters.AddWithValue("@IdPlazoleta", local.IdPlazoleta);
                    cmd.Parameters.AddWithValue("@IdDueñoLocal", local.IdDueñoLocal);
                    cmd.Parameters.AddWithValue("@NumeroLocal", local.NumeroLocal);
                    cmd.Parameters.AddWithValue("@Id", local.Id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}