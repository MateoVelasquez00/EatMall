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
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
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

        public bool MtCambiarEstadoCentroComercial(int idCentroComercial, bool estado)
        {
            bool resultado = false;

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                string sql = @"
                    UPDATE CentroComercial
                    SET Estado = @Estado
                    WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Id", idCentroComercial);
                cmd.Parameters.AddWithValue("@Estado", estado);

                resultado = cmd.ExecuteNonQuery() > 0;
            }

            return resultado;
        }

        public CentroComercial MtObtenerCentroComercialPorId(int id)
        {
            CentroComercial cc = null;

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = @" SELECT CC.Id, CC.Nombre, CC.Direccion, CC.UbicacionUrl, CC.Imagen,
                   CC.Estado, CC.Descripcion, CC.Latitud, CC.Longitud, CC.IdCiudad
                    FROM CentroComercial CC
                    WHERE CC.Id = @Id
                    AND CC.Estado = 1";

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            cc = new CentroComercial()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Ubicacion = dr["Direccion"].ToString(),
                                UbicacionUrl = dr["UbicacionUrl"].ToString(),
                                Imagen = dr["Imagen"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Latitud = Convert.ToDecimal(dr["Latitud"]),
                                Longitud = Convert.ToDecimal(dr["Longitud"]),

                                Ciudad = new Ciudad()
                                {
                                    Id = Convert.ToInt32(dr["IdCiudad"])
                                }
                            };
                        }
                    }
                }
            }
            return cc;
        }

        public bool MtActualizarCentroComercial(CentroComercial cc)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = @"UPDATE CentroComercial SET
                Nombre       = @Nombre,
                Direccion    = @Direccion,
                UbicacionUrl = @UbicacionUrl,
                Imagen       = @Imagen,
                Descripcion  = @Descripcion,
                Latitud      = @Latitud,
                Longitud     = @Longitud,
                IdCiudad     = @IdCiudad
                WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", cc.Id);
                    cmd.Parameters.AddWithValue("@Nombre", cc.Nombre);
                    cmd.Parameters.AddWithValue("@Direccion", cc.Ubicacion);
                    cmd.Parameters.AddWithValue("@UbicacionUrl", cc.UbicacionUrl);
                    cmd.Parameters.AddWithValue("@Imagen", cc.Imagen);
                    cmd.Parameters.AddWithValue("@Descripcion", cc.Descripcion);
                    cmd.Parameters.AddWithValue("@Latitud", cc.Latitud);
                    cmd.Parameters.AddWithValue("@Longitud", cc.Longitud);
                    cmd.Parameters.AddWithValue("@IdCiudad", cc.Ciudad.Id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Alias para mantener compatibilidad con código que usa el nombre anterior
        public void MtActualizarCC(CentroComercial cc)
        {
            MtActualizarCentroComercial(cc);
        }

        public bool MtCrearCentroComercial(CentroComercial cc)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = @"
                INSERT INTO CentroComercial 
                (Nombre, Direccion, UbicacionUrl, Imagen, Descripcion, 
                 Latitud, Longitud, IdCiudad, Estado)
                VALUES 
                (@Nombre, @Direccion, @UbicacionUrl, @Imagen, @Descripcion,
                 @Latitud, @Longitud, @IdCiudad, 1)";

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", cc.Nombre);
                    cmd.Parameters.AddWithValue("@Direccion", cc.Ubicacion);
                    cmd.Parameters.AddWithValue("@UbicacionUrl", cc.UbicacionUrl);
                    cmd.Parameters.AddWithValue("@Imagen", cc.Imagen);
                    cmd.Parameters.AddWithValue("@Descripcion", cc.Descripcion);
                    cmd.Parameters.AddWithValue("@Latitud", cc.Latitud);
                    cmd.Parameters.AddWithValue("@Longitud", cc.Longitud);
                    cmd.Parameters.AddWithValue("@IdCiudad", cc.Ciudad.Id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<CentroComercial> MtListarCentroComercialAdmin()
        {
            List<CentroComercial> listaCC = new List<CentroComercial>();

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("SpListarCentrosComercialesAdmin", cn))
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
                                Imagen = dr["Imagen"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                UbicacionUrl = dr["UbicacionUrl"].ToString(),
                                Latitud = Convert.ToDecimal(dr["Latitud"]),
                                Longitud = Convert.ToDecimal(dr["Longitud"]),
                                idAdminCC = Convert.ToInt32(dr["IdAdminCC"]),
                                Administrador = dr["Administrador"].ToString(),

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
                                Estado = Convert.ToBoolean(dr["Estado"]),
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