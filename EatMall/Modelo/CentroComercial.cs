using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatMall.Modelo
{
    public class CentroComercial
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string UbicacionUrl { get; set; }
        public string Imagen { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public int IdCiudad { get; set; }
        public int idAdminCC { get; set; }
        public string Ubicacion { get; set; }
        public string Direccion { get; set; }
        public Ciudad Ciudad { get; set; }
    }
}