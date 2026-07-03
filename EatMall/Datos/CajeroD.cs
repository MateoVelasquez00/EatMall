using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
namespace EatMall.Datos
{
    public class CajeroD
    {
        public bool Insertar(Cajero cajero)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = "INSERT INTO CajeroLocal (Gmail, Contraseña, Estado, IdLocal) " +
                               "VALUES (@Gmail, @Contraseña, 1, @IdLocal)";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Gmail", cajero.Gmail);
                cmd.Parameters.AddWithValue("@Contraseña", cajero.Contraseña);
                cmd.Parameters.AddWithValue("@IdLocal", cajero.IdLocal);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool ExisteGmail(string gmail)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = "SELECT COUNT(*) FROM CajeroLocal WHERE Gmail = @Gmail";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Gmail", gmail);

                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool LocalPerteneceADueno(int idLocal, int idDuenoLocal)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = "SELECT COUNT(*) FROM Local WHERE Id = @IdLocal AND IdDuenoLocal = @IdDuenoLocal";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@IdLocal", idLocal);
                cmd.Parameters.AddWithValue("@IdDuenoLocal", idDuenoLocal);

                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public Cajero Login(string gmail, string contraseña)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = @"
                SELECT CL.Id, CL.Gmail, CL.Estado, CL.IdLocal, L.Nombre AS NombreLocal
                FROM CajeroLocal CL
                INNER JOIN Local L ON CL.IdLocal = L.Id
                WHERE CL.Gmail = @Gmail AND CL.Contraseña = @Contraseña AND CL.Estado = 1";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Gmail", gmail);
                cmd.Parameters.AddWithValue("@Contraseña", contraseña);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Cajero
                    {
                        Id = (int)reader["Id"],
                        Gmail = reader["Gmail"].ToString(),
                        Estado = (bool)reader["Estado"],
                        IdLocal = (int)reader["IdLocal"],
                        NombreLocal = reader["NombreLocal"].ToString()
                    };
                }
                return null;
            }
        }
        public bool CambiarEstado(int idCajero, bool estado)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = "UPDATE CajeroLocal SET Estado = @Estado WHERE Id = @IdCajero";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Estado", estado);
                cmd.Parameters.AddWithValue("@IdCajero", idCajero);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Cajero> ListarPorLocal(int IdDuenoLocal)
        {
            List<Cajero> oCajero = new List<Cajero>();

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = @"  SELECT CL.Id, CL.Gmail, CL.Estado, CL.IdLocal, L.Nombre, L.IdDueñoLocal 
                            FROM CajeroLocal AS CL 
                            INNER JOIN Local AS L 
                                ON CL.IdLocal = L.Id
                            WHERE L.IdDueñoLocal = @IdDueñoLocal";

                using (SqlCommand cmd = new SqlCommand(consulta,cn))
                {
                    cmd.Parameters.AddWithValue("@IdDueñoLocal", IdDuenoLocal);

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            oCajero.Add(new Cajero
                            {
                                Id = Convert.ToInt32(rd["Id"]),
                                Gmail = rd["Gmail"].ToString(),
                                Estado = Convert.ToBoolean(rd["Estado"]),
                                IdLocal = Convert.ToInt32(rd["IdLocal"]),
                                NombreLocal = rd["Nombre"].ToString(),
                                IdDuenoLocal = Convert.ToInt32(rd["IdDueñoLocal"])
							});
                        }
                    }
                }
			}
			return oCajero;
		}
    }
}