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
                                          P.Id AS IdProducto,
                                          P.Nombre AS NombreProducto,
                                          P.Imagen AS ImagenProducto,
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
                                ImagenProducto = dr["ImagenProducto"].ToString(),
                                PrecioProducto = Convert.ToDecimal(dr["PrecioProducto"]),
                                NombreLocal = dr["NombreLocal"].ToString(),
                                NombreCC = dr["NombreCC"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}