using EatMall.Logica;
using EatMall.Modelo;
using System;

namespace EatMall.Vista.Usuario
{
    public partial class Perfil : System.Web.UI.Page
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

            var pedidos = logica.ObtenerPedidosPorCliente(idCliente);
            if (pedidos.Count > 0)
            {
                rptPedidos.DataSource = pedidos;
                rptPedidos.DataBind();
            }
            else
            {
                lblSinPedidos.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            UsuarioLogin usuario = (UsuarioLogin)Session["Usuario"];
            int idCliente = usuario.Id;
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
                Session["NombreCliente"] = oCliente.Nombre;
                lblMensaje.Text = "✅ Cambios guardados correctamente";
                CargarDatos();
            }
            else
            {
                lblMensaje.Text = "❌ Error al guardar los cambios";
                lblMensaje.CssClass = "text-danger fw-semibold";
            }
        }
    }
}