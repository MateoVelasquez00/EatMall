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
        public CentroComercial MtObtenerCCPorId(int idCC)
        {
            CentroComercialD oDatosCC = new CentroComercialD();
            return oDatosCC.MtObtenerCCPorId(idCC);
        }

        public void MtActualizarCC(CentroComercial cc)
        {
            CentroComercialD oDatosCC = new CentroComercialD();
            oDatosCC.MtActualizarCC(cc);
        }
    }

}