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
                    Subtotal = item.Subtotal
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
                    cmd.Parameters.AddWithValue("@CodigoPedido", pedido.CodigoPedido);
                    cmd.Parameters.AddWithValue("@FechaPedido", pedido.FechaPedido);
                    cmd.Parameters.AddWithValue("@Estado", pedido.Estado);
                    cmd.Parameters.AddWithValue("@Total", pedido.Total);
                    cmd.Parameters.AddWithValue("@TipoEntrega", pedido.TipoEntrega);
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
                string query = @"INSERT INTO DetallePedido (IdPedido, IdProducto, IdLocal, Cantidad, PrecioUnitario, Subtotal)
                                 VALUES (@IdPedido, @IdProducto, @IdLocal, @Cantidad, @PrecioUnitario, @Subtotal)";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@IdPedido", detalle.IdPedido);
                    cmd.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                    cmd.Parameters.AddWithValue("@IdLocal", detalle.IdLocal);
                    cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                    cmd.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Pedido> MtListarPedidoLocal(int idLocal)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = @"SELECT DISTINCT
                P.Id,
                P.CodigoPedido,
                P.FechaPedido,
                P.Estado,
                P.Total,
                P.TipoEntrega,
                P.HoraEntrega,
                P.IdCliente,
                CONCAT(U.Nombre,' ',U.Apellido) AS NombreCliente
            FROM Pedido P
            INNER JOIN Usuario U
                ON U.Id = P.IdCliente
            INNER JOIN DetallePedido DP
                ON DP.IdPedido = P.Id
            WHERE DP.IdLocal = @IdLocal
            ORDER BY P.FechaPedido DESC";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@IdLocal", idLocal);

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        List<Pedido> pedidos = new List<Pedido>();
                        while (rd.Read())
                        {
                            Pedido pedido = new Pedido()
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                CodigoPedido = rd["CodigoPedido"].ToString(),
                                FechaPedido = Convert.ToDateTime(rd["FechaPedido"]),
                                Estado = rd["Estado"].ToString(),
                                Total = Convert.ToDecimal(rd["Total"]),
                                TipoEntrega = rd["TipoEntrega"].ToString(),
                                IdCliente = Convert.ToInt32(rd["IdCliente"]),
                                HoraEntrega = (TimeSpan)rd["HoraEntrega"],
                                NombreCliente = rd["NombreCliente"].ToString()
                            }
                        ;
                            pedidos.Add(pedido);
                        }
                        return pedidos;
                    }
                }
            }
        }

    }
}