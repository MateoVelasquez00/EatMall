using EatMall.Modelo;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EatMall.Datos
{
    public class CiudadD
    {
        public List<Ciudad> MtListarCiudades()
        {
            List<Ciudad> lista = new List<Ciudad>();
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = "SELECT Id, NombreCiudad FROM Ciudad ORDER BY NombreCiudad";
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Ciudad()
                            {
                                Id = System.Convert.ToInt32(dr["Id"]),
                                NombreCiudad = dr["NombreCiudad"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}