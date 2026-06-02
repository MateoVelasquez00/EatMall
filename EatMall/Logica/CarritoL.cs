using System.Collections.Generic;
using System.Web;
using EatMall.Modelo;

namespace EatMall.Logica
{
    public class CarritoL
    {
        private const string SESSION_KEY = "Carrito";
        private const string MODIFICADO_KEY = "CarritoModificado";

        public List<Carrito> ObtenerCarrito()
        {
            if (HttpContext.Current.Session[SESSION_KEY] == null)
                HttpContext.Current.Session[SESSION_KEY] = new List<Carrito>();
            return (List<Carrito>)HttpContext.Current.Session[SESSION_KEY];
        }

        public void AgregarProducto(Producto producto, int cantidad = 1, int IdLocal = 0)
        {
            var carrito = ObtenerCarrito();
            var item = carrito.Find(c => c.Id == producto.Id);

            if (item != null)
                item.Cantidad += cantidad;
            else
                carrito.Add(new Carrito
                {
                    Id       = producto.Id,
                    Nombre   = producto.Nombre,
                    Precio   = producto.Precio,
                    Cantidad = cantidad,
                    IdLocal  = IdLocal
                   
                });

            // Marcar que hubo movimiento
            HttpContext.Current.Session[MODIFICADO_KEY] = true;
            HttpContext.Current.Session[SESSION_KEY] = carrito;
        }

        public void ActualizarCantidad(int id, int cantidad)
        {
            var carrito = ObtenerCarrito();
            var item = carrito.Find(c => c.Id == id);
            if (item != null)
            {
                item.Cantidad = cantidad;
                HttpContext.Current.Session[MODIFICADO_KEY] = true;
                HttpContext.Current.Session[SESSION_KEY] = carrito;
            }
        }

        
        // REEMPLAZA EL MÉTODO LimpiarSiNoHuboMovimiento POR ESTE:
        public void VaciarCarritoDespuesDePedido()
        {
            // Solo vaciamos la lista y el flag cuando el pedido sea REAL
            HttpContext.Current.Session[SESSION_KEY] = new List<Carrito>();
            HttpContext.Current.Session[MODIFICADO_KEY] = false;
        }

        public void EliminarProducto(int id)
        {
            var carrito = ObtenerCarrito();
            carrito.RemoveAll(c => c.Id == id);
            HttpContext.Current.Session[MODIFICADO_KEY] = true;
            HttpContext.Current.Session[SESSION_KEY] = carrito;
        }

        public int ObtenerCantidadTotal()
        {
            int total = 0;
            foreach (var item in ObtenerCarrito())
                total += item.Cantidad;
            return total;
        }

        public decimal ObtenerTotal()
        {
            decimal total = 0;
            foreach (var item in ObtenerCarrito())
                total += item.Subtotal;
            return total;
        }
    }
}