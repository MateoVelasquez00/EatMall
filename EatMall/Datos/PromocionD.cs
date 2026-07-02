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
                                "WHERE l.IdPlazoleta = @idPlazoleta";

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

    }
}


                          