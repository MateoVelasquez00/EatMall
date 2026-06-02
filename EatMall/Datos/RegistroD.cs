using System;
using System.Data;
using System.Data.SqlClient;
using EatMall.Modelo; 

namespace EatMall.Datos
{
	public class RegistroD
	{
		public int MtRegistrarUsuarioFinal(Cliente oCliente)
		{
			int idGenerado = 0;
			using (SqlConnection cn = ConexionDB.MtAbrirConexion())
			{
				cn.Open();

				try
				{
					SqlCommand cmd = new SqlCommand("SpRegistrarUsuarioCliente", cn);


					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@Nombre", oCliente.Nombre);
					cmd.Parameters.AddWithValue("@Apellido", oCliente.Apellido);
					cmd.Parameters.AddWithValue("@Documento", oCliente.Documento);
					cmd.Parameters.AddWithValue("@Email", oCliente.Email);
					cmd.Parameters.AddWithValue("@Telefono", (object)oCliente.Telefono ?? DBNull.Value);
					cmd.Parameters.AddWithValue("@Contraseña", oCliente.Contraseña);
					cmd.Parameters.AddWithValue("@Estado", 1);

					idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
				}
				catch (Exception ex)
				{
					throw new Exception("Error: " + ex.Message);
				}
			}
			return idGenerado;
		}
	}
}