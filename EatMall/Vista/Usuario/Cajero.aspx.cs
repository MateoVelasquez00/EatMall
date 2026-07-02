using System;
using System.Collections.Generic;
using System.Web.UI;
using EatMall.Logica;
using EatMall.Modelo;

namespace EatMall.Vista.Usuario
{
	public partial class Cajero : System.Web.UI.Page
	{
		PedidoL opedidoL = new PedidoL();
		DetallePedidoL odetallepedidoL = new DetallePedidoL();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["Cajero"] == null)
			{
				Response.Redirect("~/Vista/Auth/Login.aspx");
				return;
			}

			if (!IsPostBack)
				CargarPedidos();
		}

		private void CargarPedidos()
		{
			EatMall.Modelo.Cajero cajero = (EatMall.Modelo.Cajero)Session["Cajero"];

			List<EatMall.Modelo.Pedido> pedidos = opedidoL.ListarPedidosPorLocal(cajero.IdLocal);

			List<EatMall.Modelo.Pedido> pedidosHoy = pedidos.FindAll(p =>
				p.FechaPedido.Date == DateTime.Today);

			foreach (var pedido in pedidosHoy)
			{
				List<DetallePedido> detallesDelPedido = odetallepedidoL.MtObtenerDetalles(pedido.Id);

				decimal totalLocal = 0;
				string estadoDelLocal = "Pendiente";

				foreach (var item in detallesDelPedido)
				{
					if (item.NombreLocal.Trim().ToUpper() == cajero.NombreLocal.Trim().ToUpper())
					{
						totalLocal += item.Subtotal;


						estadoDelLocal = item.EstadoProducto;
					}
				}


				pedido.Total = totalLocal;
				pedido.Estado = estadoDelLocal;
			}

			gvPedidos.DataSource = pedidosHoy;
			gvPedidos.DataBind();
		}

		protected void gvPedidos_SelectedIndexChanged(object sender, EventArgs e)
		{
			EatMall.Modelo.Cajero cajero = (EatMall.Modelo.Cajero)Session["Cajero"];

			int idPedido = Convert.ToInt32(gvPedidos.SelectedDataKey?.Value
				?? gvPedidos.SelectedRow.Cells[0].Text);

			string codigo = gvPedidos.SelectedRow.Cells[0].Text;

			List<DetallePedido> detalle = odetallepedidoL.ObtenerDetallePedido(idPedido, cajero.IdLocal);

			decimal montoTotal = 0;
			string estadoProductoActual = "";

			foreach (var item in detalle)
			{
				montoTotal += item.Subtotal;
				estadoProductoActual = item.EstadoProducto.Trim().ToUpper();
			}

			lblCodigoPedido.Text = codigo;
			hfIdPedido.Value = idPedido.ToString();
			lblMontoTotal.Text = montoTotal.ToString("C");

			rptDetalle.DataSource = detalle;
			rptDetalle.DataBind();

			rptResumenPrecios.DataSource = detalle;
			rptResumenPrecios.DataBind();

			if (estadoProductoActual == "ENTREGADO")
			{
				btnEnPreparacion.Enabled = false;
				btnEnPreparacion.CssClass = "btn btn-secondary fw-bold text-white";

				btnEntregado.Enabled = false;
				btnEntregado.CssClass = "btn btn-secondary fw-bold text-white";
			}
			else if (estadoProductoActual == "EN PREPARACIÓN" || estadoProductoActual == "EN PREPARACION")
			{
				btnEnPreparacion.Enabled = false;
				btnEnPreparacion.CssClass = "btn btn-secondary fw-bold text-white";

				btnEntregado.Enabled = true;
				btnEntregado.CssClass = "btn btn-success fw-bold";
			}
			else
			{
				btnEnPreparacion.Enabled = true;
				btnEnPreparacion.CssClass = "btn btn-warning fw-bold text-white";

				btnEntregado.Enabled = true;
				btnEntregado.CssClass = "btn btn-success fw-bold";
			}

			Transaccion transaccion = opedidoL.ObtenerTransaccionPorPedido(idPedido);
			if (transaccion != null)
			{
				lblPayuRef.Text = transaccion.PayuCodigoReferencia;
				lblFechaTrans.Text = transaccion.FechaTransaccion.ToString("dd/MM/yyyy hh:mm tt");
				lblEstadoPago.Text = transaccion.Estado;
				lblMedioPago.Text = "PayU (Online)";

				string estadoUpper = transaccion.Estado.ToUpper();
				if (estadoUpper == "APROBADA" || estadoUpper == "CAPTURADA" || estadoUpper == "APPROVED")
				{
					lblEstadoPago.CssClass = "badge bg-success text-white fw-bold";
				}
				else if (estadoUpper == "RECHAZADA" || estadoUpper == "FALLIDA" || estadoUpper == "DECLINED")
				{
					lblEstadoPago.CssClass = "badge bg-danger text-white fw-bold";
				}
				else
				{
					lblEstadoPago.CssClass = "badge bg-warning text-dark fw-bold";
				}
			}
			else
			{
				lblPayuRef.Text = "N/A";
				lblFechaTrans.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
				lblEstadoPago.Text = "Pendiente";
				lblEstadoPago.CssClass = "badge bg-warning text-dark fw-bold";
				lblMedioPago.Text = "Efectivo";
			}

			lblMontoTotal.Text = montoTotal.ToString("C");

			pnlPedidosLista.Visible = false;
			pnlDetalle.Visible = true;
		}

		protected void btnVolver_Click(object sender, EventArgs e)
		{
			pnlPedidosLista.Visible = true;
			pnlDetalle.Visible = false;
			CargarPedidos();
		}

		protected void btnEnPreparacion_Click(object sender, EventArgs e)
		{
			int idPedido = Convert.ToInt32(hfIdPedido.Value);
			EatMall.Modelo.Cajero cajero = (EatMall.Modelo.Cajero)Session["Cajero"];

			odetallepedidoL.CambiarEstadoProductoPorLocal(idPedido, cajero.IdLocal, "En preparación");


			opedidoL.CambiarEstadoPedido(idPedido, "En preparación");

			pnlPedidosLista.Visible = true;
			pnlDetalle.Visible = false;
			CargarPedidos();
		}

		protected void btnEntregado_Click(object sender, EventArgs e)
		{
			int idPedido = Convert.ToInt32(hfIdPedido.Value);
			EatMall.Modelo.Cajero cajero = (EatMall.Modelo.Cajero)Session["Cajero"];

			odetallepedidoL.CambiarEstadoProductoPorLocal(idPedido, cajero.IdLocal, "Entregado");

			int pendientes = odetallepedidoL.ContarProductosPendientes(idPedido);

			if (pendientes == 0)
			{
				opedidoL.CambiarEstadoPedido(idPedido, "Entregado");
			}

			pnlPedidosLista.Visible = true;
			pnlDetalle.Visible = false;
			CargarPedidos();
		}
	}
}