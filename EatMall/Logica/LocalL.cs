using EatMall.Datos;
using EatMall.Modelo;
using System.Collections.Generic;
using System.Data;

namespace EatMall.Logica
{
    public class LocalL
    {
        public List<Local> MtListarLocales(int IdPlazoleta)
        {
            LocalD oDatosL = new LocalD();
            return oDatosL.MtListarLocales(IdPlazoleta);
        }
        public Local ObtenerLocalPorId(int id)
        {
            LocalD oDatosL = new LocalD();
            return oDatosL.ObtenerLocalPorId(id);
        }
        public DataTable MtListarTodosLocales(int idCC)
        {
            LocalD oDatosL = new LocalD();
            return oDatosL.MtListarTodosLocales(idCC);
        }

        public void MtCambiarEstadoLocal(int idLocal, string nuevoEstado)
        {
            LocalD oDatosL = new LocalD();
            oDatosL.MtCambiarEstadoLocal(idLocal, nuevoEstado);
        }
        public void MtCrearLocal(Local nuevoLocal)
        {
            LocalD oDatosL = new LocalD();
            oDatosL.MtCrearLocal(nuevoLocal);
        }
        public void MtActualizarLocal(Local local)
        {
            LocalD oDatosL = new LocalD();
            oDatosL.MtActualizarLocal(local);
        }
    }
}