using EatMall.Logica;
using System;
using System.Web.UI;

namespace EatMall.Vista.Usuario.GestionAdmin
{
    public partial class ListarCentroComercial : System.Web.UI.Page
    {
        CentroComercialL logica = new CentroComercialL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["estado"] != null)
                {
                    int idCC = Convert.ToInt32(Request.QueryString["estado"]);
                    bool nuevoEstado = Convert.ToBoolean(Request.QueryString["valor"]);

                    logica.MtCambiarEstadoCentroComercial(idCC, nuevoEstado);

                    Response.Redirect("ListarCentroComercial.aspx");
                }

                CargarCentrosComerciales();
            }
        }

        private void CargarCentrosComerciales()
        {
            gvCentroComercial.DataSource = logica.MtListarCentroComercialAdmin();
            gvCentroComercial.DataBind();
        }
    }
}