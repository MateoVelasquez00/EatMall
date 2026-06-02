using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EatMall.Modelo;

namespace EatMall.Datos
{
    public class MenuD
    {
        public List<Menu> ObtenerMenuPorRol(string rol)
        {
            List<Menu> menus = new List<Menu>();
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string consulta = @"
                    SELECT m.Id, m.Nombre, m.IdPadre, m.Ruta, mr.Orden
                    FROM Menu m
                    INNER JOIN MenuRol mr ON m.Id = mr.IdMenu
                    INNER JOIN Rol r ON r.Id = mr.IdRol
                    WHERE r.NombreRol = @Rol
                    ORDER BY m.IdPadre, mr.Orden";

                using (SqlCommand cmd = new SqlCommand(consulta, cn))
                {
                    cmd.Parameters.AddWithValue("@Rol", rol);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            menus.Add(new Menu
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                IdPadre = dr["IdPadre"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["IdPadre"]),
                                Ruta = dr["Ruta"].ToString(),
                                Orden = Convert.ToInt32(dr["Orden"])
                            });
                        }
                    }
                }
            }
            return menus;
        }
    }
}