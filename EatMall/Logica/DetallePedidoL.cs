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
    }
}