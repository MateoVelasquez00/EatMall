using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatMall.Modelo
{
    public class CodigoVerificacion
    {
		public int Id { get; set; }
		public int IdUsuario { get; set; }
		public string Codigo { get; set; }
		public string Tipo { get; set; } 
		public DateTime FechaExpiracion { get; set; }
		public bool Estado { get; set; }
	}
}