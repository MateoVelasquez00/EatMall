using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Web.UI;

namespace EatMall.Vista.Usuario.GestionLocal
{
    public partial class CrearCategoria : System.Web.UI.Page
    {
        CategoriaLocalL logica = new CategoriaLocalL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bool esEditar = !string.IsNullOrEmpty(Request.QueryString["id"]);

                if (esEditar)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    CargarDatos(id);

                    btnGuardar.Text = "Actualizar Categoria";
                    lblTituloPagina.InnerHtml =
                        "<span class='material-symbols-outlined'>edit</span> Editar Categoria";
                }
            }
        }
        private void CargarDatos(int id)
        {
            CategoriaProducto categoria = logica.MtObtenerCategoriaPorId(id);

            if (categoria != null)
            {
                txtNombre.Text = categoria.Nombre;
                txtImagen.Text = categoria.Imagen;

                ddlEstado.SelectedValue = categoria.Estado ? "true" : "false";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarAlerta("El nombre de la categoria es obligatorio.", "warning");
                return;
            }

            UsuarioLogin usuario = Session["Usuario"] as UsuarioLogin;

            if (usuario == null)
            {
                Response.Redirect("~/Vista/Auth/Login.aspx");
                return;
            }

            bool esEditar = !string.IsNullOrEmpty(Request.QueryString["id"]);

            bool estado = Convert.ToBoolean(ddlEstado.SelectedValue);

            CategoriaProducto categoria = new CategoriaProducto()
            {
                Nombre = txtNombre.Text.Trim(),
                Imagen = txtImagen.Text.Trim(),
                Estado = estado,
                IdLocal = usuario.IdLocal
            };

            bool resultado;
            string mensajeExito;

            if (esEditar)
            {
                categoria.Id = Convert.ToInt32(Request.QueryString["id"]);
                resultado = logica.MtActualizarCategoria(categoria);
                mensajeExito = "Categoria actualizada correctamente.";
            }
            else
            {
                resultado = logica.MtCrearCategoria(categoria);
                mensajeExito = "Categoria creada correctamente.";
            }

            if (resultado)
            {
                string script = $@"Swal.fire({{
            title: 'Listo!',
            text: '{mensajeExito}',
            icon: 'success',
            confirmButtonColor: '#006948').then(() => window.location.href = 'Categoria.aspx';);";

                ScriptManager.RegisterStartupScript(this, GetType(), "exito", script, true);
            }
            else
            {
                MostrarAlerta("Error al guardar la categoria.", "error");
            }
        }

        private void MostrarAlerta(string mensaje, string icono)
        {
            string script = $@"
            Swal.fire({{
                title: 'Atención',
                text: '{mensaje}',
                icon: '{icono}',
                confirmButtonColor: '#006948');";

            ScriptManager.RegisterStartupScript(this, GetType(), "alerta", script, true);
        }
    }
}