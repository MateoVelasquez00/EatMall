using System;
using System.Collections.Generic;
using System.Web;
using EatMall.Modelo;

namespace EatMall.Datos
{
    public class CarritoD
    {
        private const string SESSION_KEY = "carrito";

        public List<Carrito> ObtenerCarrito()
        {
            if (HttpContext.Current.Session[SESSION_KEY] == null)
            {
                HttpContext.Current.Session[SESSION_KEY] = new List<Carrito>();
            }
            return (List<Carrito>)HttpContext.Current.Session[SESSION_KEY];
        }

        public void GuardarCarrito(List<Carrito> carrito)
        {
            HttpContext.Current.Session[SESSION_KEY] = carrito;
        }

        public void VaciarCarrito()
        {
            HttpContext.Current.Session[SESSION_KEY] = new List<Carrito>();
        }
    }
}