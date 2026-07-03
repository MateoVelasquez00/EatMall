using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EatMall.Logica;
using EatMall.Modelo;

namespace EatMall.Vista.Usuario.GestionLocal
{
    public partial class CajeroLocal : System.Web.UI.Page
    {
        CajeroL oCajeroL = new CajeroL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UsuarioLogin usuario = Session["Usuario"] as UsuarioLogin;
                if (usuario == null)
                {
                    Response.Redirect("~/Vista/Auth/Login.aspx");
                    return;
                }

                int IdDuenoLocal = usuario.Id;
                CargarCajeros(IdDuenoLocal);
            }
        }

        private void CargarCajeros(int IdDuenoLocal)
        {
            gvCajeros.DataSource = oCajeroL.ListarCajerosPorLocal(IdDuenoLocal);
            gvCajeros.DataBind();
        }
    }
}