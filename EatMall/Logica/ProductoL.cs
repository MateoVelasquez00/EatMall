using EatMall.Datos;
using EatMall.Modelo;
using System.Collections.Generic;

namespace EatMall.Logica
{
    public class ProductoL
    {
        ProductoD datos = new ProductoD();
        public bool MtCrearProducto(Producto producto)
        {
            return datos.MtCrearProducto(producto);
        }
        public List<Producto> ObtenerProductos(int idLocal)
        {
            return datos.ObtenerProductos(idLocal);
        }

        public List<Producto> ObtenerPromocionesPorLocal(int idLocal)
        {

            return new ProductoD().ObtenerPromocionesPorLocal(idLocal);
        }
        public List<Producto> MtListarProductosPorLocal(int idLocal)
        {
            return datos.MtListarProductosPorLocal(idLocal);
        }

        public Producto MtObtenerProductoPorId(int id)
        {
            return datos.MtObtenerProductoPorId(id);
        }

        public bool MtActualizarProducto(Producto producto)
        {
            return datos.MtActualizarProducto(producto);
        }

        public bool MtCambiarEstadoProducto(int idProducto, bool estado)
        {
            return datos.MtCambiarEstadoProducto(idProducto, estado);
        }
    }


}