using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
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

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    CargarDatosUsuario(id);
                    btnCrear.Text = "Actualizar Usuario";
                    lblTituloPagina.InnerHtml = "<span class='material-symbols-outlined'>edit</span> Editar Usuario";
                }
                else
                {
                    btnCrear.Text = "Crear Usuario";
                }
            }
        }

        private void CargarDatosUsuario(int id)
        {
            Cliente oCliente = datos.ObtenerClientePorId(id);

            if (oCliente != null)
            {
                txtNombre.Text = oCliente.Nombre;
                txtApellido.Text = oCliente.Apellido;
                txtDocumento.Text = oCliente.Documento;
                txtEmail.Text = oCliente.Email;
                txtTelefono.Text = oCliente.Telefono;

                List<Rol> rolesUsuario = datos.MtObtenerRolesPorUsuario(id);
                foreach (System.Web.UI.WebControls.ListItem item in chkRoles.Items)
                {
                    item.Selected = rolesUsuario.Any(r => r.Id.ToString() == item.Value);
                }
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
            bool esEditar = !string.IsNullOrEmpty(Request.QueryString["id"]);

            // Contraseña obligatoria solo al crear
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtDocumento.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                (!esEditar && string.IsNullOrWhiteSpace(txtPassword.Text)))
            {
                MostrarAlerta("Por favor completa todos los campos obligatorios.", "warning");
                return;
            }

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

            if (esEditar)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                ActualizarUsuario(id);
            }
            else
            {
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

                    int idUsuario = datos.MtCrearUsuario(nuevoUsuario);

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
                        confirmButtonColor: '#006948'
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
        }

        private void ActualizarUsuario(int id)
        {
            try
            {
                Cliente oCliente = datos.ObtenerClientePorId(id);

                oCliente.Nombre = txtNombre.Text.Trim();
                oCliente.Apellido = txtApellido.Text.Trim();
                oCliente.Documento = txtDocumento.Text.Trim();
                oCliente.Email = txtEmail.Text.Trim();
                oCliente.Telefono = txtTelefono.Text.Trim();

                if (!string.IsNullOrEmpty(txtPassword.Text))
                    oCliente.Contraseña = txtPassword.Text;

                bool resultado = datos.ActualizarCliente(oCliente);

                if (resultado)
                {
                    List<Rol> rolesActuales = datos.MtObtenerRolesPorUsuario(id);

                    foreach (System.Web.UI.WebControls.ListItem item in chkRoles.Items)
                    {
                        int idRol = Convert.ToInt32(item.Value);
                        bool yaLoTiene = rolesActuales.Any(r => r.Id == idRol);

                        if (item.Selected && !yaLoTiene)
                            datos.MtCambiarRol(id, idRol, true);

                        if (!item.Selected && yaLoTiene)
                            datos.MtCambiarRol(id, idRol, false);
                    }

                    string script = @"Swal.fire({
                        title: 'Usuario actualizado!',
                        text: 'Los cambios se guardaron correctamente.',
                        icon: 'success',
                        confirmButtonColor: '#006948'
                    }).then(() => {
                        window.location.href = 'GestionUsuarios.aspx';
                    });";

                    ScriptManager.RegisterStartupScript(this, GetType(), "exito", script, true);
                }
                else
                {
                    MostrarAlerta("Error al actualizar el usuario. Intenta de nuevo.", "error");
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.Contains("UNIQUE KEY") || ex.Message.Contains("duplicate")
                    ? "El correo electronico ya esta registrado en el sistema."
                    : "Error al actualizar el usuario. Intenta de nuevo.";

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