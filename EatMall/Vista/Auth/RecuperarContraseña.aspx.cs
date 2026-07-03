using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EatMall.Logica;
using EatMall.Modelo;

namespace EatMall.Vista.Auth
{
	public partial class RecuperarContraseña : System.Web.UI.Page
	{
		ClienteL clienteL = new ClienteL();
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnIngresar_Click(object sender, EventArgs e)
		{

			lblMensaje.Text = "";
			lblMensaje.Visible = false;

			string email = txtEmail.Text.Trim();
			string password = txtNuevaPass.Text.Trim();
			string confirmarPass = txtConfirmarPass.Text.Trim();

			if (string.IsNullOrWhiteSpace(email))
			{
				lblMensaje.Text = "El campo de Correo es obligatorio";
				lblMensaje.Visible = true;
				return;
			}
			if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmarPass))
			{
				lblMensaje.Text = "El campo de confirmar contraseña es obligatorio";
				lblMensaje.Visible = true;
				return;
			}
			if (password != confirmarPass)
			{
				lblMensaje.Text = "Las contraseñas no coinciden, por favor verifica que las contraseñas sean iguales";
				lblMensaje.Visible = true;
				return;
			}

			try
			{
				Cliente oCliente = new Cliente()
				{
					Email = email,
					Contraseña = password
				};

				bool actualizado = clienteL.MtActualizarContraseña(oCliente);

				if (actualizado)
				{
					string script = "Swal.fire('¡Contraseña Actualizada!', 'Tu contraseña ha sido restablecida correctamente.', 'success').then(() => { window.location.href = 'Login.aspx'; });";
					ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSuccess", script, true);
				}
				else
				{
					MostrarAlertaSweet("Error de Validación", "El correo ingresado no se encuentra registrado en el sistema.", "error");
				}
			}
			catch (Exception ex)
			{
				MostrarAlertaSweet("Validación de contraseña", ex.Message, "error");
			}
		}

		protected void btnVolver_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Vista/Auth/Login.aspx");
		}

		private void MostrarAlertaSweet(string titulo, string mensaje, string tipo)
		{
			string script = $"Swal.fire('{titulo}', '{mensaje}', '{tipo}');";
			ScriptManager.RegisterStartupScript(this, this.GetType(), "sweetAlert", script, true);
		}
	}
}