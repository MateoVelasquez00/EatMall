using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EatMall.Vista.Usuario
{
    public partial class PerfilAdmin : System.Web.UI.Page
    {
        ClienteL logica = new ClienteL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Index.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarDatos();
                if (Request.QueryString["guardado"] == "1")
                lblMensaje.Text = "✅ Cambios guardados correctamente";
                lblMensaje.CssClass = "text-success fw-semibold d-block mb-3";
                
            }
        }

        private void CargarDatos()
        {
            UsuarioLogin usuario = (UsuarioLogin)Session["Usuario"];
            int idCliente = usuario.Id;
            Cliente oCliente = logica.ObtenerClientePorId(idCliente);

            if (oCliente != null)
            {
                lblNombreCompleto.Text = oCliente.Nombre + " " + oCliente.Apellido;
                lblEmail.Text = oCliente.Email;
                txtNombre.Text = oCliente.Nombre;
                txtApellido.Text = oCliente.Apellido;
                txtTelefono.Text = oCliente.Telefono;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            UsuarioLogin usuario = (UsuarioLogin)Session["Usuario"];
            int idCliente = usuario.Id;

            // Primero obtienes el cliente
            Cliente oCliente = logica.ObtenerClientePorId(idCliente);

            oCliente.Nombre = txtNombre.Text;
            oCliente.Apellido = txtApellido.Text;
            oCliente.Telefono = txtTelefono.Text;

            if (!string.IsNullOrEmpty(txtContrasena.Text))
            {
                oCliente.Contraseña = txtContrasena.Text;
            }

            bool resultado = logica.ActualizarCliente(oCliente);

            if (resultado)
            {
                // Actualiza el nombre en Session para que el sidebar lo refleje
                usuario.Nombre = oCliente.Nombre;
                Session["Usuario"] = usuario;

                Response.Redirect(Request.Url.AbsolutePath + "?guardado=1");
                return;

                lblMensaje.Text = "✅ Cambios guardados correctamente";
                lblMensaje.CssClass = "text-success fw-semibold d-block mb-3";
                CargarDatos();
            }
            else
            {
                lblMensaje.Text = "❌ Error al guardar los cambios";
                lblMensaje.CssClass = "text-danger fw-semibold d-block mb-3";
            }
        }
    }
}