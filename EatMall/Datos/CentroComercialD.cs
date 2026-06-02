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
    }
}