using System;
using System.Collections.Generic;
using EatMall.Datos;
using EatMall.Modelo;

namespace EatMall.Logica
{
    public class CarritoL
    {
        private CarritoD carritoD = new CarritoD();

        public List<Carrito> ObtenerCarrito()
        {
            return carritoD.ObtenerCarrito();
        }

        public void AgregarProducto(Producto producto, int cantidad = 1, int idLocal = 0)
        {
            List<Carrito> carrito = carritoD.ObtenerCarrito();
            Carrito item = carrito.Find(c => c.Id == producto.Id);

            if (item != null)
            {
                item.Cantidad += cantidad;
            }
            else
            {
                carrito.Add(new Carrito
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Cantidad = cantidad,
                    IdLocal = idLocal
                });
            }
            carritoD.GuardarCarrito(carrito);
        }

        public decimal ObtenerTotal()
        {
            List<Carrito> carrito = carritoD.ObtenerCarrito();
            decimal total = 0;
            foreach (var item in carrito)
            {
                total += item.Precio * item.Cantidad;
            }
            return total;
        }

      
        public int ObtenerCantidadTotal()
        {
            List<Carrito> carrito = carritoD.ObtenerCarrito();
            int totalUnidades = 0;
            foreach (var item in carrito)
            {
                totalUnidades += item.Cantidad;
            }
            return totalUnidades;
        }


        public void EliminarProducto(int idProducto)
        {
            List<Carrito> carrito = carritoD.ObtenerCarrito();
            carrito.RemoveAll(c => c.Id == idProducto);
            carritoD.GuardarCarrito(carrito);
        }


        public void VaciarCarritoDespuesDePedido()
        {
            carritoD.VaciarCarrito();
        }
    }
}