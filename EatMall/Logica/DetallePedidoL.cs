using EatMall.Datos;
using EatMall.Modelo;
using EatMall.Vista.Usuario;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EatMall.Logica
{
    public class DetallePedidoL
    {
        public List<DetallePedido> MtObtenerDetalles(int idPedido)
        {
            DetallePedidoD oDetallePedidoD = new DetallePedidoD();
            return oDetallePedidoD.MtObtenerDetalles(idPedido);
        }
        public List<DetallePedido> ObtenerDetallePedido(int idPedido, int idLocal)
        {
            DetallePedidoD oDetallePedidoD = new DetallePedidoD();
            
            return oDetallePedidoD.ObtenerDetallePorLocal(idPedido, idLocal);
        }

        public void CambiarEstadoProductoPorLocal(int idPedido, int idLocal, string estado)
        {
            DetallePedidoD oDetallePedidoD = new DetallePedidoD();
            oDetallePedidoD.ActualizarEstadoProductoLocal(idPedido, idLocal, estado);
        }

        public int ContarProductosPendientes(int idPedido)
        {
            DetallePedidoD oDetallePedidoD = new DetallePedidoD();
            return oDetallePedidoD.ContarProductosPendientes(idPedido);
        }
    }
}