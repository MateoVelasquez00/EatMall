using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace EatMall.Vista.Usuario.GestionLocal
{
    public partial class CrearProducto : System.Web.UI.Page
    {
        ProductoL productoL = new ProductoL();
        CategoriaLocalL categoriaL = new CategoriaLocalL();

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

                CargarCategorias(usuario.IdLocal);

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);

                    CargarDatos(id);

                    btnGuardar.Text = "Actualizar Producto";

                    lblTituloPagina.InnerHtml =
                        "<span class='material-symbols-outlined'>edit</span> Editar Producto";
                }
            }
        }

        private void CargarCategorias(int idLocal)
        {
            ddlCategoria.DataSource = categoriaL.MtListarCategoria(idLocal);
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();

            ddlCategoria.Items.Insert(0,
                new System.Web.UI.WebControls.ListItem(
                    "Selecciona una categoría", "0"));
        }

        private void CargarDatos(int idProducto)
        {
            Producto producto = productoL.MtObtenerProductoPorId(idProducto);

            if (producto != null)
            {
                txtNombre.Text = producto.Nombre;
                txtDescripcion.Text = producto.Descripcion;
                txtPrecio.Text = producto.Precio.ToString(CultureInfo.InvariantCulture);
                txtImagen.Text = producto.Imagen;

                ddlCategoria.SelectedValue = producto.IdCategoria.ToString();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MostrarAlerta("Nombre y precio son obligatorios.", "warning");
                return;
            }

            if (ddlCategoria.SelectedValue == "0")
            {
                MostrarAlerta("Selecciona una categoría.", "warning");
                return;
            }

            UsuarioLogin usuario = Session["Usuario"] as UsuarioLogin;

            Producto producto = new Producto()
            {
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                Precio = Convert.ToDecimal(txtPrecio.Text),
                Imagen = txtImagen.Text.Trim(),
                IdCategoria = Convert.ToInt32(ddlCategoria.SelectedValue),
                Local = new EatMall.Modelo.Local()
                {
                    Id = usuario.IdLocal
                }
            };

            bool resultado;
            string mensaje;

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                producto.Id = Convert.ToInt32(Request.QueryString["id"]);

                resultado = productoL.MtActualizarProducto(producto);

                mensaje = "Producto actualizado correctamente.";
            }
            else
            {
                resultado = productoL.MtCrearProducto(producto);

                mensaje = "Producto creado correctamente.";
            }

            if (resultado)
            {
                string script = $@"
                Swal.fire({{
                    title:'¡Listo!',
                    text:'{mensaje}',
                    icon:'success',
                    confirmButtonColor:'#006948'
                }}).then(()=>{{
                    window.location='ProductosLocal.aspx';
                }});";

                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "ok",
                    script,
                    true);
            }
            else
            {
                MostrarAlerta("No fue posible guardar el producto.", "error");
            }
        }

        private void MostrarAlerta(string mensaje, string icono)
        {
            string script = $@"
            Swal.fire({{
                title:'Atención',
                text:'{mensaje}',
                icon:'{icono}',
                confirmButtonColor:'#006948'
            }});";

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "alerta",
                script,
                true);
        }
    }
}
