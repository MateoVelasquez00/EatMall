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

    }
}