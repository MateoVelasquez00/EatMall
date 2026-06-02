using EatMall.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EatMall.Modelo;

namespace EatMall.Logica
{
	public class LoginL
	{
		public UsuarioLogin MtLogin(UsuarioLogin oDatos, bool esFuncionario)
		{
			LoginD oLoginD = new LoginD();
			return oLoginD.MtLogin(oDatos, esFuncionario);
		}
	}
}