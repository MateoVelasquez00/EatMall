using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EatMall.Datos;

namespace EatMall.Datos
{
    public class CategoriaProductoD
    {

        public List<CategoriaProducto> ObtenerCategoriasPorLocal(int idLocal)
        {
            List<CategoriaProducto> lista = new List<CategoriaProducto>();
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = @"SELECT DISTINCT cp.Id, cp.Nombre, cp.Imagen
                                 FROM dbo.CategoriaProducto cp
                                 INNER JOIN dbo.Producto p ON cp.Id = p.IdCategoria
                                 WHERE p.IdLocal = @IdLocal";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@IdLocal", idLocal);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())

                        {
                            lista.Add(new CategoriaProducto
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Imagen = dr["Imagen"].ToString()
                            });

                        }

                    }
                }
                return lista;
            }

        }
    }
}