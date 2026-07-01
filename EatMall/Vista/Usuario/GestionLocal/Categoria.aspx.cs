using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EatMall.Vista.Usuario.GestionLocal
{
    public partial class Categoria : System.Web.UI.Page
    {
        CategoriaLocalL logica = new CategoriaLocalL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UsuarioLogin usuario = (UsuarioLogin)Session["Usuario"];

                if (usuario == null)
                {
                    Response.Redirect("~/Vista/Auth/Login.aspx");
                    return;
                }

                if (Request.QueryString["estado"] != null)
                {
                    int idCategoria = Convert.ToInt32(Request.QueryString["estado"]);
                    bool nuevoEstado = Convert.ToBoolean(Request.QueryString["valor"]);

                    logica.MtCambiarEstadoCategoria(idCategoria, nuevoEstado);

                    Response.Redirect("Categoria.aspx");
                    return;
                }

                CargarCategorias(usuario.IdLocal);
            }
        }
        private void CargarCategorias(int idLocal)
        {
            gvCategoria.DataSource = logica.MtListarCategoria(idLocal);
            gvCategoria.DataBind();
        }
    }
}