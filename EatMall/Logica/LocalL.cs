using EatMall.Datos;
using EatMall.Modelo;
using System.Collections.Generic;
using System.Data;

namespace EatMall.Logica
{
    public class LocalL
    {
        LocalD oDatosL = new LocalD();

        public List<Local> MtListarLocales(int IdPlazoleta)
        {
            return oDatosL.MtListarLocales(IdPlazoleta);
        }
        public Local ObtenerLocalPorId(int id)
        {
            return oDatosL.ObtenerLocalPorId(id);
        }
        public DataTable MtListarTodosLocales(int idCC)
        {
            return oDatosL.MtListarTodosLocales(idCC);
        }
        public void MtCambiarEstadoLocal(int idLocal, string nuevoEstado)
        {
            oDatosL.MtCambiarEstadoLocal(idLocal, nuevoEstado);
        }
        public void MtCrearLocal(Local nuevoLocal)
        {
            oDatosL.MtCrearLocal(nuevoLocal);
        }
        public bool MtActualizarLocal(Local local)
        {
            return oDatosL.MtActualizarLocal(local);
        }
    }
}