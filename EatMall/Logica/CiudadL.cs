using EatMall.Datos;
using EatMall.Modelo;
using System.Collections.Generic;

namespace EatMall.Logica
{
    public class CiudadL
    {
        CiudadD datos = new CiudadD();

        public List<Ciudad> MtListarCiudades()
        {
            return datos.MtListarCiudades();
        }
    }
}