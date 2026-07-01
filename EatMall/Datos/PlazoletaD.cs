using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EatMall.Datos
{
    public class PlazoletaD
    {
        public List<Plazoleta> MtListarPlazoletas(int IdCC)
        {
            List<Plazoleta> listaP = new List<Plazoleta>();

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("SpListarPlazoletasPorCC", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCentroComercial", IdCC);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaP.Add(new Plazoleta()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["NombrePlazoleta"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = dr["Estado"].ToString(),
                                Imagen = dr["Imagen"].ToString(),
                                CentroComercial = new CentroComercial()
                                {
                                    Nombre = dr["NombreCentroComercial"].ToString(),
                                }
                            });
                        }
                    }
                }
            }
            return listaP;
        }
        public Plazoleta MtObtenerPlazoletaPorId(int idPlazoleta)
        {
            Plazoleta plazoleta = null;

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = @"SELECT Id, Nombre, Descripcion, Estado, Imagen, IdCentroComercial
                         FROM Plazoleta
                         WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", idPlazoleta);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            plazoleta = new Plazoleta()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = dr["Estado"].ToString(),
                                Imagen = dr["Imagen"].ToString()
                            };
                        }
                    }
                }
            }
            return plazoleta;
        }

        public void MtActualizarPlazoleta(Plazoleta plazoleta)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = @"UPDATE Plazoleta 
                         SET Nombre = @Nombre,
                             Descripcion = @Descripcion,
                             Imagen = @Imagen,
                             Estado = @Estado
                         WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", plazoleta.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", plazoleta.Descripcion);
                    cmd.Parameters.AddWithValue("@Imagen", plazoleta.Imagen);
                    cmd.Parameters.AddWithValue("@Estado", plazoleta.Estado);
                    cmd.Parameters.AddWithValue("@Id", plazoleta.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
