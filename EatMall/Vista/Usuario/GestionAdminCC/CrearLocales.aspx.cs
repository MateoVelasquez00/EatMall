using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EatMall.Logica;
using EatMall.Modelo;
using LocalM = EatMall.Modelo.Local;

namespace EatMall.Vista.Usuario.GestionAdminCC
{
    public partial class CrearLocales : System.Web.UI.Page
    {
        LocalL localL = new LocalL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPlazoletas();
                CargarDueños();
            }
        }

        private void CargarPlazoletas()
        {
            UsuarioLogin usuario = (UsuarioLogin)Session["Usuario"];
            int idCC = usuario.IdCC;

            PlazoletaL plazoletaL = new PlazoletaL();
            ddlPlazoleta.DataSource = plazoletaL.MtListarPlazoletas(idCC);
            ddlPlazoleta.DataTextField = "Nombre";
            ddlPlazoleta.DataValueField = "Id";
            ddlPlazoleta.DataBind();
            ddlPlazoleta.Items.Insert(0, new ListItem("-- Selecciona --", "0"));
        }

        private void CargarDueños()
        {
            ClienteL clienteL = new ClienteL();
            ddlDueño.DataSource = clienteL.MtListarTodosUsuario();
            ddlDueño.DataTextField = "Nombre";
            ddlDueño.DataValueField = "Id";
            ddlDueño.DataBind();
            ddlDueño.Items.Insert(0, new ListItem("-- Selecciona --", "0"));
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtNumeroLocal.Text) ||
                ddlPlazoleta.SelectedValue == "0" ||
                ddlDueño.SelectedValue == "0")
            {
                MostrarAlerta("Por favor completa todos los campos obligatorios.", "warning");
                return;
            }

            try
            {
                LocalM nuevoLocal = new LocalM()
                {
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Imagen = txtImagen.Text.Trim(),
                    Estado = ddlEstado.SelectedValue,
                    NumeroLocal = Convert.ToInt32(txtNumeroLocal.Text.Trim()),
                    IdPlazoleta = Convert.ToInt32(ddlPlazoleta.SelectedValue),
                    IdDueñoLocal = Convert.ToInt32(ddlDueño.SelectedValue)
                };

                localL.MtCrearLocal(nuevoLocal);

                string script = @"Swal.fire({
                    title: 'Local creado!',
                    text: 'El local se registro correctamente.',
                    icon: 'success',
                    confirmButtonColor: '#006948'
                }).then(() => {
                    window.location.href = 'Locales.aspx';
                });";

                ScriptManager.RegisterStartupScript(this, GetType(), "exito", script, true);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.Contains("UNIQUE KEY") || ex.Message.Contains("duplicate")
                    ? "El numero de local ya esta registrado."
                    : "Error al crear el local. Intenta de nuevo.";

                MostrarAlerta(mensaje, "warning");
            }
        }

        private void MostrarAlerta(string mensaje, string icono)
        {
            mensaje = mensaje.Replace("'", "").Replace("\"", "").Replace("\n", " ")
                             .Replace("á", "a").Replace("é", "e").Replace("í", "i")
                             .Replace("ó", "o").Replace("ú", "u").Replace("ñ", "n")
                             .Replace("Á", "A").Replace("É", "E").Replace("Í", "I")
                             .Replace("Ó", "O").Replace("Ú", "U").Replace("Ñ", "N");

            string script = $"Swal.fire({{ title: 'Atencion', text: '{mensaje}', icon: '{icono}', confirmButtonColor: '#006948' }});";
            ScriptManager.RegisterStartupScript(this, GetType(), "alerta", script, true);
        }
    }
}