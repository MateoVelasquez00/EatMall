using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatMall.Modelo
{
    public class Promocion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public decimal Total { get; set; }
        public int IdLocal { get; set; }
        public int IdPlazoleta { get; set; }

    }
}