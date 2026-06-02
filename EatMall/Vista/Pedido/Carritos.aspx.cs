using EatMall.Datos;
using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace EatMall.Vista.Pedido
{
	public partial class Carritos : System.Web.UI.Page
	{
		CarritoL carritoL = new CarritoL();
		PedidoL pedidoL = new PedidoL();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
				CargarCarrito();
			for (int i = 8; i <= 20; i++)
			{
				string hora = i.ToString("D2") + ":00";
				ddlHoraEntrega.Items.Add(new ListItem(hora, hora));
			}
		}

		private void CargarCarrito()
		{
			var items = carritoL.ObtenerCarrito();

			if (items.Count == 0)
			{
				pnlVacio.Visible = true;
				rptCarrito.Visible = false;
				btnConfirmar.Visible = false;
				lblTotal.Text = "0.00";
				lblSubtotal.Text = "0.00";
			}
			else
			{
				pnlVacio.Visible = false;
				rptCarrito.DataSource = items;
				rptCarrito.DataBind();
				lblTotal.Text = string.Format("{0:N2}", carritoL.ObtenerTotal());
				lblSubtotal.Text = string.Format("{0:N2}", carritoL.ObtenerTotal());
			}
		}

		protected void rptCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			if (e.CommandName == "Eliminar")
			{
				carritoL.EliminarProducto(Convert.ToInt32(e.CommandArgument));
				CargarCarrito();
			}
		}

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                
                string urlRetorno = Request.Url.PathAndQuery;
                Response.Redirect("~/Vista/Auth/Login.aspx");
                return;
            }
            
            UsuarioLogin oUser = (UsuarioLogin)Session["Usuario"];
            List<Carrito> items = carritoL.ObtenerCarrito();
            string horaSeleccionada = ddlHoraEntrega.SelectedValue;


			// Esto guarda en la BD y retorna el objeto pedido con el ID generado
			Modelo.Pedido pedido = pedidoL.ConfirmarPedido(items, oUser.Id, horaSeleccionada);

           
            Session["Total"] = carritoL.ObtenerTotal().ToString("N2");
            Session["IdPedido"] = pedido.Id;
            Session["CodigoPedido"] = pedido.CodigoPedido;
            Session["HoraEntrega"] = ddlHoraEntrega.SelectedValue;
           

			Response.Redirect("~/Vista/Pago/MetodosPago.aspx");
		}
	}
}