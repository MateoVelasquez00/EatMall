using EatMall.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

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
       public bool MtRegistrarPromocionCompleta(string nombre, string imagen, DateTime inicio, DateTime fin, decimal total, int idLocal, List<Modelo.Producto> productos)
{
    return promocionD.MtRegistrarPromocionCompleta(nombre, imagen, inicio, fin, total, idLocal, productos);
}
    }
}