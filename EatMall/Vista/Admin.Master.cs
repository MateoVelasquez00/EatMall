using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EatMall.Datos;
using EatMall.Modelo;

namespace EatMall.Vista
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
            {
                UsuarioLogin oUsuario = (UsuarioLogin)Session["Usuario"];

                if (oUsuario.IdRol >= 1 && oUsuario.IdRol <= 4)
                {
                    string nombreRol = "";
                    string colorRol = "";
                    string colorLight = "";

                    switch (oUsuario.IdRol)
                    {
                        case 1:
                            nombreRol = "Administrador";
                            colorRol = "#006948";
                            colorLight = "#adedd3";
                            break;
                        case 2:
                            nombreRol = "AdministradorCC";
                            colorRol = "#1d4ed8";
                            colorLight = "#dbeafe";
                            break;
                        case 3:
                            nombreRol = "Local";
                            colorRol = "#7c3aed";
                            colorLight = "#ede9fe";
                            break;
                        case 4:
                            nombreRol = "Cajero";
                            colorRol = "#0e7490";
                            colorLight = "#cffafe";
                            break;
                    }

                    string[] partes = oUsuario.Nombre.Trim().Split(' ');
                    string iniciales = "";

                    if (partes.Length >= 2 && partes[1].Length > 0)
                        iniciales = $"{partes[0][0]}{partes[1][0]}";
                    else if (partes[0].Length > 0)
                        iniciales = $"{partes[0][0]}";
                    else
                        iniciales = "?";

                    lblUsuario.Text = oUsuario.Nombre;
                    lblRolSidebar.InnerText = nombreRol.ToUpper();
                    avatarInicial.InnerText = iniciales.ToUpper();

                    Page.ClientScript.RegisterStartupScript(
                        this.GetType(), "colorRol",
                        $@"document.documentElement.style.setProperty('--color-rol', '{colorRol}');
                   document.documentElement.style.setProperty('--color-rol-light', '{colorLight}');
                   document.querySelector('.sidebar-user-avatar').style.background = '{colorRol}';",
                        true
                    );

                    if (!IsPostBack)
                        CargarMenu(nombreRol);
                }
                else
                {
                    Response.Redirect("~/Index.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Vista/Auth/Login.aspx");
            }
        }

        private void CargarMenu(string rol)
        {
            MenuD menuD = new MenuD();
            List<Modelo.Menu> menus = menuD.ObtenerMenuPorRol(rol);

            string html = "";

            foreach (var item in menus.Where(m => m.IdPadre == null))
            {
                string icono = ObtenerIconoPorNombre(item.Nombre);
                html += $@"<a href='{item.Ruta}' class='menu-link'>
                       <span class='material-symbols-outlined'>{icono}</span>
                       <span>{item.Nombre}</span>
                   </a>";

                foreach (var hijo in menus.Where(m => m.IdPadre == item.Id))
                {
                    string iconoHijo = ObtenerIconoPorNombre(hijo.Nombre);
                    html += $@"<a href='{hijo.Ruta}' class='menu-link menu-hijo'>
                           <span class='material-symbols-outlined'>{iconoHijo}</span>
                           <span>{hijo.Nombre}</span>
                       </a>";
                }
            }

            MenuRol.InnerHtml = html;
        }

        private string ObtenerIconoPorNombre(string nombre)
        {
            switch (nombre)
            {
                case "Inicio / Bienvenida": return "home";
                case "Centros Comerciales": return "store";
                case "Usuarios": return "group";
                case "Mi Perfil": return "person";
                case "Menu Administrador": return "admin_panel_settings";
                case "Menu AdministradorCC": return "business";
                case "Menu Local": return "storefront";
                case "Menu Cajero": return "point_of_sale";
                case "Index": return "home";
                case "Locales": return "storefront";
                case "PerfilCC": return "business";
                case "PerfiPlazoleta": return "store";
                default: return "circle";
            }
        }

        protected void lbCerrar_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Vista/Auth/Login.aspx");
        }
    }
}