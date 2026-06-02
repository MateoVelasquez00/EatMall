using EatMall.Logica;
using EatMall.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EatMall.Vista.Usuario
{
    public partial class DetallesPedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["id"], out int idPedido))
                {
                    CargarDetalles(idPedido);
                }
                else
                {
                    Response.Redirect("Perfil.aspx");
                }
            }
        }

        private void CargarDetalles(int idPedido)
        {
            DetallePedidoL oLogica = new DetallePedidoL();
            List<DetallePedido> lista = oLogica.MtObtenerDetalles(idPedido);

            if (lista.Count > 0)
            {
                rptDetalles.DataSource = lista;
                rptDetalles.DataBind();
            }
            else
            {
                lblSinDetalles.Visible = true;
            }
        }
    }
}
