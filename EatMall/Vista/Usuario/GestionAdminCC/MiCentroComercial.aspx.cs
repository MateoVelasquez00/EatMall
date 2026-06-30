using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EatMall.Logica;
using EatMall.Modelo;

namespace EatMall.Vista.Usuario.GestionAdminCC
{
    public partial class MiCentroComercial : System.Web.UI.Page
    {
        CentroComercialL ccL = new CentroComercialL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarDatosCC();
        }

        private void CargarDatosCC()
        {
            UsuarioLogin usuario = (UsuarioLogin)Session["Usuario"];
            CentroComercial cc = ccL.MtObtenerCCPorId(usuario.IdCC);

            if (cc == null) return;

            lblNombre.Text = cc.Nombre;
            lblDireccion.Text = cc.Ubicacion;
            lblEstadoBadge.Text = $"<span class='{(cc.Estado ? "badge-estado-cc badge-activo" : "badge-estado-cc badge-inactivo")}'>" +
                                  $"{(cc.Estado ? "Activo" : "Inactivo")}</span>";

            imgBanner.ImageUrl = cc.Imagen;
            txtDescripcion.Text = cc.Descripcion;
            txtImagen.Text = cc.Imagen;
            lblDescripcion.Text = cc.Descripcion;
            ddlEstado.SelectedValue = cc.Estado.ToString();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioLogin usuario = (UsuarioLogin)Session["Usuario"];

                CentroComercial cc = new CentroComercial()
                {
                    Id = usuario.IdCC,
                    Descripcion = txtDescripcion.Text.Trim(),
                    Imagen = txtImagen.Text.Trim(),
                    Estado = ddlEstado.SelectedValue == "True"
                };

                ccL.MtActualizarCC(cc);

                string script = @"Swal.fire({
                    title: 'Guardado!',
                    text: 'Los cambios se guardaron correctamente.',
                    icon: 'success',
                    confirmButtonColor: '#006948'
                }).then(() => { location.reload(); });";

                ScriptManager.RegisterStartupScript(this, GetType(), "ok", script, true);
            }
            catch (Exception)
            {
                string script = "Swal.fire({ title: 'Error', text: 'No se pudieron guardar los cambios.', icon: 'error', confirmButtonColor: '#006948' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "err", script, true);
            }
        }
    }
}