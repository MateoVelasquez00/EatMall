using EatMall.Modelo;          
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EatMall.Datos
{
    public class MetodoPagoD
    {
        public List<EatMall.Modelo.MetodoPago> ObtenerMetodos(int idLocal)  
        {
            List<EatMall.Modelo.MetodoPago> lista = new List<EatMall.Modelo.MetodoPago>();

            using (SqlConnection con = ConexionDB.MtAbrirConexion())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT Id, NombreMetodo, Estado, Imagen FROM MetodoPago WHERE Estado = 1 AND IdLocal = @idLocal", con))
            {
                cmd.Parameters.AddWithValue("@idLocal", idLocal);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new EatMall.Modelo.MetodoPago  
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            NombreMetodo = reader["NombreMetodo"].ToString(),
                            Estado = Convert.ToBoolean(reader["Estado"]),
                            Imagen = reader["Imagen"] == DBNull.Value ? "" : reader["Imagen"].ToString()
                        });
                    }
                }
            }

            return lista;
        }
    }
}
