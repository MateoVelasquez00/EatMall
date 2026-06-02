using EatMall.Datos;
using EatMall.Modelo;
using System.Collections.Generic;

namespace EatMall.Logica
{
    public class ProductoL
    {
        public List<Producto> ObtenerProductos(int idLocal)
        {
            ProductoD datos = new ProductoD();
            return datos.ObtenerProductos(idLocal);
        }

        public List<Producto> ObtenerPromocionesPorLocal(int idLocal)
        {

            return new ProductoD().ObtenerPromocionesPorLocal(idLocal); 
        }
    }


}