using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatMall.Modelo
{
    public class Local
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Imagen { get; set; }
        public string Estado { get; set; }
        public double Promedio { get; set; }
        public int IdPlazoleta { get; set; }
        public int IdCC { get; set; }
        public string HorarioLocal { get; set; }
        public Plazoleta Plazoleta { get; set; }
        public CentroComercial CentroComercial { get; set; }
        public Calificacion Calificacion { get; set; }
    }
}