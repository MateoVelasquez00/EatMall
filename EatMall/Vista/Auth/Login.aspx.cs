using System;
using System.Web.UI;
using EatMall.Logica;
using EatMall.Modelo;

namespace EatMall.Vista.Auth
{
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void btnIngresar_Click(object sender, EventArgs e)
		{
			{
				UsuarioLogin oDatos = new UsuarioLogin()
				{
					Email = txtEmail.Text.Trim(),
					Contraseña = txtPassword.Text.Trim()
				};

				LoginL oLogin = new LoginL();
				UsuarioLogin oUser = oLogin.MtLogin(oDatos, chkTipo.Checked);

				if (oUser != null)
				{
					Session["Usuario"] = oUser;
					Response.Redirect(oUser.UrlInicio);
				}
				else
				{
					if (chkTipo.Checked)
						lblMensaje.Text = "No tienes permisos de administrador o datos incorrectos.";
					else
						lblMensaje.Text = "No tienes una cuenta de cliente asignada o datos incorrectos.";

					lblMensaje.ForeColor = System.Drawing.Color.Red;
				}
			}
		}

		protected void btnVolver_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Index.aspx");
		}
	}
}