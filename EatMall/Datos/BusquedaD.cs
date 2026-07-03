using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EatMall.Datos
{
    public class BusquedaD
    {
        public List<Producto> MtBuscarProductos(string busqueda)
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = @"SELECT P.Id,
                                           P.Nombre,
                                           P.Precio,
                                           P.Descripcion,   
                                           P.Imagen,
                                           L.Id AS IdLocal,
                                           Pl.Id AS IdPlazoleta,
                                           CC.Id AS IdCC,
                                           L.Nombre AS NombreLocal,
                                           CC.Nombre AS NombreCentroComercial
                                   FROM Producto P
                                   INNER JOIN Local  L ON P.IdLocal = L.Id
                                   INNER JOIN Plazoleta Pl ON Pl.Id = L.IdPlazoleta
                                   INNER JOIN CentroComercial CC on Pl.IdCentroComercial = CC.Id
                                   WHERE P.Nombre LIKE '%' + @Busqueda + '%'
                                   AND P.Estado = 'True'
                                   AND CC.Estado = 'True'
                                   AND L.Estado = 'Abierto'";

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Busqueda", busqueda);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Imagen = dr["Imagen"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                IdPlazoleta = Convert.ToInt32(dr["IdPlazoleta"]),
                                IdCC = Convert.ToInt32(dr["IdCC"]),
                                Local = new Local()
                                {
                                    Id = Convert.ToInt32(dr["IdLocal"]),
                                    Nombre = dr["NombreLocal"].ToString(),
                                },
                                CentroComercial = new CentroComercial()
                                {
                                    Nombre = dr["NombreCentroComercial"].ToString()
                                }

                            });
                        }
                    }
                }
            }
            return lista;
        }
        public List<Local> MtBuscarLocales(string busqueda)
        {
            List<Local> lista = new List<Local>();

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = @"SELECT 
                                  l.Id,
                                  l.Nombre as Local,
                                  l.Descripcion,
                                  l.Estado,
                                  l.Imagen,
                                  p.Nombre AS Plazoleta,
                                  p.Id AS IdPlazoleta,
                                  cc.Id AS IdCC,
                                  cc.Nombre AS CentroComercial
                                  FROM Local l
                                  INNER JOIN Plazoleta p ON l.IdPlazoleta = p.Id
                                  INNER JOIN CentroComercial cc ON p.IdCentroComercial = cc.Id
                                  WHERE l.Nombre LIKE '%' + @Busqueda + '%'
                                  AND l.Estado = 'Abierto'
                                  AND cc.Estado = 'True'";

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Busqueda", busqueda);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Local()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Local"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Imagen = dr["Imagen"].ToString(),
                                IdPlazoleta = Convert.ToInt32(dr["IdPlazoleta"]),
                                IdCC = Convert.ToInt32(dr["IdCC"]),
                                Plazoleta = new Plazoleta()
                                {
                                    Nombre = dr["Plazoleta"].ToString()
                                },
                                CentroComercial = new CentroComercial()
                                {
                                    Nombre = dr["CentroComercial"].ToString()
                                }

                            });
                        }
                    }
                }
            }
            return lista;
        }
        public List<CentroComercial> MtBuscarCentroComercialPorCiudad(string busqueda)
        {
            List<CentroComercial> lista = new List<CentroComercial>();

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = @"SELECT 
                    cc.Id,
                    cc.Nombre,
                    cc.Descripcion,
                    cc.Imagen,
                    cc.Direccion,
                    c.NombreCiudad
                    FROM CentroComercial cc
                    INNER JOIN Ciudad c ON cc.IdCiudad = c.Id
                    WHERE c.NombreCiudad LIKE '%' + @Busqueda + '%'
                    AND cc.Estado = 'True'"; ;

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Busqueda", busqueda);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new CentroComercial()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Imagen = dr["Imagen"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Ciudad = new Ciudad()
                                {
                                    NombreCiudad = dr["NombreCiudad"].ToString()
                                }
                            });
                        }
                    }
                }
            }
            return lista;
        }
        public List<CentroComercial> MtBuscarCentroComercialPorNombre(string busqueda)
        {
            List<CentroComercial> lista = new List<CentroComercial>();

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = @"SELECT 
                    cc.Id as IdCentroComercial,
                    cc.Nombre,
                    cc.Descripcion,
                    cc.Imagen,
                    cc.Direccion,
                    cc.UbicacionUrl,
                    c.NombreCiudad
                    FROM CentroComercial cc
                    INNER JOIN Ciudad c ON cc.IdCiudad = c.Id
                    WHERE cc.Nombre LIKE '%' + @Busqueda + '%' 
                    AND cc.Estado = 'True'";

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Busqueda", busqueda);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new CentroComercial()
                            {
                                Id = Convert.ToInt32(dr["IdCentroComercial"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Imagen = dr["Imagen"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Ciudad = new Ciudad()
                                {
                                    NombreCiudad = dr["NombreCiudad"].ToString()
                                }
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
