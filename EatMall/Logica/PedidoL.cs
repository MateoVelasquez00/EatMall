using System;
using System.Collections.Generic;
using EatMall.Datos;
using EatMall.Modelo;
using EatMall.Vista.Usuario.GestionLocal;

namespace EatMall.Logica
{
	public class PedidoL
	{
		PedidoD oPedidoD = new PedidoD();
		public Pedido ConfirmarPedido(List<Carrito> carrito, int idCliente, string horaEntrega)
		{
			return new PedidoD().ConfirmarPedido(carrito, idCliente, horaEntrega);
		}
		public List<Pedido> ListarPedidosPorLocal(int idLocal)
		{
			if (idLocal <= 0)
				throw new Exception("Local no válido.");

			return oPedidoD.ListarPedidosPorLocal(idLocal);
		}

		public List<DetallePedido> ObtenerDetallePedido(int idPedido, int idLocal)
		{
			if (idPedido <= 0)
				throw new Exception("Pedido no válido.");

			if (idLocal <= 0)
				throw new Exception("Local no válido.");

			return oPedidoD.ObtenerDetallePedido(idPedido, idLocal);
		}

		public bool CambiarEstadoPedido(int idPedido, string nuevoEstado)
		{
			if (idPedido <= 0)
				throw new Exception("Pedido no válido.");

			if (string.IsNullOrEmpty(nuevoEstado))
				throw new Exception("El estado no puede estar vacío.");

			return oPedidoD.CambiarEstadoPedido(idPedido, nuevoEstado);
		}
		public int MtGuardarPedido(Pedido oPedido)
		{
			return new PedidoD().GuardarPedido(oPedido);
		}

		public List<Pedido> MtListarPedido(int idLocal)
		{
			return oPedidoD.MtListarPedidoLocal(idLocal);
		}
		public Transaccion ObtenerTransaccionPorPedido(int idPedido)
		{
			PedidoD datos = new PedidoD();
			return datos.ObtenerTransaccionPorPedido(idPedido);
		}
		private PedidoD datos = new PedidoD();

		public bool CambiarEstadoProductoPorLocal(int idPedido, int idLocal, string nuevoEstado)
		{
			return datos.CambiarEstadoProductoPorLocal(idPedido, idLocal, nuevoEstado);
		}

		public int ObtenerProductosPendientes(int idPedido)
		{
			return datos.ObtenerProductosPendientes(idPedido);
		}
	}
}


