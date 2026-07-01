using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EatMall.Vista.Usuario.GestionAdminCC
{
    public partial class MiPlazoleta : System.Web.UI.Page
    {
        PlazoletaL plazoletaL = new PlazoletaL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarSelectorPlazoletas();
                if (ddlPlazoletaSelector.Items.Count > 0)
                    CargarDatosPlazoleta(Convert.ToInt32(ddlPlazoletaSelector.SelectedValue));
            }
        }

        private void CargarSelectorPlazoletas()
        {
            UsuarioLogin usuario = (UsuarioLogin)Session["Usuario"];

            ddlPlazoletaSelector.DataSource = plazoletaL.MtListarPlazoletas(usuario.IdCC);
            ddlPlazoletaSelector.DataTextField = "Nombre";
            ddlPlazoletaSelector.DataValueField = "Id";
            ddlPlazoletaSelector.DataBind();
        }

        protected void ddlPlazoletaSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatosPlazoleta(Convert.ToInt32(ddlPlazoletaSelector.SelectedValue));
        }

        private void CargarDatosPlazoleta(int idPlazoleta)
        {
            Plazoleta plazoleta = plazoletaL.MtObtenerPlazoletaPorId(idPlazoleta);

            if (plazoleta == null) return;

            lblNombre.Text = plazoleta.Nombre;
            lblDescripcion.Text = plazoleta.Descripcion;
            imgBanner.ImageUrl = plazoleta.Imagen;

            bool activo = plazoleta.Estado == "True";
            lblEstadoBadge.Text = $"<span class='{(activo ? "badge-estado-cc badge-activo" : "badge-estado-cc badge-inactivo")}'>" +
                                  $"{(activo ? "Activo" : "Inactivo")}</span>";

            txtNombre.Text = plazoleta.Nombre;
            txtDescripcion.Text = plazoleta.Descripcion;
            txtImagen.Text = plazoleta.Imagen;
            ddlEstado.SelectedValue = plazoleta.Estado;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int idPlazoleta = Convert.ToInt32(ddlPlazoletaSelector.SelectedValue);

                Plazoleta plazoleta = new Plazoleta()
                {
                    Id = idPlazoleta,
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    Imagen = txtImagen.Text.Trim(),
                    Estado = ddlEstado.SelectedValue
                };

                plazoletaL.MtActualizarPlazoleta(plazoleta);

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