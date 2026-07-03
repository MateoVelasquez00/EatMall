using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Web.UI;

namespace EatMall.Vista.Usuario.GestionAdmin
{
    public partial class CrearCentroComercial : System.Web.UI.Page
    {
        CentroComercialL logica = new CentroComercialL();
        CiudadL ciudadL = new CiudadL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCiudad.DataSource = ciudadL.MtListarCiudades();
                ddlCiudad.DataTextField = "NombreCiudad";
                ddlCiudad.DataValueField = "Id";
                ddlCiudad.DataBind();
                ddlCiudad.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona una ciudad", "0"));

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    CargarDatos(id);
                    btnGuardar.Text = "Actualizar Centro Comercial";
                    lblTituloPagina.InnerHtml = "<span class='material-symbols-outlined'>edit</span> Editar Centro Comercial";
                }
            }
        }

        private void CargarDatos(int id)
        {
            CentroComercial cc = logica.MtObtenerCentroComercialPorId(id);

            if (cc != null)
            {
                txtNombre.Text = cc.Nombre;
                txtDireccion.Text = cc.Ubicacion;
                txtDescripcion.Text = cc.Descripcion;
                txtUbicacionUrl.Text = cc.UbicacionUrl;
                txtLatitud.Text = cc.Latitud.ToString();
                txtLongitud.Text = cc.Longitud.ToString();
                txtImagen.Text = cc.Imagen;

                ddlCiudad.SelectedValue = cc.Ciudad.Id.ToString();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MostrarAlerta("El nombre y la direccion son obligatorios.", "warning");
                return;
            }

            bool esEditar = !string.IsNullOrEmpty(Request.QueryString["id"]);

            if (ddlCiudad.SelectedValue == "0")
            {
                MostrarAlerta("Debes seleccionar una ciudad.", "warning");
                return;
            }

            CentroComercial cc = new CentroComercial()
            {
                Nombre = txtNombre.Text.Trim(),
                Ubicacion = txtDireccion.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                UbicacionUrl = txtUbicacionUrl.Text.Trim(),
                Imagen = txtImagen.Text.Trim(),
                Latitud = string.IsNullOrEmpty(txtLatitud.Text) ? 0 : Convert.ToDecimal(txtLatitud.Text),
                Longitud = string.IsNullOrEmpty(txtLongitud.Text) ? 0 : Convert.ToDecimal(txtLongitud.Text),
                Ciudad = new Ciudad() { Id = Convert.ToInt32(ddlCiudad.SelectedValue) }
            };


            bool resultado;
            string mensajeExito;

            if (esEditar)
            {
                cc.Id = Convert.ToInt32(Request.QueryString["id"]);
                resultado = logica.MtActualizarCentroComercial(cc);
                mensajeExito = "Centro comercial actualizado correctamente.";
            }
            else
            {
                resultado = logica.MtCrearCentroComercial(cc);
                mensajeExito = "Centro comercial creado correctamente.";
            }

            if (resultado)
            {
                string script = $@"Swal.fire({{
                    title: 'Listo!',
                    text: '{mensajeExito}',
                    icon: 'success',
                    confirmButtonColor: '#006948'
                }}).then(() => {{
                    window.location.href = 'ListarCentroComercial.aspx';
                }});";

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