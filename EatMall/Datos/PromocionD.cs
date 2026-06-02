using EatMall.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace EatMall.Datos
{
    public class PromocionD
    {
        public List<Promocion> MtListarPromocionesPorPlazoleta(int idPlazoleta)
        {
            List<Promocion> lista = new List<Promocion>();
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = "SELECT p.Id, p.Nombre, p.Imagen, p.Total, p.IdLocal, l.IdPlazoleta " +
                    "FROM dbo.Promocion p " +
                    "INNER JOIN dbo.Local l ON p.IdLocal = l.Id " +
                    "Where l.IdPlazoleta = @idPlazoleta";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@idPlazoleta", idPlazoleta);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Promocion()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Imagen = dr["Imagen"].ToString(),
                                Total = Convert.ToDecimal(dr["Total"]),
                                IdLocal = Convert.ToInt32(dr["IdLocal"]),
                                IdPlazoleta = Convert.ToInt32(dr["IdPlazoleta"])

                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}


                          