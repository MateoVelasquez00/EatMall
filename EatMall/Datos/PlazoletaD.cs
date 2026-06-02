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
    }
}
