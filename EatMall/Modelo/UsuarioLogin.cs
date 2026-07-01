using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatMall.Modelo
{
    public class UsuarioLogin
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string UrlInicio { get; set; }
        public bool Estado { get; set; }
        public int IdCC { get; set; }
    }
}
