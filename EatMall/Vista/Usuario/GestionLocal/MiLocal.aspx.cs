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
    public partial class MiLocal : System.Web.UI.Page
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

                CargarDatosLocal(usuario.IdLocal);
            }
        }

        private void CargarDatosLocal(int idLocal)
        {
            Modelo.Local local = logica.ObtenerLocalPorId(idLocal);

            if (local == null)
            {
                Response.Redirect("~/Vista/Auth/Login.aspx");
                return;
            }

            imgLocal.ImageUrl = local.Imagen;
            lblNombre.Text = local.Nombre;
            lblEmail.Text = local.Email;
            lblTelefono.Text = local.Telefono;
            lblDescripcion.Text = local.Descripcion;
            lblHorario.Text = local.HorarioLocal;
            lblCalificacion.Text = local.Calificacion.Puntaje.ToString("0.0");
            lblNumeroLocal.Text = local.NumeroLocal.ToString();
            lblEstado.Text = local.Estado == "Abierto"
                ? "<span class='badge-estado badge-abierto'>Abierto</span>"
                : "<span class='badge-estado badge-cerrado'>Cerrado</span>";
        }
    }
}

