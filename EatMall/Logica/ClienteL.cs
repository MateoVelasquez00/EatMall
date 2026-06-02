using EatMall.Datos;
using EatMall.Modelo;
using System.Collections.Generic;

namespace EatMall.Logica
{
    public class ClienteL
    {
        private ClienteD datos = new ClienteD();

        public Cliente ObtenerClientePorId(int id)
        {
            return datos.ObtenerClientePorId(id);
        }

        public bool ActualizarCliente(Cliente oCliente)
        {
            return datos.ActualizarCliente(oCliente);
        }
        public List<Pedido> ObtenerPedidosPorCliente(int idCliente)
        {
            return datos.ObtenerPedidosPorCliente(idCliente);
        }
    }
}