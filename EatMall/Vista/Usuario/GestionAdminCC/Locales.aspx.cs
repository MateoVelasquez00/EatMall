using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EatMall.Vista.Usuario.GestionAdminCC
{
    public partial class Locales : System.Web.UI.Page
    {
        LocalL localL = new LocalL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["estado"] != null)
                {
                    int idLocal = Convert.ToInt32(Request.QueryString["estado"]);
                    string nuevoEstado = Request.QueryString["valor"];

                    localL.MtCambiarEstadoLocal(idLocal, nuevoEstado);

                    Response.Redirect("Locales.aspx");
                }

                CargarLocales();
            }
        }

        private void CargarLocales()
        {
            UsuarioLogin usuario = (UsuarioLogin)Session["Usuario"];
            gvLocales.DataSource = localL.MtListarTodosLocales(usuario.IdCC);
            gvLocales.DataBind();
        }
        protected void gvLocales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // lógica futura aquí
        }

    }

}