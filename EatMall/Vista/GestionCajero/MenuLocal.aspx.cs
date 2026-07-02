using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EatMall.Vista.Usuario
{
    public partial class MenuLocal : System.Web.UI.Page
    {
        ProductoL oproductol = new ProductoL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Cajero"] == null)
            {
                Response.Redirect("~/Vista/Auth/Login.aspx");
                return;
            }

            if (!IsPostBack)
                CargarProductos();
        }

        private void CargarProductos()
        {
            Modelo.Cajero cajero = (Modelo.Cajero)Session["Cajero"];
            List<Producto> productos = oproductol.ObtenerProductos(cajero.IdLocal);

            gvProductos.DataSource = productos;
            gvProductos.DataBind();
        }
        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CambiarEstado")
            {     
                int idProducto = Convert.ToInt32(e.CommandArgument);

                bool resultado = oproductol.CambiarEstadoProducto(idProducto);

                if (resultado)
                {
                    CargarProductos();
                }
            }
        }
    }
}