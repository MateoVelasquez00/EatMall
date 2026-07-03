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
        public List<Rol> MtObtenerTodosLosRoles()
        {
            return datos.MtObtenerTodosLosRoles();
        }
        public List<Rol> MtObtenerRolesPorUsuario(int idUsuario)
        {
            return datos.MtObtenerRolesPorUsuario(idUsuario);
        }
        public void MtCambiarRol(int idUsuario, int idRol, bool asignar)
        {
            datos.MtCambiarRol(idUsuario, idRol, asignar);
        }
        public int MtCrearUsuario(Cliente oCliente)
        {
            return datos.MtCrearUsuario(oCliente);
        }
        public bool MtCambiarEstadoUsuario(int idUsuario, bool estado)
        {
            return datos.MtCambiarEstadoUsuario(idUsuario, estado);
        }
        public List<Cliente> MtListarTodosUsuario()
        {
            return datos.MtListarTodosUsuarios();
        }
		public bool MtActualizarContraseña(Cliente cliente)
		{
			return datos.MtActualizarContraseña(cliente);
		}
	}
}