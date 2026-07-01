using EatMall.Datos;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatMall.Logica
{
    public class CajeroL
    {
        CajeroD dao = new CajeroD();

        public bool CrearCajero(Cajero cajero, int idDuenoLocal)
        {
            if (string.IsNullOrEmpty(cajero.Gmail))
                throw new Exception("El Gmail es obligatorio.");

            if (string.IsNullOrEmpty(cajero.Contraseña))
                throw new Exception("La contraseña es obligatoria.");

            if (cajero.IdLocal <= 0)
                throw new Exception("Debe seleccionar un local.");

            if (!dao.LocalPerteneceADueno(cajero.IdLocal, idDuenoLocal))
                throw new Exception("No tienes permiso para gestionar este local.");

            if (dao.ExisteGmail(cajero.Gmail))
                throw new Exception("Este Gmail ya está registrado.");

            return dao.Insertar(cajero);
        }

        public Cajero LoginCajero(string gmail, string contraseña)
        {
            if (string.IsNullOrEmpty(gmail))
                throw new Exception("El Gmail es obligatorio.");

            if (string.IsNullOrEmpty(contraseña))
                throw new Exception("La contraseña es obligatoria.");

            Cajero cajero = dao.Login(gmail, contraseña);

            if (cajero == null)
                throw new Exception("Credenciales incorrectas o cajero inactivo.");

            return cajero;
        }

        public List<Cajero> ListarCajerosPorLocal(int idLocal, int idDuenoLocal)
        {
            if (!dao.LocalPerteneceADueno(idLocal, idDuenoLocal))
                throw new Exception("No tienes permiso para ver los cajeros de este local.");

            return dao.ListarPorLocal(idLocal);
        }

        public bool CambiarEstadoCajero(int idCajero, int idLocal, int idDuenoLocal, bool nuevoEstado)
        {
            if (!dao.LocalPerteneceADueno(idLocal, idDuenoLocal))
                throw new Exception("No tienes permiso para gestionar este cajero.");

            return dao.CambiarEstado(idCajero, nuevoEstado);
        }
    }
}