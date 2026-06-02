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
                                   AND P.Estado = 1";

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
                                  AND l.Estado = 'Abierto'";

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
                    cc.Ubicacion,
                    c.NombreCiudad
                    FROM CentroComercial cc
                    INNER JOIN Ciudad c ON cc.IdCiudad = c.Id
                    WHERE c.NombreCiudad LIKE '%' + @Busqueda + '%'"; ;

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
                                Ubicacion = dr["Ubicacion"].ToString(),
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
                    cc.Ubicacion,
                    cc.UbicacionUrl,
                    c.NombreCiudad
                    FROM CentroComercial cc
                    INNER JOIN Ciudad c ON cc.IdCiudad = c.Id
                    WHERE cc.Nombre LIKE '%' + @Busqueda + '%' ";

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
                                Ubicacion = dr["Ubicacion"].ToString(),
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
        public List<Cliente> MtBuscarUsuario(string busqueda)
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = @"
                select U.Nombre, U.Apellido, U.Documento, U.Email, STRING_AGG(R.NombreRol, ', ') AS Roles from Usuario U
                join RolUsuario RU on RU.IdUsuario = U.Id
                join Rol R on R.id = RU.IdRol
                WHERE U.Nombre LIKE '%' + @Busqueda + '%' 
                or U.Apellido like '%' + @Busqueda + '%' 
                or cast(U.Documento as varchar) LIKE '%' + @Busqueda + '%'
                or U.Email like '%' + @Busqueda + '%'
                GROUP BY U.Nombre,U.Apellido,U.Documento,U.Email" ;

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Busqueda", busqueda);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                Documento = dr["Documento"].ToString(),
                                Email = dr["Email"].ToString(),
                                Rol = new Rol()
                                {
                                    Nombre = dr["Roles"].ToString()
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
