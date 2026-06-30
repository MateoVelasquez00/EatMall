using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace EatMall.Vista.Pago
{
    public partial class Local : Page
    {
        LocalL localL = new LocalL();
        PromocionL promocionL = new PromocionL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["idCC"]))
                    Session["IdCC"] = Request.QueryString["idCC"];

                if (!string.IsNullOrEmpty(Request.QueryString["idPlazoleta"]))
                    Session["IdPlazoleta"] = Request.QueryString["idPlazoleta"];

                string idCC = Request.QueryString["idCC"] ?? Session["IdCC"]?.ToString();

                btnVolverPlazoleta.NavigateUrl = "~/Vista/Plazoleta/Plazoleta.aspx?id=" + idCC;

                if (!string.IsNullOrEmpty(idCC))
                {
                    CentroComercialL logicaCC = new CentroComercialL();
                    CentroComercial cc = logicaCC.MtObtenerCentroComercialPorId(Convert.ToInt32(idCC));

                    if (cc == null)
                    {
                        Response.Redirect("~/Index.aspx");
                        return;
                    }
                }

                CargarLocales();
            }
        }

        private void CargarLocales()
        {
            int idPlazoleta = 0;
            //Si la URL trae el idPlazoleta y no está vacío, guárdalo en la variable local y lo vuelve int
            if (!string.IsNullOrEmpty(Request.QueryString["idPlazoleta"]))
                int.TryParse(Request.QueryString["idPlazoleta"], out idPlazoleta);
            else if (Session["IdPlazoleta"] != null)
                int.TryParse(Session["IdPlazoleta"].ToString(), out idPlazoleta);

            List<Modelo.Local> lista = localL.MtListarLocales(idPlazoleta);

            if (lista.Count == 0)
            {
                lblMensaje.Text = "No hay locales disponibles en esta plazoleta.";
                lblMensaje.Visible = true;
            }

            rptLocales.DataSource = lista;
            rptLocales.DataBind();

            rptCarousel.DataSource = promocionL.MtListarPromocionesPorPlazoleta(idPlazoleta);
            rptCarousel.DataBind();
        }
    }
}