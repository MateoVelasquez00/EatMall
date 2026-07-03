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
		LoginD oLoginD = new LoginD();
		public UsuarioLogin MtLogin(UsuarioLogin oDatos, bool esFuncionario)
		{
			return oLoginD.MtLogin(oDatos, esFuncionario);
		}
	}
}