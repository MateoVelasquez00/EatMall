using EatMall.Datos;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatMall.Logica
{
    public class BusquedaL
    {
        public List<Producto> MtBuscarProductos(string Busqueda)
        {
            BusquedaD oBusquedaD = new BusquedaD();
            return oBusquedaD.MtBuscarProductos(Busqueda);
        }
        public List<Local> MtBuscarLocales(string Busqueda)
        {
            BusquedaD oBusquedaD = new BusquedaD();
            return oBusquedaD.MtBuscarLocales(Busqueda);
        }
        public List<CentroComercial> MtBuscarCentroComercialPorCiudad(string Busqueda)
        {
            BusquedaD oBusquedaD = new BusquedaD();
            return oBusquedaD.MtBuscarCentroComercialPorCiudad(Busqueda);
        }
        public List<CentroComercial> MtBuscarCentroComercialPorNombre(string Busqueda)
        {
            BusquedaD oBusquedaD = new BusquedaD();
            return oBusquedaD.MtBuscarCentroComercialPorNombre(Busqueda);
        }
        public List<Cliente> MtBuscarUsuario(string Busqueda)
        {
            BusquedaD oBusquedaD = new BusquedaD();
            return oBusquedaD.MtBuscarUsuario(Busqueda);
        }
    }
}