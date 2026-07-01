using EatMall.Datos;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatMall.Logica
{
    public class PlazoletaL
    {
        public List<Plazoleta> MtListarPlazoletas(int IdCC)
        {
            PlazoletaD oDatosP = new PlazoletaD();
            return oDatosP.MtListarPlazoletas(IdCC);
        }
        public Plazoleta MtObtenerPlazoletaPorId(int idPlazoleta)
        {
            PlazoletaD oDatosP = new PlazoletaD();
            return oDatosP.MtObtenerPlazoletaPorId(idPlazoleta);
        }

        public void MtActualizarPlazoleta(Plazoleta plazoleta)
        {
            PlazoletaD oDatosP = new PlazoletaD();
            oDatosP.MtActualizarPlazoleta(plazoleta);
        }
    }
}

