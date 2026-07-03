using System;
using System.Web.UI;
using EatMall.Logica;
using EatMall.Modelo;

namespace EatMall.Vista.Auth
{
	public partial class Registro : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e) { }

		protected void btnRegistrar_Click(object sender, EventArgs e)
		{

			lblMensajeError.Text = "";
			lblMensajeError.Visible = false;

			string password = txtPassword.Text.Trim();
			string confirmarPass = txtConfirmarPass.Text.Trim();

			if (string.IsNullOrWhiteSpace(confirmarPass))
			{
				lblMensajeError.Text = "El campo de confirmar contraseña es obligatorio";
				lblMensajeError.Visible = true;
				return;
			}
			if (password != confirmarPass)
			{
				lblMensajeError.Text = "Las contraseñas no coinciden, por favor verifica que las contraseñas sean iguales";
				lblMensajeError.Visible = true;
				return;
			}

			try
			{

				Cliente oClienteTem = new Cliente()
				{
					Documento = txtDocumento.Text,
					Nombre = txtNombre.Text,
					Apellido = txtApellido.Text,
					Email = txtEmail.Text,
					Telefono = txtTelefono.Text,
					Contraseña = txtPassword.Text,
					Estado = true
				};

				RegistroL oRegistro = new RegistroL();

				if (oRegistro.MtValidarDatosProspecto(oClienteTem))
				{
					string codigoOTP = new Random().Next(100000, 999999).ToString();

					CodigoVerificacionL oEmail = new CodigoVerificacionL();
					oEmail.MtEnviarCodigo(oClienteTem.Email, codigoOTP);

					Session["DatosRegistro"] = oClienteTem;
					Session["CodigoEsperado"] = codigoOTP;

					Response.Redirect("VerificarCuenta.aspx");
				}
			}
			catch (Exception ex)
			{
				lblMensajeError.Text = ex.Message;
				lblMensajeError.Visible = true;
			}
		}
		protected void btnVolver_Click(object sender, EventArgs e)
		{
			Response.Redirect("../../Index.aspx");
		}
	}
}