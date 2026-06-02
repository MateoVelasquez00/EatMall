using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace EatMall.Vista.Pago
{
    public partial class MetodosPago : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMetodos();
                CargarResumen();
            }
        }

        private void CargarMetodos()
        {
            int idLocal = Session["IdLocal"] != null ? (int)Session["IdLocal"] : 1;
            hfIdLocal.Value = idLocal.ToString();

            MetodoPagoL logica = new MetodoPagoL();
            List<EatMall.Modelo.MetodoPago> metodos = logica.ObtenerMetodos(idLocal);

            if (metodos.Count == 0)
                lblSinMetodos.Visible = true;
            else
            {
                rptMetodos.DataSource = metodos;
                rptMetodos.DataBind();
            }
        }

        private void CargarResumen()
        {
            CarritoL carritoL = new CarritoL();
            List<Carrito> items = carritoL.ObtenerCarrito();

            rptResumen.DataSource = items;
            rptResumen.DataBind();

            decimal total = carritoL.ObtenerTotal();

            // Si el carrito ya fue procesado, usar el total de Session
            if (total <= 0 && Session["Total"] != null)
            {
                total = Convert.ToDecimal(Session["Total"].ToString().Replace(",", "."));

                // EatMall no cobra envío por ahora
                decimal envio = 0;

                lblSubtotal.Text = "$" + total.ToString("F2");
                lblEnvio.Text = envio == 0 ? "Gratis" : "$" + envio.ToString("F2");
                lblTotal.Text = "$" + total.ToString("F2");
                lblHoraEntrega.Text = Session["HoraEntrega"]?.ToString() ?? "No seleccionada";
                lblDireccion.Text = Session["Direccion"]?.ToString() ?? "No especificada";
            }

            // Mostrar código de pedido si ya fue generado
            if (Session["CodigoPedido"] != null)
            {
                lblCodigoPedido.Text = "Pedido #" + Session["CodigoPedido"].ToString();
            }
        }

        protected void rptMetodos_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e) { }

        protected void btnServerContinuar_Click(object sender, EventArgs e)
        {
            Session["MetodoPago"] = hfMetodoId.Value;
            Session["MetodoPagoNombre"] = hfMetodoNombre.Value;
            Session["StripeToken"] = hfStripeToken.Value;

            Response.Redirect("~/Vista/Pago/ConfirmacionPago.aspx");
        }
    }
}