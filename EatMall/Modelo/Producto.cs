using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EatMall.Modelo
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        public int IdPlazoleta { get; set; }
        public int IdCC { get; set; }
        public Local Local { get; set; }
        public int IdCategoria { get; set; }
        public CentroComercial CentroComercial { get; set; }







    }
}