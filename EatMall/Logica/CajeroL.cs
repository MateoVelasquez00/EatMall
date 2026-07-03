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
        CajeroD oCajeroD = new CajeroD();

        public bool CrearCajero(Cajero cajero, int idDuenoLocal)
        {
            if (string.IsNullOrEmpty(cajero.Gmail))
                throw new Exception("El Gmail es obligatorio.");

            if (string.IsNullOrEmpty(cajero.Contraseña))
                throw new Exception("La contraseña es obligatoria.");

            if (cajero.IdLocal <= 0)
                throw new Exception("Debe seleccionar un local.");

            if (!oCajeroD.LocalPerteneceADueno(cajero.IdLocal, idDuenoLocal))
                throw new Exception("No tienes permiso para gestionar este local.");

            if (oCajeroD.ExisteGmail(cajero.Gmail))
                throw new Exception("Este Gmail ya está registrado.");

            return oCajeroD.Insertar(cajero);
        }

        public Cajero LoginCajero(string gmail, string contraseña)
        {
            if (string.IsNullOrEmpty(gmail))
                throw new Exception("El Gmail es obligatorio.");

            if (string.IsNullOrEmpty(contraseña))
                throw new Exception("La contraseña es obligatoria.");

            Cajero cajero = oCajeroD.Login(gmail, contraseña);

            if (cajero == null)
                throw new Exception("Credenciales incorrectas o cajero inactivo.");

            return cajero;
        }

        public List<Cajero> ListarCajerosPorLocal(int IdDuenoLocal)
        {
            return oCajeroD.ListarPorLocal(IdDuenoLocal);
        }

        public bool CambiarEstadoCajero(int idCajero, int idLocal, int idDuenoLocal, bool nuevoEstado)
        {
            if (!oCajeroD.LocalPerteneceADueno(idLocal, idDuenoLocal))
                throw new Exception("No tienes permiso para gestionar este cajero.");

            return oCajeroD.CambiarEstado(idCajero, nuevoEstado);
        }
    }
}