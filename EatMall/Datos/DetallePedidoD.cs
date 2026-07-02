using EatMall.Modelo;
using EatMall.Vista.Usuario;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace EatMall.Datos
{
    public class DetallePedidoD
    {
        public List<DetallePedido> MtObtenerDetalles(int idPedido)
        {
            List<DetallePedido> lista = new List<DetallePedido>();

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();

      string consulta = @"SELECT DP.Id,
                                  DP.Cantidad,
                                  DP.Subtotal,
                                  DP.EstadoProducto, 
                                  DP.IdLocal, -- <-- AQUÍ FALTABA
                                  P.Id AS IdProducto,
                                  P.Nombre AS NombreProducto,
                                  P.Imagen AS Imagen,
                                  P.Precio AS PrecioProducto,
                                  P.Descripcion AS Descripcion,
                                  L.Nombre AS NombreLocal,
                                  CC.Nombre AS NombreCC
                           FROM DetallePedido DP
                           INNER JOIN Producto P ON DP.IdProducto = P.Id
                           INNER JOIN Local L ON DP.IdLocal = L.Id
                           INNER JOIN Plazoleta PL ON L.IdPlazoleta = PL.Id
                           INNER JOIN CentroComercial CC ON PL.IdCentroComercial = CC.Id
                           WHERE DP.IdPedido = @IdPedido";
                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IdPedido", idPedido);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new DetallePedido
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                Subtotal = Convert.ToDecimal(dr["Subtotal"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Imagen = dr["Imagen"].ToString(),
                                PrecioProducto = Convert.ToDecimal(dr["PrecioProducto"]),
                                NombreLocal = dr["NombreLocal"].ToString(),
                                NombreCC = dr["NombreCC"].ToString(),
                                EstadoProducto = dr["EstadoProducto"].ToString(),
                                IdLocal = Convert.ToInt32(dr["IdLocal"])

                            });
                        }
                    }
                }
            }
            return lista;
        }
        public List<DetallePedido> ObtenerDetallePorLocal(int idPedido, int idLocal)
        {
            List<DetallePedido> lista = new List<DetallePedido>();

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                if (cn.State == ConnectionState.Closed) cn.Open();

                string consulta = @"SELECT DP.Id, DP.Cantidad, DP.Subtotal, DP.EstadoProducto, 
                                           DP.IdLocal, -- <-- AQUÍ TAMBIÉN FALTABA
                                           P.Id AS IdProducto, P.Nombre AS NombreProducto, P.Imagen AS Imagen,
                                           P.Precio AS PrecioProducto, P.Descripcion AS Descripcion,
                                           L.Nombre AS NombreLocal, CC.Nombre AS NombreCC
                                    FROM DetallePedido DP
                                    INNER JOIN Producto P ON DP.IdProducto = P.Id
                                    INNER JOIN Local L ON DP.IdLocal = L.Id
                                    INNER JOIN Plazoleta PL ON L.IdPlazoleta = PL.Id
                                    INNER JOIN CentroComercial CC ON PL.IdCentroComercial = CC.Id
                                    WHERE DP.IdPedido = @IdPedido AND DP.IdLocal = @IdLocal";

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                    cmd.Parameters.AddWithValue("@IdLocal", idLocal);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearDetalle(dr));
                        }
                    }
                }
            }
            return lista;
        }
        public void ActualizarEstadoProductoLocal(int idPedido, int idLocal, string estado)
        {
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                if (cn.State == ConnectionState.Closed) cn.Open();

                string consulta = @"UPDATE DetallePedido 
                                    SET EstadoProducto = @Estado 
                                    WHERE IdPedido = @IdPedido AND IdLocal = @IdLocal";

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@Estado", estado);
                    cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                    cmd.Parameters.AddWithValue("@IdLocal", idLocal);

                    cmd.ExecuteNonQuery();
                }
            }
        }
            public int ContarProductosPendientes(int idPedido)
        {
            int pendientes = 0;

            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                if (cn.State == ConnectionState.Closed) cn.Open();

                string consulta = @"SELECT COUNT(*) FROM DetallePedido 
                                    WHERE IdPedido = @IdPedido AND EstadoProducto <> 'Listo'";

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                    pendientes = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return pendientes;
        }
        private DetallePedido MapearDetalle(SqlDataReader dr)
        {
            return new DetallePedido
            {
                Id = Convert.ToInt32(dr["Id"]),
                Cantidad = Convert.ToInt32(dr["Cantidad"]),
                Subtotal = Convert.ToDecimal(dr["Subtotal"]),
                Descripcion = dr["Descripcion"].ToString(),
                NombreProducto = dr["NombreProducto"].ToString(),
                Imagen = dr["Imagen"].ToString(),
                PrecioProducto = Convert.ToDecimal(dr["PrecioProducto"]),
                NombreLocal = dr["NombreLocal"].ToString(),
                NombreCC = dr["NombreCC"].ToString(),
                EstadoProducto = dr["EstadoProducto"].ToString(),
                IdLocal = Convert.ToInt32(dr["IdLocal"]),
            };
        }
    }
}
    
    
