using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EatMall.Datos;
using EatMall.Logica;
using EatMall.Modelo;
using Newtonsoft.Json;

namespace EatMall.Vista.Pedido
{
	public partial class Carritos : System.Web.UI.Page
	{
		private PedidoL pedidoL = new PedidoL();
		private CarritoL carritoL = new CarritoL();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

				for (int i = 8; i <= 20; i++)
				{
					string hora = i.ToString("D2") + ":00";
					ddlHoraEntrega.Items.Add(new ListItem(hora, hora));
				}
			}
		}

		protected void btnConfirmar_Click(object sender, EventArgs e)
		{

			if (Session["Usuario"] == null)
			{
				Response.Redirect("~/Vista/Auth/Login.aspx");
				return;
			}

			UsuarioLogin oUsuarioLogin = (UsuarioLogin)Session["Usuario"];
			string horaSeleccionadaUser = ddlHoraEntrega.SelectedValue;
			decimal montoAPagar = 0;
			int idPedido = 0;
			string codigoPedido = "PED-" + DateTime.Now.ToString("yyyyMMddHHmmss");

			try
			{

				string jsonCarrito = CarritoData.Value;

				if (!string.IsNullOrEmpty(jsonCarrito) && jsonCarrito != "[]")
				{

					var items = JsonConvert.DeserializeObject<List<EatMall.Modelo.Carrito>>(jsonCarrito);

					if (items != null && items.Count > 0)
					{

						Modelo.Pedido pedido = pedidoL.ConfirmarPedido(items, oUsuarioLogin.Id, horaSeleccionadaUser);

						idPedido = pedido.Id;
						codigoPedido = pedido.CodigoPedido;

						foreach (var item in items)
						{
							montoAPagar += (item.Precio * item.Cantidad);
						}
					}
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Error al procesar el carrito de EatMall: " + ex.Message);
			}

			if (idPedido == 0)
			{
				montoAPagar = carritoL.ObtenerTotal();

				if (montoAPagar <= 0 && Session["Total"] != null)
				{
					montoAPagar = Convert.ToDecimal(Session["Total"]);
				}
			}

			Session["IdPedido"] = idPedido;
			Session["Total"] = montoAPagar.ToString("N2");
			

			if (montoAPagar > 0)
			{
				string fechaPedido = DateTime.Now.ToString("yyyyMMddHHmmss");
				string referenciaUnica = $"EATMALL-ORDEN-{idPedido}-{fechaPedido}";

				Transaccion oTransaccion = new Transaccion()
				{
					IdPedido = idPedido,
					IdMetodoPago = Session["MetodoPago"] != null ? Convert.ToInt32(Session["MetodoPago"]) : 1,
					Monto = montoAPagar,
					Estado = "Pendiente",
					FechaTransaccion = DateTime.Now,
					PayuCodigoReferencia = referenciaUnica
				};

				TransaccionL oTransaccionL = new TransaccionL();
				oTransaccionL.MtCrearPago(oTransaccion);

				string apiKey = ConfigurationManager.AppSettings["PayU_ApiKey"];
				string merchantId = ConfigurationManager.AppSettings["PayU_MerchantId"];
				string accountId = ConfigurationManager.AppSettings["PayU_AccountId"];
				string currency = "COP";
				string montoFormateado = Math.Round(oTransaccion.Monto, 0).ToString();
				string cadenaFirma = $"{apiKey}~{merchantId}~{referenciaUnica}~{montoFormateado}~{currency}";
				string firmaMD5 = GenerarMD5(cadenaFirma);

				string urlCheckout = "https://sandbox.checkout.payulatam.com/ppp-web-gateway-payu/";

				StringBuilder script = new StringBuilder();
				script.Append("var form = document.createElement('form');");
				script.Append("form.method = 'POST';");
				script.Append($"form.action = '{urlCheckout}';");

				// Campos obligatorios del formulario WebCheckout de PayU
				script.Append("function addInput(name, value) { var input = document.createElement('input'); input.type = 'hidden'; input.name = name; input.value = value; form.appendChild(input); }");
				script.Append($"addInput('merchantId', '{merchantId}');");
				script.Append($"addInput('accountId', '{accountId}');");
				script.Append($"addInput('description', 'Pedido EatMall - Entrega: {horaSeleccionadaUser}');");
				script.Append($"addInput('referenceCode', '{referenciaUnica}');");
				script.Append($"addInput('amount', '{montoFormateado}');");
				script.Append($"addInput('currency', '{currency}');");
				script.Append($"addInput('signature', '{firmaMD5}');");
				script.Append("addInput('test', '1');");
				script.Append($"addInput('buyerEmail', '{oUsuarioLogin.Email}');");
				script.Append("addInput('tax', '0');");
				script.Append("addInput('taxReturnBase', '0');");
				script.Append("addInput('responseUrl', 'https://localhost:44377/Vista/Pedido/RespuestaPago.aspx');");
				script.Append("document.body.appendChild(form);");
				script.Append("form.submit();");

				ScriptManager.RegisterStartupScript(this, this.GetType(), "PayURedirect", script.ToString(), true);
			}
		}

		private string GenerarMD5(string input)
		{
			using (MD5 md5 = MD5.Create())
			{
				byte[] inputBytes = Encoding.UTF8.GetBytes(input);
				byte[] hashBytes = md5.ComputeHash(inputBytes);
				StringBuilder sb = new StringBuilder();

				for (int i = 0; i < hashBytes.Length; i++)
				{
					sb.Append(hashBytes[i].ToString("x2"));
				}
				return sb.ToString();
			}
		}
	}
}
