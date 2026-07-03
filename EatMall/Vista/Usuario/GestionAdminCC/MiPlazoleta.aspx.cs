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
                CargarPlazoletas();
            }
        }

        private void CargarPlazoletas()
        {
            UsuarioLogin usuario = (UsuarioLogin)Session["Usuario"];

            List<Plazoleta> plazoletas = plazoletaL.MtListarPlazoletas(usuario.IdCC);

            rptPlazoletas.DataSource = plazoletas;
            rptPlazoletas.DataBind();

            if (plazoletas.Count == 0) return;

            int idPlazoleta;

            if (ViewState["IdPlazoletaActual"] != null)
                idPlazoleta = Convert.ToInt32(ViewState["IdPlazoletaActual"]);
            else
                idPlazoleta = plazoletas[0].Id;

            ViewState["IdPlazoletaActual"] = idPlazoleta;
            CargarDatosPlazoleta(idPlazoleta);
        }

        protected void rptPlazoletas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerPlazoleta")
            {
                int idPlazoleta = Convert.ToInt32(e.CommandArgument);
                ViewState["IdPlazoletaActual"] = idPlazoleta;
                CargarPlazoletas();
            }
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
                int idPlazoleta = Convert.ToInt32(ViewState["IdPlazoletaActual"]);

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