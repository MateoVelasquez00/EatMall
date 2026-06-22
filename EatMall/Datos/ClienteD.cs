using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;

namespace EatMall.Datos
{
    public class ClienteD
    {
        public bool ActualizarCliente(Cliente oCliente)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string consulta = string.IsNullOrEmpty(oCliente.Contraseña)
                    ? "UPDATE Usuario SET Nombre=@Nombre, Apellido=@Apellido, Telefono=@Telefono WHERE Id=@Id"
                    : "UPDATE Usuario SET Nombre=@Nombre, Apellido=@Apellido, Telefono=@Telefono, Contraseña=@Contraseña WHERE Id=@Id";

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", oCliente.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", oCliente.Apellido);
                    cmd.Parameters.AddWithValue("@Telefono", oCliente.Telefono);
                    cmd.Parameters.AddWithValue("@Id", oCliente.Id);

                    if (!string.IsNullOrEmpty(oCliente.Contraseña))
                        cmd.Parameters.AddWithValue("@Contraseña", oCliente.Contraseña);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public List<Pedido> ObtenerPedidosPorCliente(int idCliente)
        {
            List<Pedido> lista = new List<Pedido>();
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT Id, CodigoPedido, FechaPedido, Estado, Total, TipoEntrega FROM Pedido WHERE IdCliente = @IdCliente ORDER BY FechaPedido DESC", cn))
                {
                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Pedido()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                CodigoPedido = dr["CodigoPedido"].ToString(),
                                FechaPedido = Convert.ToDateTime(dr["FechaPedido"]),
                                Estado = dr["Estado"].ToString(),
                                Total = Convert.ToDecimal(dr["Total"]),
                                TipoEntrega = dr["TipoEntrega"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public Cliente ObtenerClientePorId(int id)
        {
            Cliente oCliente = null;
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = @"SELECT Id, Documento, Nombre, Apellido, Email, Telefono, Contraseña, Estado 
									FROM Usuario WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            oCliente = new Cliente()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Documento = dr["Documento"] != DBNull.Value ? dr["Documento"].ToString() : string.Empty,
                                Nombre = dr["Nombre"] != DBNull.Value ? dr["Nombre"].ToString() : string.Empty,
                                Apellido = dr["Apellido"] != DBNull.Value ? dr["Apellido"].ToString() : string.Empty,
                                Email = dr["Email"] != DBNull.Value ? dr["Email"].ToString() : string.Empty,
                                Telefono = dr["Telefono"] != DBNull.Value ? dr["Telefono"].ToString() : string.Empty,
                            };
                            // Asignación segura de 'Estado' para soportar tanto bool como string en el modelo
                            var propEstado = typeof(Cliente).GetProperty("Estado");
                            if (propEstado != null && dr["Estado"] != DBNull.Value)
                            {
                                if (propEstado.PropertyType == typeof(bool))
                                {
                                    propEstado.SetValue(oCliente, Convert.ToBoolean(dr["Estado"]));
                                }
                                else if (propEstado.PropertyType == typeof(string))
                                {
                                    propEstado.SetValue(oCliente, dr["Estado"].ToString());
                                }
                            }
                        }
                    }
                }
            }
            return oCliente;
        }
        public List<Cliente> MtListarTodosUsuarios()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = @"
                    SELECT U.Id, U.Nombre, U.Apellido, U.Documento, U.Email,U.Estado, STRING_AGG(R.NombreRol, ', ') AS Roles
                    FROM Usuario U
                    JOIN RolUsuario RU ON RU.IdUsuario = U.Id
                    JOIN Rol R ON R.Id = RU.IdRol
                    GROUP BY U.Id, U.Nombre, U.Apellido, U.Documento, U.Email,U.Estado
                    ORDER BY U.Id";

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                Documento = dr["Documento"].ToString(),
                                Email = dr["Email"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                Rol = new Rol()
                                {
                                    Nombre = dr["Roles"].ToString()
                                }
                            });
                        }
                    }
                }
            }
            return lista;
        }
        public List<Rol> MtObtenerTodosLosRoles()
        {
            List<Rol> lista = new List<Rol>();
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = "SELECT Id, NombreRol FROM Rol";
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Rol()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["NombreRol"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<Rol> MtObtenerRolesPorUsuario(int idUsuario)
        {
            List<Rol> lista = new List<Rol>();
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = @"
            SELECT R.Id, R.NombreRol
            FROM RolUsuario RU
            JOIN Rol R ON R.Id = RU.IdRol
            WHERE RU.IdUsuario = @IdUsuario";

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Rol()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["NombreRol"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public void MtCambiarRol(int idUsuario, int idRol, bool asignar)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = asignar
                    ? "INSERT INTO RolUsuario (IdUsuario, IdRol) VALUES (@IdUsuario, @IdRol)"
                    : "DELETE FROM RolUsuario WHERE IdUsuario = @IdUsuario AND IdRol = @IdRol";

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@IdRol", idRol);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public int MtCrearUsuario(Cliente oCliente)
        {
            int idGenerado = 0;

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string consulta = @"INSERT INTO Usuario (Nombre, Apellido, Documento, Email, Telefono, Contraseña, Estado)
                    VALUES (@Nombre, @Apellido, @Documento, @Email, @Telefono, @Contraseña, @Estado);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", oCliente.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", oCliente.Apellido);
                    cmd.Parameters.AddWithValue("@Documento", oCliente.Documento);
                    cmd.Parameters.AddWithValue("@Email", oCliente.Email);
                    cmd.Parameters.AddWithValue("@Telefono", oCliente.Telefono);
                    cmd.Parameters.AddWithValue("@Contraseña", oCliente.Contraseña);
                    cmd.Parameters.AddWithValue("@Estado", true);
                    idGenerado = (int)cmd.ExecuteScalar();
                }
            }
            return idGenerado;
        }
        public bool MtCambiarEstadoUsuario(int idUsuario, bool estado)
        {
            bool resultado = false;

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string sql = @"
            UPDATE Usuario
            SET Estado = @Estado
            WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Id", idUsuario);
                cmd.Parameters.AddWithValue("@Estado", estado);

                resultado = cmd.ExecuteNonQuery() > 0;
            }

            return resultado;
        }
    }
}