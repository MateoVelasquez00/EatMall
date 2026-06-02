using EatMall.Datos;
using EatMall.Modelo;
using System.Collections.Generic;

namespace EatMall.Logica
{
    public class LocalL
    {
        public List<Local> MtListarLocales(int IdPlazoleta)
        {
            LocalD oDatosL = new LocalD();
            return oDatosL.MtListarLocales(IdPlazoleta);
        }
        public Local ObtenerLocalPorId(int id)
        {
            LocalD oDatosL = new LocalD();
            return oDatosL.ObtenerLocalPorId(id);
        }
    }
}