using EatMall.Modelo;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EatMall.Datos
{
    public class TransaccionD
    {
        public int InsertarTransaccion(Transaccion oTransaccion)
        {
            int idTransaccion = 0;

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Transaccion (IdMetodoPago, Monto, Estado, FechaTransaccion) " +
                    "VALUES (@IdMetodoPago, @Monto, @Estado, @FechaTransaccion); " +
                    "SELECT SCOPE_IDENTITY();", cn))
                {
                    cmd.Parameters.AddWithValue("@IdMetodoPago", oTransaccion.IdMetodoPago);
                    cmd.Parameters.AddWithValue("@Monto", oTransaccion.Monto);
                    cmd.Parameters.AddWithValue("@Estado", "Aprobado");
                    cmd.Parameters.AddWithValue("@FechaTransaccion", DateTime.Now);

                    idTransaccion = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return idTransaccion;
        }

        public void InsertarPedidoTransaccion(int idPedido, int idTransaccion)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO PedidoTransaccion (IdPedido, IdTransaccion) " +
                    "VALUES (@IdPedido, @IdTransaccion)", cn))
                {
                    cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                    cmd.Parameters.AddWithValue("@IdTransaccion", idTransaccion);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}