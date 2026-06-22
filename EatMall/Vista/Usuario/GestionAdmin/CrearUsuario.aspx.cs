using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Web.UI;

namespace EatMall.Vista.Usuario.GestionAdmin
{
    public partial class CrearUsuario : System.Web.UI.Page
    {
        ClienteL datos = new ClienteL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRolesDisponibles();
            }
        }

        private void CargarRolesDisponibles()
        {
            chkRoles.DataSource = datos.MtObtenerTodosLosRoles();
            chkRoles.DataTextField = "Nombre";
            chkRoles.DataValueField = "Id";
            chkRoles.DataBind();
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtDocumento.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MostrarAlerta("Por favor completa todos los campos obligatorios.", "warning");
                return;
            }

            // Validación: debe tener al menos un rol marcado
            bool tieneAlgunRol = false;
            foreach (System.Web.UI.WebControls.ListItem item in chkRoles.Items)
            {
                if (item.Selected) { tieneAlgunRol = true; break; }
            }

            if (!tieneAlgunRol)
            {
                MostrarAlerta("Debes asignar al menos un rol al usuario.", "warning");
                return;
            }

            try
            {
                Cliente nuevoUsuario = new Cliente()
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Documento = txtDocumento.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Contraseña = txtPassword.Text
                };

                // Crea el usuario y recupera el Id generado para asignarle los roles
                int idUsuario = datos.MtCrearUsuario(nuevoUsuario);

                // Asigna cada rol que el admin marcó
                foreach (System.Web.UI.WebControls.ListItem item in chkRoles.Items)
                {
                    if (item.Selected)
                    {
                        int idRol = Convert.ToInt32(item.Value);
                        datos.MtCambiarRol(idUsuario, idRol, true);
                    }
                }

                string script = @"Swal.fire({
                    title: 'Usuario creado!',
                    text: 'El usuario se registro correctamente.',
                    icon: 'success',
                    confirmButtonColor: '#FFA94D'
                }).then(() => {
                    window.location.href = 'GestionUsuarios.aspx';
                });";

                ScriptManager.RegisterStartupScript(this, GetType(), "exito", script, true);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.Contains("UNIQUE KEY") || ex.Message.Contains("duplicate")
                  ? "El correo electronico ya esta registrado en el sistema."
                  : "Error al crear el usuario. Intenta de nuevo.";

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

            string script = $"Swal.fire({{ title: 'Atencion', text: '{mensaje}', icon: '{icono}', confirmButtonColor: '#FFA94D' }});";
            ScriptManager.RegisterStartupScript(this, GetType(), "alerta", script, true);
        }
    }
}