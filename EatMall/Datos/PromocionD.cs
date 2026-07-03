using EatMall.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace EatMall.Datos
{
    public class PromocionD
    {
        public List<Promocion> MtListarPromocionesPorPlazoleta(int idPlazoleta)
        {
            List<Promocion> lista = new List<Promocion>();
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = "SELECT p.Id, p.Nombre, p.Imagen, p.Total, p.IdLocal, l.IdPlazoleta, p.Estado " +
                        "FROM dbo.Promocion p " +
                        "INNER JOIN dbo.Local l ON p.IdLocal = l.Id " +
                        "WHERE l.IdPlazoleta = @idPlazoleta AND p.Estado = 1";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@idPlazoleta", idPlazoleta);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Promocion()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Imagen = dr["Imagen"].ToString(),
                                Total = Convert.ToDecimal(dr["Total"]),
                                IdLocal = Convert.ToInt32(dr["IdLocal"]),
                                IdPlazoleta = Convert.ToInt32(dr["IdPlazoleta"]),
                                Estado = Convert.ToInt32(dr["Estado"]) == 1

                            });
                        }
                    }
                }
            }
            return lista;
        }
        public List<Promocion> MtListarPromocionesPorLocal(int idLocal)
        {
            List<Promocion> lista = new List<Promocion>();
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = "SELECT Id, Nombre, Imagen, Total, IdLocal, Estado FROM dbo.Promocion WHERE IdLocal = @idLocal";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@idLocal", idLocal);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Promocion()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Imagen = dr["Imagen"].ToString(),
                                Total = Convert.ToDecimal(dr["Total"]),
                                IdLocal = Convert.ToInt32(dr["IdLocal"]),
                                Estado = Convert.ToInt32(dr["Estado"]) == 1
                            });
                        }
                    }
                }
            }
            return lista;
        }
        public bool MtCambiarEstadoPromocion(int idPromocion)
        {
            bool actualizado = false;
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = "UPDATE dbo.Promocion SET Estado = CASE WHEN Estado = 1 THEN 0 ELSE 1 END WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", idPromocion);
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    actualizado = filasAfectadas > 0;
                }
            }
            return actualizado;
        }
        public bool MtRegistrarPromocionCompleta(string nombre, string imagen, DateTime inicio, DateTime fin, decimal total, int idLocal, List<Producto> productos)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                SqlTransaction transaccion = cn.BeginTransaction();

                try
                {
                    // 1. Insertar Cabecera de la Promoción
                    string queryPromo = @"INSERT INTO dbo.Promocion (Nombre, Imagen, FechaInicio, FechaFinalizacion, Total, IdLocal, Estado) 
                          VALUES (@Nombre, @Imagen, @Inicio, @Fin, @Total, @IdLocal, 1);
                          SELECT SCOPE_IDENTITY();";

                    int idPromocionGenerado = 0;

                    using (SqlCommand cmd = new SqlCommand(queryPromo, cn, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Imagen", imagen);
                        cmd.Parameters.AddWithValue("@Inicio", inicio);
                        cmd.Parameters.AddWithValue("@Fin", fin);
                        cmd.Parameters.AddWithValue("@Total", total);
                        cmd.Parameters.AddWithValue("@IdLocal", idLocal);

                        idPromocionGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    
                    string queryDetalle = @"INSERT INTO dbo.PromocionProducto (IdPromocion, IdProducto, Descripcion, Precio) 
                            VALUES (@IdPromocion, @IdProducto, @Descripcion, @Precio)";

                    
                    foreach (Producto prod in productos)
                    {
                        using (SqlCommand cmdDet = new SqlCommand(queryDetalle, cn, transaccion))
                        {
                            cmdDet.Parameters.AddWithValue("@IdPromocion", idPromocionGenerado);
                            cmdDet.Parameters.AddWithValue("@IdProducto", prod.Id);
                            cmdDet.Parameters.AddWithValue("@Descripcion", prod.Descripcion); 
                            cmdDet.Parameters.AddWithValue("@Precio", prod.Precio);           

                            cmdDet.ExecuteNonQuery();
                        }
                    }

                    transaccion.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    return false;
                }
            }
        }
    }
}


                          