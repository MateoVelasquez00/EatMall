using EatMall.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EatMall.Vista.Busqueda
{
    public partial class Resultados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string termino = Request.QueryString["q"];

                if (!string.IsNullOrEmpty(termino))
                {
                    BusquedaL oBusquedaL = new BusquedaL();

                    rptLocales.DataSource = oBusquedaL.MtBuscarLocales(termino);
                    rptLocales.DataBind();

                    rptProductos.DataSource = oBusquedaL.MtBuscarProductos(termino);
                    rptProductos.DataBind();

                    rptCiudad.DataSource = oBusquedaL.MtBuscarCentroComercialPorCiudad(termino);
                    rptCiudad.DataBind();

                    rptCentros.DataSource = oBusquedaL.MtBuscarCentroComercialPorNombre(termino);
                    rptCentros.DataBind();
                }
                if (rptLocales.Items.Count == 0 && rptProductos.Items.Count == 0 && rptCiudad.Items.Count == 0 && rptCentros.Items.Count == 0)
                {
                    lblMensaje.Text = $"No se encontraron resultados para: <strong>{termino}</strong>";
                    lblMensaje.Visible = true;
                }
            }
            
        }
    }
}