using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EatMall.Datos
{
    public class ProductoD
    {
        public List<Producto> ObtenerProductos(int idLocal)
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("SpListarProductosPorLocal", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdLocal", idLocal);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Imagen = dr["Imagen"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                Estado = Convert.ToInt32(dr["Estado"]) == 1,
                                IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                Local = new Local()
                                {
                                    Id = Convert.ToInt32(dr["IdLocal"])
                                }
                            });
                        }
                    }
                }
            }

            return lista;
        }
        public List<Producto> ObtenerPromocionesPorLocal(int idLocal)
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = @"SELECT Id, Nombre, Imagen, Total AS Precio
                         FROM dbo.Promocion
                         WHERE IdLocal = @IdLocal";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    
                    cmd.Parameters.AddWithValue("@IdLocal", idLocal);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),                               
                                Imagen = dr["Imagen"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                Descripcion = "Promoción",
                                IdCategoria = 8
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}