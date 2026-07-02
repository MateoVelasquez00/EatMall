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
    public partial class ProductosLocal : System.Web.UI.Page
    {
        ProductoL logica = new ProductoL();
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioLogin usuario = Session["Usuario"] as UsuarioLogin;

            if (usuario == null)
            {
                Response.Redirect("~/Vista/Auth/Login.aspx");
                return;
            }

            int idLocal = usuario.IdLocal;

            
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["estado"]))
                {
                    int idProducto = Convert.ToInt32(Request.QueryString["estado"]);
                    bool estado = Convert.ToBoolean(Request.QueryString["valor"]);

                    logica.MtCambiarEstadoProducto(idProducto, estado);

                    Response.Redirect("ProductosLocal.aspx");
                    return;
                }

                CargarProductos(idLocal);
            }
        }
        private void CargarProductos(int idLocal)
        {
            gvProductos.DataSource = logica.MtListarProductosPorLocal(idLocal);
            gvProductos.DataBind();
        }
    }
}