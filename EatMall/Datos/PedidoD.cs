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
        // ── NUEVOS PARA CAJERO ─────────────────────────────────────

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
                U.Nombre AS NombreCliente, U.Telefono AS TelefonoCliente
            FROM Pedido P
            INNER JOIN DetallePedido DP ON P.Id = DP.IdPedido
            INNER JOIN Usuario U ON P.IdCliente = U.Id
            WHERE DP.IdLocal = @IdLocal
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
                string query = @"
            SELECT 
                DP.Id, DP.IdProducto, PR.Nombre AS NombreProducto,
                DP.Cantidad, DP.PrecioUnitario, DP.Subtotal
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
                                Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                PrecioUnitario = Convert.ToDecimal(dr["PrecioUnitario"]),
                                Subtotal = Convert.ToDecimal(dr["Subtotal"])
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

 public int MtGuardarPedido(Pedido oPedido)
        {
       return new PedidoD().GuardarPedido(oPedido);
        }
    }
}
       
    
		
	
