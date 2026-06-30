using EatMall.Datos;
using EatMall.Modelo;
using System.Collections.Generic;

namespace EatMall.Logica
{
    public class CentroComercialL
    {
        CentroComercialD oDatosCC = new CentroComercialD();
        public List<CentroComercial> MtListarCentrosComercial()
        {
            return oDatosCC.MtListarCentroComercial();
        }
        public bool MtCambiarEstadoCentroComercial(int idCentroComercial, bool estado)
        {
            return oDatosCC.MtCambiarEstadoCentroComercial(idCentroComercial, estado);
        }
        public CentroComercial MtObtenerCentroComercialPorId(int id)
        {
            return oDatosCC.MtObtenerCentroComercialPorId(id);
        }

        public bool MtActualizarCentroComercial(CentroComercial cc)
        {
            return oDatosCC.MtActualizarCentroComercial(cc);
        }
        public bool MtCrearCentroComercial(CentroComercial cc)
        {
            return oDatosCC.MtCrearCentroComercial(cc);
        }
        public List<CentroComercial> MtListarCentroComercialAdmin()
        {
            return oDatosCC.MtListarCentroComercialAdmin();
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