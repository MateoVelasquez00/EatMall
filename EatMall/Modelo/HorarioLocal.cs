using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace EatMall.Modelo
{
    public class HorarioLocal
    {
        public int Id { get; set; }
        public decimal HorarioApertura { get; set; }
        public decimal HorarioCierre { get; set; }
        public string Dia { get; set; }
        public Local local { get; set; }

    }
}