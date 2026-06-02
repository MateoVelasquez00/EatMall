using EatMall.Datos;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatMall.Logica
{
    public class MetodoPagoL
    {
        public List<MetodoPago> ObtenerMetodos(int idLocal)
        {
            MetodoPagoD datos = new MetodoPagoD();
            return datos.ObtenerMetodos(idLocal);
        }
    }
}