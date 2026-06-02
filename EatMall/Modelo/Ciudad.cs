using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatMall.Modelo
{
    public class Ciudad
    {
        public int Id { get; set; }
        public string NombreCiudad { get; set; }
        public int IdDepartamento { get; set; }
        public Departamento Departamento { get; set; }
    }
}