using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EatMall.Datos;
using EatMall.Logica;
using EatMall.Modelo;

namespace EatMall.Vista.Pedido
{
    public partial class RespuestaPago : System.Web.UI.Page
    {
		CarritoL oCarritoL = new CarritoL();
		protected void Page_Load(object sender, EventArgs e)
        {
			if (!IsPostBack)
			{
				string estadoPayU = Request.QueryString["transactionState"];
				string referencia = Request.QueryString["referenceCode"];
				string payuId = Request.QueryString["transactionId"];

				if (!string.IsNullOrEmpty(referencia))
				{
					string estadoFinal = "Desconocido";

					if (estadoPayU == "4")
					{
						estadoFinal = "Aprobado";
					}
					else if (estadoPayU == "6")
					{
						estadoFinal = "Rechazado";
					}
					else if (estadoPayU == "7")
					{
						estadoFinal = "Pendiente";
					}

					TransaccionL oTransaccionL = new TransaccionL();
					oTransaccionL.MtActualizarEstadoPago(referencia, estadoFinal, payuId);

					MapearResultadoPago(estadoFinal);
				}
				else
				{
					MapearResultadoPago("Desconocido");
				}
			}
		}

		private void MapearResultadoPago(string estado)
		{
			switch (estado)
			{
				case "Aprobado":
					//Exitosa
					litIcono.Text = "<span style='font-size: 70px; color: #28a745;'>✔️</span>";
					lblTituloEstado.Text = "¡Pago Aprobado!";
					lblMensajeDetalle.Text = "Tu transacción fue exitosa. En un momento iniciaremos la preparación de tu pedido en el Food Court de EatMall.";
					oCarritoL.VaciarCarritoDespuesDePedido();

					string scriptBorrarStorage = "localStorage.removeItem('carrito'); if(typeof ActualizarBadge === 'function') { ActualizarBadge(); }";
					ScriptManager.RegisterStartupScript(this, this.GetType(), "LimpiarCarritoExitoso", scriptBorrarStorage, true);
					break;

				case "Pendiente":
					//Pendiente
					litIcono.Text = "<span style='font-size: 70px; color: #ffc107;'>⏳</span>";
					lblTituloEstado.Text = "Pago en Validación";
					lblMensajeDetalle.Text = "Tu pago se encuentra en estado pendiente o en proceso de verificación por la pasarela de PayU. Te notificaremos pronto.";
					break;

				case "Rechazado":
					// Rechazada
					litIcono.Text = "<span style='font-size: 70px; color: #dc3545;'>❌</span>";
					lblTituloEstado.Text = "Pago Rechazado";
					lblMensajeDetalle.Text = "La transacción no pudo ser procesada por la pasarela de pagos. Por favor, verifica tus fondos o intenta con otro método.";
					break;

				default:
					//Error de Conexión o API
					litIcono.Text = "<span style='font-size: 70px; color: #6c757d;'>⚠️</span>";
					lblTituloEstado.Text = "Estado Desconocido";
					lblMensajeDetalle.Text = "Hubo un inconveniente al sincronizar tu pago con PayU, pero guardamos tu orden. Nuestro equipo la revisará de inmediato.";
					break;
			}
		}
	}
}