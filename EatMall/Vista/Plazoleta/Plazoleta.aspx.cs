using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace EatMall.Vista
{
    public partial class Plazoletas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnVolverIndex.NavigateUrl = "~/Index.aspx";

                int idCC = 0;

                if (Request.QueryString["id"] != null)
                {
                    int.TryParse(Request.QueryString["id"], out idCC);
                }

                PlazoletaL logica = new PlazoletaL();
                List<Plazoleta> lista = logica.MtListarPlazoletas(idCC);
                rptPlazoletas.DataSource = lista;
                rptPlazoletas.DataBind();

                if (lista.Count == 0)
                {
                    lblMensaje.Text = "No hay plazoletas disponibles para este centro comercial.";
                }
            }
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idPlazoleta = btn.CommandArgument;
            string idCC = Request.QueryString["id"];
            Response.Redirect("~/Vista/Local/Local.aspx?idPlazoleta=" +
                idPlazoleta + "&idCC=" + idCC);

        }
    }
}