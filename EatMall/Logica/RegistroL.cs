using System;
using EatMall.Datos;
using EatMall.Modelo;
using EatMall.Vista.Auth;

namespace EatMall.Logica
{
    public class RegistroL
    {
        RegistroD oRegistroD = new RegistroD();
        public bool MtValidarDatosProspecto(Cliente oCliente)
        {
            if (string.IsNullOrWhiteSpace(oCliente.Nombre))
                throw new Exception("El nombre es obligatorio");

            if (string.IsNullOrWhiteSpace(oCliente.Apellido))
                throw new Exception("El apellido es obligatorio");

            if (string.IsNullOrWhiteSpace(oCliente.Documento))
                throw new Exception("El documento es obligatorio");

            if (string.IsNullOrWhiteSpace(oCliente.Email))
                throw new Exception("El correo es obligatorio");

            if (!oCliente.Email.Contains("@"))
                throw new Exception("El correo ingresado no es válido");

            if (string.IsNullOrWhiteSpace(oCliente.Contraseña))
                throw new Exception("La contraseña es obligatoria");

            if (oCliente.Contraseña.Length < 6)
                throw new Exception("La contraseña debe tener al menos 6 caracteres");

            return true;
        }
        public bool MtInsertarClienteFinal(Cliente oCliente)
        {

            int resultado = oRegistroD.MtRegistrarUsuarioFinal(oCliente);
            return resultado > 0;
        }
    }
}
