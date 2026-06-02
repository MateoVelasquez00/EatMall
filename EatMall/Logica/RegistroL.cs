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
			if (string.IsNullOrEmpty(oCliente.Nombre) || string.IsNullOrEmpty(oCliente.Apellido) || string.IsNullOrEmpty(oCliente.Documento))
				throw new Exception("El Documento, el nombre y el apellido son obligatorios.");

			if (string.IsNullOrEmpty(oCliente.Email) || !oCliente.Email.Contains("@"))
				throw new Exception("Por favor, ingrese un correo electrónico válido.");

			if (oCliente.Contraseña.Length < 6)
				throw new Exception("La contraseña debe tener al menos 6 caracteres.");

			return true;
		}
		public bool MtInsertarClienteFinal(Cliente oCliente)
		{

			int resultado = oRegistroD.MtRegistrarUsuarioFinal(oCliente);
			return resultado > 0;
		}
	}
}
