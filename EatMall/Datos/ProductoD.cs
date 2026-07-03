using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EatMall.Datos
{
    public class ProductoD
    {
        public bool MtCrearProducto(Producto producto)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string query = @"
                        INSERT INTO Producto
                        (
                            Nombre,
                            Descripcion,
                            Imagen,
                            Precio,
                            Estado,
                            IdLocal,
                            IdCategoria
                        )
                        VALUES
                        (
                            @Nombre,
                            @Descripcion,
                            @Imagen,
                            @Precio,
                            1,
                            @IdLocal,
                            @IdCategoria
                        )";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@Imagen", producto.Imagen);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@IdLocal", producto.Local.Id);
                    cmd.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
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
                         WHERE IdLocal = @IdLocal AND Estado = 1";
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
        public List<Producto> MtListarProductosPorLocal(int idLocal)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string query = @"SELECT
                                    Id,
                                    Nombre,
                                    Descripcion,
                                    Imagen,
                                    Precio,
                                    Estado,
                                    IdCategoria
                                FROM Producto
                                WHERE IdLocal = @IdLocal
                                ORDER BY Nombre";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@IdLocal", idLocal);

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        List<Producto> lista = new List<Producto>();

                        while (rd.Read())
                        {
                            lista.Add(new Producto()
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                Nombre = rd["Nombre"].ToString(),
                                Descripcion = rd["Descripcion"].ToString(),
                                Imagen = rd["Imagen"].ToString(),
                                Precio = Convert.ToDecimal(rd["Precio"]),
                                Estado = Convert.ToBoolean(rd["Estado"]),
                                IdCategoria = Convert.ToInt32(rd["IdCategoria"])
                            });
                        }

                        return lista;
                    }
                }
            }
        }
        public Producto MtObtenerProductoPorId(int id)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string query = @"
                    SELECT
                        Id,
                        Nombre,
                        Descripcion,
                        Imagen,
                        Precio,
                        Estado,
                        IdCategoria
                    FROM Producto
                    WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            return new Producto()
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                Nombre = rd["Nombre"].ToString(),
                                Descripcion = rd["Descripcion"].ToString(),
                                Imagen = rd["Imagen"].ToString(),
                                Precio = Convert.ToDecimal(rd["Precio"]),
                                Estado = Convert.ToBoolean(rd["Estado"]),
                                IdCategoria = Convert.ToInt32(rd["IdCategoria"])
                            };
                        }
                    }
                }
            }

            return null;
        }
        public bool MtActualizarProducto(Producto producto)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string query = @"
                    UPDATE Producto
                    SET
                        Nombre = @Nombre,
                        Descripcion = @Descripcion,
                        Imagen = @Imagen,
                        Precio = @Precio,
                        IdCategoria = @IdCategoria
                    WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", producto.Id);
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@Imagen", producto.Imagen);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool MtCambiarEstadoProducto(int idProducto, bool estado)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string query = @"
                    UPDATE Producto
                    SET Estado = @Estado
                    WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Estado", estado);
                    cmd.Parameters.AddWithValue("@Id", idProducto);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

		public bool CambiarEstadoProducto(int idProducto)
		{
			bool actualizado = false;

			using (SqlConnection cn = ConexionDB.MtAbrirConexion())
			{
				cn.Open();

				string query = "UPDATE Producto SET Estado = CASE WHEN Estado = 1 THEN 0 ELSE 1 END WHERE Id = @Id";

				using (SqlCommand cmd = new SqlCommand(query, cn))
				{
					cmd.Parameters.AddWithValue("@Id", idProducto);

					int filasAfectadas = cmd.ExecuteNonQuery();
					actualizado = filasAfectadas > 0;
				}
			}

			return actualizado;
		}

	}
}