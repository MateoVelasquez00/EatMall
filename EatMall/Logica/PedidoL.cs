using System;
using System.Collections.Generic;
using EatMall.Datos;
using EatMall.Modelo;

namespace EatMall.Logica
{
	public class PedidoL
	{
		public Pedido ConfirmarPedido(List<Carrito> carrito, int idCliente, string horaEntrega)
		{
			return new PedidoD().ConfirmarPedido(carrito, idCliente, horaEntrega);
		}
	}
}
