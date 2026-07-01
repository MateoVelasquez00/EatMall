using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatMall.Modelo
{
    public class Cajero
    {
        public int Id { get; set; }
        public string Gmail { get; set; }
        public string Contraseña { get; set; }
        public bool Estado { get; set; }
        public int IdLocal { get; set; }

        public string NombreLocal { get; set; }
    }
}