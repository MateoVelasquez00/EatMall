using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Collections.Generic;

namespace EatMall.Vista.Pago
{
    public partial class ConfirmacionPago : System.Web.UI.Page
    {
        TransaccionL logica = new TransaccionL();
        CarritoL carritoL = new CarritoL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] == null)
                {
                    Response.Redirect("~/Vista/Auth/Login.aspx");
                    return;
                }

                decimal total = carritoL.ObtenerTotal();

                if (total <= 0 && Session["Total"] != null)
                    total = Convert.ToDecimal(Session["Total"]);

                if (total <= 0)
                {
                    lblTotal.Text = "Error: El carrito reporta 0";
                    return;
                }

                CargarResumenPago(total);
            }
        }

        private void CargarResumenPago(decimal total)
        {
            try
            {
                if (Session["MetodoPago"] == null)
                {
                    Response.Redirect("~/Vista/Pago/MetodosPago.aspx");
                    return;
                }

                int idMetodo = Convert.ToInt32(Session["MetodoPago"]);
                int idLocal = Session["IdLocal"] != null ? (int)Session["IdLocal"] : 0;

                if (Session["HoraEntrega"] != null)
                {
                    lblHoraEntrega.Text = Session["HoraEntrega"].ToString();
                }
                else
                {
                    lblHoraEntrega.Text = "No especificada";
                }

                MetodoPagoL metodoL = new MetodoPagoL();

                // ← Tipos explícitos para evitar ambigüedad con la página MetodoPago.aspx
                List<EatMall.Modelo.MetodoPago> metodos = metodoL.ObtenerMetodos(idLocal);
                EatMall.Modelo.MetodoPago seleccionado = metodos.Find(m => m.Id == idMetodo);

                if (seleccionado != null)
                {
                    lblMetodo.Text = seleccionado.NombreMetodo;
                    lblTotal.Text = total.ToString("C");
                    Session["Total"] = total;
                }
                else
                {
                    Response.Redirect("~/Vista/Pago/MetodosPago.aspx");
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/Vista/Pago/MetodosPago.aspx");
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            decimal montoAPagar = carritoL.ObtenerTotal();
            if (montoAPagar <= 0 && Session["Total"] != null)
                montoAPagar = Convert.ToDecimal(Session["Total"]);

            Transaccion oTransaccion = new Transaccion()
            {
                IdMetodoPago = Convert.ToInt32(Session["MetodoPago"]),
                Monto = montoAPagar,
                Estado = "Aprobado",
                FechaTransaccion = DateTime.Now
            };

            int idPedido = Session["IdPedido"] != null ? Convert.ToInt32(Session["IdPedido"]) : 1;

            int idTransaccion = logica.ProcesarPago(oTransaccion, idPedido);

            if (idTransaccion > 0)
            {
                carritoL.VaciarCarritoDespuesDePedido();
                Session["IdTransaccion"] = idTransaccion;
                Response.Redirect("~/Vista/Pago/Recibo.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Vista/Pago/MetodosPago.aspx");
        }
    }
}
