using EatMall.Datos;
using EatMall.Modelo;
using System.Collections.Generic;

namespace EatMall.Logica
{
    public class CentroComercialL
    {
        public List<CentroComercial> MtListarCentrosComercial()
        {
            CentroComercialD oDatosCC = new CentroComercialD();
            return oDatosCC.MtListarCentroComercial();
        }
    }
}