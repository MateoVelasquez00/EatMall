using EatMall.Modelo;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EatMall.Datos
{
	public class TransaccionD
	{
		public bool MtInsertarTransaccion(Transaccion oTransaccion)
		{
			bool transaccion = false;

			using (SqlConnection cn = ConexionDB.MtAbrirConexion())
			{
				cn.Open();
				string consulta = @"INSERT INTO Transaccion (IdPedido, IdMetodoPago, Monto, Estado, FechaTransaccion, PayuTransaccionId, PayuCodigoReferencia)
                    VALUES (@IdPedido, @IdMetodoPago, @Monto, @Estado, GETDATE(), @PayuTransaccionId, @PayuCodigoReferencia);
                    
                    IF @Estado = 'Aprobado'
                    BEGIN
                        UPDATE Pedido SET Estado = 'Pagado' WHERE Id = @IdPedido;
                    END";

				using (SqlCommand cmd = new SqlCommand(consulta, cn))
				{
					cmd.Parameters.AddWithValue("@IdPedido", oTransaccion.IdPedido);
					cmd.Parameters.AddWithValue("IdMetodoPago", oTransaccion.IdMetodoPago);
					cmd.Parameters.AddWithValue("@Monto", oTransaccion.Monto);
					cmd.Parameters.AddWithValue("@Estado", oTransaccion.Estado ?? "Pendiente");

					cmd.Parameters.AddWithValue("@PayuTransaccionId", (object)oTransaccion.PayuTransaccionId ?? DBNull.Value);
					cmd.Parameters.AddWithValue("@PayuCodigoReferencia", (object)oTransaccion.PayuCodigoReferencia ?? DBNull.Value);


					int filasAfectadas = cmd.ExecuteNonQuery();
					if (filasAfectadas > 0)
					{
						transaccion = true;
					}
				}
			}
			return transaccion;
		}

		public void MtActualizarEstadoTransaccion(string referencia, string estado, string payuId)
		{
			using (SqlConnection cn = ConexionDB.MtAbrirConexion())
			{
				cn.Open();
				string consulta = @"UPDATE Transaccion
									SET Estado = @Estado,
									PayuTransaccionId = @PayuTransaccionId
									WHERE PayuCodigoReferencia = @PayuCodigoReferencia";

				using (SqlCommand cmd = new SqlCommand(consulta, cn))
				{
					cmd.Parameters.AddWithValue("@Estado", estado);
					cmd.Parameters.AddWithValue("@PayuTransaccionId", payuId);
					cmd.Parameters.AddWithValue("@PayuCodigoReferencia", referencia);

					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}