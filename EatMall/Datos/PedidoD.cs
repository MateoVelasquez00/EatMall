using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EatMall.Modelo;

namespace EatMall.Datos
{
    public class PedidoD
    {
        public Pedido ConfirmarPedido(List<Carrito> carrito, int idCliente, string horaEntrega)
        {
            Pedido pedido = new Pedido
            {
                CodigoPedido = "PED-" + DateTime.Now.Ticks.ToString().Substring(10),
                FechaPedido = DateTime.Now,
                Estado = "Pendiente",
                Total = 0,
                TipoEntrega = "Local",
                IdCliente = idCliente,
                HoraEntrega = TimeSpan.Parse(horaEntrega)
            };

            foreach (var item in carrito)
                pedido.Total += item.Subtotal;

            pedido.Id = GuardarPedido(pedido);

            foreach (var item in carrito)
            {
                DetallePedido detalle = new DetallePedido
                {
                    IdPedido = pedido.Id,
                    IdProducto = item.Id,
                    IdLocal = item.IdLocal,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.Precio,
                    Subtotal = item.Subtotal,
                    EstadoProducto = "En Preparación"
                };
                GuardarDetalle(detalle);
            }

            return pedido;
        }

        public int GuardarPedido(Pedido pedido)
        {
            int idPedido = 0;
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = @"INSERT INTO Pedido (CodigoPedido, FechaPedido, Estado, Total, TipoEntrega, IdCliente, HoraEntrega)
                         VALUES (@CodigoPedido, @FechaPedido, @Estado, @Total, @TipoEntrega, @IdCliente, @HoraEntrega);
                         SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@CodigoPedido", (object)pedido.CodigoPedido ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FechaPedido", pedido.FechaPedido);
                    cmd.Parameters.AddWithValue("@Estado", pedido.Estado ?? "Pendiente");
                    cmd.Parameters.AddWithValue("@Total", pedido.Total);
                    cmd.Parameters.AddWithValue("@TipoEntrega", (object)pedido.TipoEntrega ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdCliente", pedido.IdCliente);
                    cmd.Parameters.AddWithValue("@HoraEntrega", pedido.HoraEntrega);

                    idPedido = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return idPedido;
        }

        public void GuardarDetalle(DetallePedido detalle)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = @"INSERT INTO DetallePedido (IdPedido, IdProducto, IdLocal, Cantidad, PrecioUnitario, Subtotal, EstadoProducto)
                                 VALUES (@IdPedido, @IdProducto, @IdLocal, @Cantidad, @PrecioUnitario, @Subtotal, @EstadoProducto)";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@IdPedido", detalle.IdPedido);
                    cmd.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                    cmd.Parameters.AddWithValue("@IdLocal", detalle.IdLocal);
                    cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                    cmd.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);
                    cmd.Parameters.AddWithValue("@EstadoProducto", string.IsNullOrEmpty(detalle.EstadoProducto) ? "En Preparación" : detalle.EstadoProducto);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Listar pedidos del local del cajero
        public List<Pedido> ListarPedidosPorLocal(int idLocal)
        {
            List<Pedido> lista = new List<Pedido>();
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = @"
            SELECT DISTINCT 
                P.Id, P.CodigoPedido, P.FechaPedido, P.Estado, 
                P.Total, P.TipoEntrega, P.HoraEntrega, 
                CONCAT(U.Nombre, ' ', U.Apellido) AS NombreCliente, 
                U.Telefono AS TelefonoCliente
            FROM Pedido P
            INNER JOIN DetallePedido DP ON P.Id = DP.IdPedido
            INNER JOIN Usuario U ON P.IdCliente = U.Id
            WHERE DP.IdLocal = @IdLocal
            AND CAST(P.FechaPedido AS DATE) = CAST(GETDATE() AS DATE)
            ORDER BY P.FechaPedido DESC";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@IdLocal", idLocal);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Pedido
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                CodigoPedido = dr["CodigoPedido"].ToString(),
                                FechaPedido = Convert.ToDateTime(dr["FechaPedido"]),
                                Estado = dr["Estado"].ToString(),
                                Total = Convert.ToDecimal(dr["Total"]),
                                TipoEntrega = dr["TipoEntrega"].ToString(),
                                HoraEntrega = (TimeSpan)dr["HoraEntrega"],
                                NombreCliente = dr["NombreCliente"].ToString(),
                                TelefonoCliente = dr["TelefonoCliente"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // Ver detalle de un pedido especifico del local
        public List<DetallePedido> ObtenerDetallePedido(int idPedido, int idLocal)
        {
            List<DetallePedido> lista = new List<DetallePedido>();
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string query = @"SELECT DP.Id, DP.IdProducto, PR.Nombre AS NombreProducto, PR.Imagen, DP.Cantidad, DP.PrecioUnitario, DP.Subtotal, DP.EstadoProducto 
                         FROM DetallePedido DP 
                         INNER JOIN Producto PR ON DP.IdProducto = PR.Id 
                         WHERE DP.IdPedido = @IdPedido AND DP.IdLocal = @IdLocal";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                    cmd.Parameters.AddWithValue("@IdLocal", idLocal);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new DetallePedido
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Imagen = dr["Imagen"].ToString(),
                                Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                PrecioUnitario = Convert.ToDecimal(dr["PrecioUnitario"]),
                                Subtotal = Convert.ToDecimal(dr["Subtotal"]),
                                EstadoProducto = "En Preparación"
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // Cajero cambia el estado del pedido
        public bool CambiarEstadoPedido(int idPedido, string nuevoEstado)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = "UPDATE Pedido SET Estado = @Estado WHERE Id = @IdPedido";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        // Obtener la transacción asociada a un pedido específico
        public Transaccion ObtenerTransaccionPorPedido(int idPedido)
        {
            Transaccion transaccion = null;
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string query = @"SELECT Id, Monto, Estado, FechaTransaccion, PayuCodigoReferencia 
                         FROM Transaccion 
                         WHERE IdPedido = @IdPedido";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            transaccion = new Transaccion
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Monto = Convert.ToDecimal(dr["Monto"]),
                                Estado = dr["Estado"].ToString(),
                                FechaTransaccion = Convert.ToDateTime(dr["FechaTransaccion"]),
                                PayuCodigoReferencia = dr["PayuCodigoReferencia"].ToString()
                            };
                        }
                    }
                }
            }
            return transaccion;
        }

        // Cambia el estado de los productos en DetallePedido que pertenezcan a un local específico
        public bool CambiarEstadoProductoPorLocal(int idPedido, int idLocal, string nuevoEstado)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = @"UPDATE DetallePedido 
                         SET EstadoProducto = @Estado 
                         WHERE IdPedido = @IdPedido AND IdLocal = @IdLocal";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                    cmd.Parameters.AddWithValue("@IdLocal", idLocal);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Cuenta cuántos productos de este pedido siguen "En preparación" en toda la plazoleta
        public int ObtenerProductosPendientes(int idPedido)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = @"SELECT COUNT(*) 
                         FROM DetallePedido 
                         WHERE IdPedido = @IdPedido AND EstadoProducto = 'En preparación'";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public int MtGuardarPedido(Pedido oPedido)
        {
            return new PedidoD().GuardarPedido(oPedido);
        }

    }

}




