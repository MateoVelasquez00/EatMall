using System;
using System.Collections.Generic;
using EatMall.Datos;
using EatMall.Modelo;

namespace EatMall.Logica
{
    public class PedidoL
    {
        PedidoD pedidoD = new PedidoD();
        public Pedido ConfirmarPedido(List<Carrito> carrito, int idCliente, string horaEntrega)
        {
            return pedidoD.ConfirmarPedido(carrito, idCliente, horaEntrega);
        }
        public List<Pedido> MtListarPedido(int idLocal)
        {
            return pedidoD.MtListarPedidoLocal(idLocal);
        }

    }
}
