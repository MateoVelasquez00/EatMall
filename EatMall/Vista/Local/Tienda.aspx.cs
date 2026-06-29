using EatMall.Datos;
using EatMall.Logica;
using EatMall.Modelo;
using EatMall.Vista.Pago;
using System;
using System.Security.Cryptography;
using System.Web.UI.WebControls;

namespace EatMall.Vista.Local
{
    public partial class Tienda : System.Web.UI.Page
    {
        private ProductoL productoL = new ProductoL();
        private CarritoL carritoL = new CarritoL();
        private LocalL localL = new LocalL();
        private CategoriaProductoL categoriaL = new CategoriaProductoL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["idPlazoleta"]))
                    Session["IdPlazoleta"] = Request.QueryString["idPlazoleta"];

                if (!string.IsNullOrEmpty(Request.QueryString["idCC"]))
                    Session["IdCC"] = Request.QueryString["idCC"];

                string idPlazoleta = Request.QueryString["idPlazoleta"] ?? Session["IdPlazoleta"]?.ToString();
                string idCC = Request.QueryString["idCC"] ?? Session["IdCC"]?.ToString();

                btnVolverLocal.NavigateUrl =
                    "~/Vista/Local/Local.aspx?idPlazoleta=" + idPlazoleta + "&idCC=" + idCC;

                // Valida que el CC esté activo
                if (!string.IsNullOrEmpty(idCC))
                {
                    CentroComercialL logicaCC = new CentroComercialL();
                    CentroComercial cc = logicaCC.MtObtenerCentroComercialPorId(Convert.ToInt32(idCC));

                    if (cc == null)
                    {
                        Response.Redirect("~/Index.aspx");
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(Request.QueryString["idLocal"]))
                {
                    int idLocal = Convert.ToInt32(Request.QueryString["idLocal"]);
                    Session["IdLocal"] = idLocal;
                    CargarInformacionLocal(idLocal);
                }
                else if (Session["IdLocal"] != null)
                {
                    int idLocal = (int)Session["IdLocal"];
                    CargarInformacionLocal(idLocal);
                }

                CargarCategorias();

                int idCategoria = 0;
                int.TryParse(Request.QueryString["idCategoria"], out idCategoria);
                CargarProductos(idCategoria);
            }
        }

        private void CargarCategorias()
        {
            //Si la variable de sesion es nula casteamos a int y si no tiene nada usa por defecto 0
            int idLocal = Session["IdLocal"] != null ? (int)Session["IdLocal"] : 0;
            rptCategorias.DataSource = categoriaL.ObtenerCategoriasPorLocal(idLocal);
            rptCategorias.DataBind();
        }

        private void CargarProductos(int idCategoria = 0)
        {
            int idLocal = Session["IdLocal"] != null ? (int)Session["IdLocal"] : 0;
            if (idCategoria == 99)
            {
                var promociones = productoL.ObtenerPromocionesPorLocal(idLocal);
                rptProductos.DataSource = promociones;
                rptProductos.DataBind();
            }
            else
            {

                var productos = productoL.ObtenerProductos(idLocal);
                if (idCategoria > 0)
                    productos = productos.FindAll(p => p.IdCategoria == idCategoria);
                rptProductos.DataSource = productos;
                rptProductos.DataBind();
            }
        }

        protected void rptProductos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "AgregarCarrito")
            {
                int idProducto = Convert.ToInt32(e.CommandArgument);
                int idLocal = Session["IdLocal"] != null ? (int)Session["IdLocal"] : 0;

                TextBox txtCantidad = (TextBox)e.Item.FindControl("txtCantidad");
                int cantidad = 1;
                if (txtCantidad != null && int.TryParse(txtCantidad.Text, out int cant) && cant > 0)
                    cantidad = cant;

                int idCategoria = 0;
                int.TryParse(Request.QueryString["idCategoria"], out idCategoria);

                Producto producto = null;
                if (idCategoria == 99)
                    producto = productoL.ObtenerPromocionesPorLocal(idLocal).Find(p => p.Id == idProducto);
                else
                    producto = productoL.ObtenerProductos(idLocal).Find(p => p.Id == idProducto);
                if (producto != null)
                    carritoL.AgregarProducto(producto, cantidad, idLocal);
                Response.Redirect(Request.RawUrl);

            }
        }

        private void CargarInformacionLocal(int idLocal)
        {
            Modelo.Local local = localL.ObtenerLocalPorId(idLocal);

            if (local == null)
                Response.Write("LOCAL NULL");

            if (local != null)
            {
                imgLocalInfo.ImageUrl = local.Imagen;
                lblNombreLocal.Text = local.Nombre;
                lblDescripcionLocal.Text = local.Descripcion;
                lblHorario.Text = local.HorarioLocal;
                lblTelefono.Text = local.Telefono;
                lblEmail.Text = local.Email;
                lblEstado.Text = local.Estado;
                lblCalificacion.Text = local.Calificacion.Puntaje.ToString("0.0");
            }
        }
    }
}
