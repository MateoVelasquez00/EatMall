using System;

namespace EatMall.Vista.Pago
{
    public partial class Recibo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IdTransaccion"] == null)
                {
                    Response.Redirect("~/Vista/Pago/MetodosPago.aspx");
                    return;
                }

                lblIdTransaccion.Text = "#" + Session["IdTransaccion"].ToString();
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                lblEstado.Text = "Aprobado";
                lblTotal.Text = "$" + (Session["Total"] != null ? Session["Total"].ToString() : "0");
                lblMetodo.Text = Session["NombreMetodoPago"] != null ? Session["NombreMetodoPago"].ToString() : "";
            }
        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            var idCliente = Session["Usuario"];
            var nombreCliente = Session["NombreCliente"];

            // Limpiar solo los datos del pedido
            Session.Remove("Total");
            Session.Remove("MetodoPago");
            Session.Remove("NombreMetodoPago");
            Session.Remove("IdTransaccion");
            Session.Remove("CodigoPedido");
            Session.Remove("Carrito");
            Session.Remove("IdLocal");
            Session.Remove("IdPlazoleta");
            Session.Remove("IdCC");

            // Restaurar datos del usuario
            Session["Usuario"] = idCliente;
            Session["NombreCliente"] = nombreCliente;
            
            Response.Redirect("~/Index.aspx");
        }
    }
}