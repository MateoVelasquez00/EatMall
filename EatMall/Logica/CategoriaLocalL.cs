using EatMall.Datos;
using EatMall.Modelo;
using EatMall.Vista.Usuario.GestionLocal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatMall.Logica
{
    public class CategoriaLocalL
    {
        CategoriaLocalD oDatosC = new CategoriaLocalD();
        public bool MtCambiarEstadoCategoria(int idCategoria, bool estado)
        {
            return oDatosC.MtCambiarEstadoCategoria(idCategoria, estado);
        }
        public List<CategoriaProducto> MtListarCategoria(int idLocal)
        {
            return oDatosC.MtListarCategoria(idLocal);
        }
        public bool MtCrearCategoria(CategoriaProducto categoria)
        {
            return oDatosC.MtCrearCategoria(categoria);
        }

        public bool MtActualizarCategoria(CategoriaProducto categoria)
        {
            return oDatosC.MtActualizarCategoria(categoria);
        }

        public CategoriaProducto MtObtenerCategoriaPorId(int id)
        {
            return oDatosC.MtObtenerCategoriaPorId(id);
        }
    }
}