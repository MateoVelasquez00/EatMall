using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatMall.Modelo
{
    public class Menu
    {
		public int Id { get; set; }

		public string Nombre { get; set; }

		public int? IdPadre { get; set; }

		public string Ruta { get; set; }

		public int Orden { get; set; }

		public List<Menu> MenuHijos { get; set; } = new List<Menu>();
	}
}