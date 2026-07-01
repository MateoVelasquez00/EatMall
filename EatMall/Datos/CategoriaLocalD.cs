using EatMall.Modelo;
using EatMall.Vista.Usuario.GestionLocal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EatMall.Datos
{
    public class CategoriaLocalD
    {
        public bool MtCambiarEstadoCategoria(int idCategoria, bool estado)
        {
            bool resultado = false;

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string sql = @"
                    UPDATE CategoriaLocal
                    SET Estado = @Estado
                    WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Id", idCategoria);
                cmd.Parameters.AddWithValue("@Estado", estado);

                resultado = cmd.ExecuteNonQuery() > 0;
            }

            return resultado;
        }
        public List<CategoriaProducto> MtListarCategoria(int idLocal)
        {
            List<CategoriaProducto> listaCategoria = new List<CategoriaProducto>();

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = @"SELECT
                        CL.Id,
                        C.Nombre,
                        C.Imagen,
                        CL.Estado
                    FROM CategoriaLocal CL
                    INNER JOIN Categoria C
                        ON C.Id = CL.IdCategoria
                    WHERE CL.IdLocal = @IdLocal
                    ORDER BY C.Nombre";

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@IdLocal", idLocal);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaCategoria.Add(new CategoriaProducto()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Imagen = dr["Imagen"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
            }

            return listaCategoria;
        }
        public CategoriaProducto MtObtenerCategoriaPorId(int id)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string query = @"SELECT
                        CL.Id,
                        CL.IdLocal,
                        CL.IdCategoria,
                        CL.Estado,
                        C.Nombre,
                        C.Imagen
                    FROM CategoriaLocal CL
                    INNER JOIN Categoria C
                        ON C.Id = CL.IdCategoria
                    WHERE CL.Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            return new CategoriaProducto()
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                Nombre = rd["Nombre"].ToString(),
                                Imagen = rd["Imagen"].ToString(),
                                Estado = Convert.ToBoolean(rd["Estado"]),
                                IdLocal = Convert.ToInt32(rd["IdLocal"])
                            };
                        }
                    }
                }
            }

            return null;
        }
        public bool MtCrearCategoria(CategoriaProducto categoria)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string query = @"INSERT INTO CategoriaLocal (Nombre,Imagen,Estado,IdLocal)
                        VALUES(
                            @Nombre,
                            @Imagen,
                            1,
                            @IdLocal)";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                    cmd.Parameters.AddWithValue("@Imagen", categoria.Imagen);
                    cmd.Parameters.AddWithValue("@IdLocal", categoria.IdLocal);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool MtActualizarCategoria(CategoriaProducto categoria)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string query = @"UPDATE CategoriaLocal
                         SET
                            Nombre = @Nombre,
                            Imagen = @Imagen
                         WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                    cmd.Parameters.AddWithValue("@Imagen", categoria.Imagen);
                    cmd.Parameters.AddWithValue("@Id", categoria.Id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}