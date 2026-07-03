using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using EatMall.Logica;
using EatMall.Modelo;

namespace EatMall.Vista.GestionCajero
{
    public partial class NuevaPromocion : System.Web.UI.Page
    {
        private PromocionL opromocionL = new PromocionL();
        private ProductoL oProductoL = new ProductoL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Cajero"] == null)
            {
                Response.Redirect("~/Vista/Auth/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                txtFechaInicio.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtFechaFin.Text = DateTime.Now.AddMonths(6).ToString("yyyy-MM-dd");

                CargarProductosDelLocal();
            }
        }

        private void CargarProductosDelLocal()
        {
            Modelo.Cajero cajero = (Modelo.Cajero)Session["Cajero"];


            gvSeleccionarProductos.DataSource = oProductoL.ObtenerProductos(cajero.IdLocal);
            gvSeleccionarProductos.DataBind();
        }

        protected void btnGuardarPromocion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombrePromo.Text) || string.IsNullOrEmpty(txtPrecioPromo.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Por favor, complete todos los campos obligatorios.');", true);
                return;
            }

            try
            {
                string urlImagen = "https://i.imgur.com/...";
                if (fuImagenPromo.HasFile)
                {
                    string extension = Path.GetExtension(fuImagenPromo.FileName).ToLower();
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".webp")
                    {
                        string nuevoNombre = Guid.NewGuid().ToString() + extension;
                        string rutaServidor = Server.MapPath("~/Uploads/");

                        if (!Directory.Exists(rutaServidor))
                        {
                            Directory.CreateDirectory(rutaServidor);
                        }

                        fuImagenPromo.SaveAs(rutaServidor + nuevoNombre);
                        urlImagen = "~/Uploads/" + nuevoNombre;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Formato de imagen inválido.');", true);
                        return;
                    }
                }

                Modelo.Cajero cajero = (Modelo.Cajero)Session["Cajero"];
                string nombre = txtNombrePromo.Text.Trim();
                decimal total = Convert.ToDecimal(txtPrecioPromo.Text);
                DateTime fechaInicio = Convert.ToDateTime(txtFechaInicio.Text);
                DateTime fechaFin = Convert.ToDateTime(txtFechaFin.Text);


                List<Producto> productosAAsociar = new List<Producto>();

                foreach (GridViewRow fila in gvSeleccionarProductos.Rows)
                {
                    if (fila.RowType == DataControlRowType.DataRow)
                    {
 
                        TextBox txtCantidad = (TextBox)fila.FindControl("txtCantidad");

                        if (txtCantidad != null && !string.IsNullOrEmpty(txtCantidad.Text))
                        {
                            int cantidad = Convert.ToInt32(txtCantidad.Text);

                            
                            if (cantidad > 0)
                            {
                               
                                int idProducto = Convert.ToInt32(gvSeleccionarProductos.DataKeys[fila.RowIndex].Values["Id"]);
                                string nombreProd = gvSeleccionarProductos.DataKeys[fila.RowIndex].Values["Nombre"].ToString();
                                string descProd = gvSeleccionarProductos.DataKeys[fila.RowIndex].Values["Descripcion"].ToString();
                                decimal precioUnitario = Convert.ToDecimal(gvSeleccionarProductos.DataKeys[fila.RowIndex].Values["Precio"]);

  
                                Producto itemCombo = new Producto();
                                itemCombo.Id = idProducto;

                                
                                itemCombo.Descripcion = $"{nombreProd} x{cantidad}";

                                
                                itemCombo.Precio = precioUnitario * cantidad;

                                productosAAsociar.Add(itemCombo);
                            }
                        }
                    }
                }

                if (productosAAsociar.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Debe asignar al menos 1 unidad de algún producto para guardar el combo.');", true);
                    return;
                }

                bool exito = opromocionL.MtRegistrarPromocionCompleta(nombre, urlImagen, fechaInicio, fechaFin, total, cajero.IdLocal, productosAAsociar);

                if (exito)
                {
                    Response.Redirect("~/Vista/GestionCajero/Promociones.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No se pudo guardar la promoción en la base de datos.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Error de ejecución: {ex.Message}');", true);
            }
        }
    }
}