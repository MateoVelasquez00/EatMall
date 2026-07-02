using EatMall.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatMall.Logica
{
    public class PromocionL
    {
        PromocionD promocionD = new PromocionD(); 

        public List<Modelo.Promocion> MtListarPromocionesPorPlazoleta(int idPlazoleta)
        {
            return promocionD.MtListarPromocionesPorPlazoleta(idPlazoleta);
        }
        public List<Modelo.Promocion> MtListarPromocionesPorLocal(int idLocal)
        {
            return promocionD.MtListarPromocionesPorLocal(idLocal);
        }
        public bool MtCambiarEstadoPromocion(int idPromocion)
        {
            return promocionD.MtCambiarEstadoPromocion(idPromocion);
        }
    }
}