using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EatMall.Vista.Usuario.GestionLocal
{
    public partial class EditarLocal : System.Web.UI.Page
    {
        LocalL logica = new LocalL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UsuarioLogin usuario = Session["Usuario"] as UsuarioLogin;

                if (usuario == null)
                {
                    Response.Redirect("~/Vista/Auth/Login.aspx");
                    return;
                }

                CargarDatos(usuario.IdLocal);
            }
        }

        private void CargarDatos(int idLocal)
        {
            EatMall.Modelo.Local local = logica.ObtenerLocalPorId(idLocal);

            if (local == null)
            {
                Response.Redirect("MiLocal.aspx");
                return;
            }

            txtNombre.Text = local.Nombre;
            txtTelefono.Text = local.Telefono;
            txtEmail.Text = local.Email;
            txtDescripcion.Text = local.Descripcion;
            txtNumeroLocal.Text = local.NumeroLocal.ToString();
            txtImagen.Text = local.Imagen;
            ddlEstado.SelectedValue = local.Estado;

            if (!string.IsNullOrEmpty(local.Imagen))
            {
                imgPreview.ImageUrl = local.Imagen;
                imgPreview.Style["display"] = "block";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarAlerta("El nombre del local es obligatorio.", "warning");
                return;
            }

            UsuarioLogin usuario = Session["Usuario"] as UsuarioLogin;

            if (usuario == null)
            {
                Response.Redirect("~/Vista/Auth/Login.aspx");
                return;
            }

            EatMall.Modelo.Local local = new EatMall.Modelo.Local()
            {
                Id = usuario.IdLocal,
                Nombre = txtNombre.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                Imagen = txtImagen.Text.Trim(),
                Estado = ddlEstado.SelectedValue,
                NumeroLocal = string.IsNullOrEmpty(txtNumeroLocal.Text)
                              ? 0
                              : Convert.ToInt32(txtNumeroLocal.Text)
            };

            bool resultado = logica.MtActualizarLocal(local);

            if (resultado)
            {
                string script = @"Swal.fire({
                    title: 'Listo!',
                    text: 'Local actualizado correctamente.',
                    icon: 'success',
                    confirmButtonColor: '#006948'
                }).then(() => {
                    window.location.href = 'MiLocal.aspx';
                });";

                ScriptManager.RegisterStartupScript(this, GetType(), "exito", script, true);
            }
            else
            {
                MostrarAlerta("Error al guardar. Intenta de nuevo.", "error");
            }
        }

        private void MostrarAlerta(string mensaje, string icono)
        {
            string script = $"Swal.fire({{ title: 'Atencion', text: '{mensaje}', icon: '{icono}', confirmButtonColor: '#006948' }});";
            ScriptManager.RegisterStartupScript(this, GetType(), "alerta", script, true);
        }
    }
}

