using EatMall.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EatMall.Datos
{
    public class CentroComercialD
    {
        public List<CentroComercial> MtListarCentroComercial()
        {
            List<CentroComercial> listaCC = new List<CentroComercial>();

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("SpListarCentrosComerciales", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaCC.Add(new CentroComercial()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                UbicacionUrl = dr["UbicacionUrl"].ToString(),
                                Imagen = dr["Imagen"].ToString(),
                                Estado = dr["Estado"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Ubicacion = dr["Direccion"].ToString(),
                                Latitud = Convert.ToDecimal(dr["Latitud"]),
                                Longitud = Convert.ToDecimal(dr["Longitud"]),
                                Ciudad = new Ciudad()
                                {
                                    Id = Convert.ToInt32(dr["IdCiudad"]),
                                    NombreCiudad = dr["Ciudad"].ToString(),
                                    IdDepartamento = Convert.ToInt32(dr["IdDepartamento"]),

                                    Departamento = new Departamento()
                                    {
                                        Id = Convert.ToInt32(dr["IdDepartamento"]),
                                        Nombre = dr["Departamento"].ToString()
                                    }
                                }
                            });
                        }
                    }
                }
            }
            return listaCC;
        }
        public CentroComercial MtObtenerCCPorId(int idCC)
        {
            CentroComercial cc = null;

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = @"SELECT Id, Nombre, Direccion, Imagen, 
                                Estado, Descripcion
                         FROM CentroComercial 
                         WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", idCC);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            cc = new CentroComercial()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Ubicacion = dr["Direccion"].ToString(),
                                Imagen = dr["Imagen"].ToString(),
                                Estado = dr["Estado"].ToString(),
                                Descripcion = dr["Descripcion"].ToString()
                            };
                        }
                    }
                }
            }
            return cc;
        }

        public void MtActualizarCC(CentroComercial cc)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = @"UPDATE CentroComercial 
                         SET Descripcion = @Descripcion,
                             Imagen      = @Imagen,
                             Estado      = @Estado
                         WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Descripcion", cc.Descripcion);
                    cmd.Parameters.AddWithValue("@Imagen", cc.Imagen);
                    cmd.Parameters.AddWithValue("@Estado", cc.Estado);
                    cmd.Parameters.AddWithValue("@Id", cc.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}