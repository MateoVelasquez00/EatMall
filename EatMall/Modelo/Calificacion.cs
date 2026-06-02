using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatMall.Modelo
{
    public class Calificacion
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }  
        public int IdLocal { get; set; }
        public decimal Puntaje { get; set; }
    }
}