using EatMall.Logica;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EatMall.Modelo;
using System.Web.Script.Serialization;

namespace EatMall.Vista
{
	public partial class Site : System.Web.UI.MasterPage
	{
		private CarritoL carritoL = new CarritoL();

		protected void Page_Load(object sender, EventArgs e)
		{
			ActualizarBadgeCarrito();

			if (Session["Usuario"] != null)
			{
				UsuarioLogin oUsuario = (UsuarioLogin)Session["Usuario"];

				pnlLogin.Visible = false;
				pnlRegistro.Visible = false; 
				pnlBuscador.Visible = true;
				liPerfil.Visible = true;
				lblNombreUsuario.Text = oUsuario.Nombre;
				
			}
			else
			{
				pnlLogin.Visible = true;
				pnlRegistro.Visible = true;
				pnlBuscador.Visible = true;
				liPerfil.Visible = false;
			}
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			string busqueda = txtBusqueda.Text.Trim();
			if (!string.IsNullOrEmpty(busqueda))
			{
				Response.Redirect("/Vista/Busqueda/Resultados.aspx?q=" + Server.UrlEncode(busqueda));
			}
			else
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(), "warning",
					"Swal.fire({ icon: 'warning', title: '¿Qué buscas?', text: 'Ingresa el nombre de un restaurante o centro comercial.' });", true);
			}
		}

		private void ActualizarBadgeCarrito()
		{
			int cantidad = carritoL.ObtenerCantidadTotal();
			lblCantidadCarrito.Text = cantidad.ToString();
		}

		protected void btnCerrarSesion_Click(object sender, EventArgs e)
		{
			Session.Clear();
			Session.Abandon(); 
			Response.Redirect("/Index.aspx");
		}
        
    }
}