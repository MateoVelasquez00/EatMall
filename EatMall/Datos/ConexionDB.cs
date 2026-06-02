using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EatMall.Datos
{
    public class ConexionDB
    {
        private static readonly string cadena = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDB"].ConnectionString;

        public static SqlConnection MtAbrirConexion()
        {
            if (string.IsNullOrEmpty(cadena))
            {
                throw new Exception("La cadena de conexion no se ha configurado correctamente");
            }
            return new SqlConnection(cadena);
        }
    }
}