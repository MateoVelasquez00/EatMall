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
                    if (!oUser.Estado)
                    {
                        lblMensaje.Text = "Tu cuenta ha sido desactivada. Contacta al administrador.";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    Session["Usuario"] = oUser;
                    Response.Redirect(oUser.UrlInicio);
                }
                else
                {
                    lblMensaje.Text = "Correo o contraseña incorrectos.";
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