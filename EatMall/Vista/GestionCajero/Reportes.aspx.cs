using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EatMall.Logica; // Asegúrate de apuntar a tus capas
using EatMall.Modelo;

namespace EatMall.Vista.GestionCajero
{
    public partial class Reportes : System.Web.UI.Page
    {
        // Instanciamos tu capa lógica de Pedidos
        PedidoL oPedidoL = new PedidoL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Cajero"] == null)
                {
                    Response.Redirect("~/Vista/Usuario/Login.aspx");
                    return;
                }
                int IdLocal = Convert.ToInt32(Session["IdLocal"]);
                CargarPedidosEntregados(IdLocal);
            }
        }

        private void CargarPedidosEntregados(int IdLocal)
        {
            

 
            gvHistorialPedidos.DataSource = oPedidoL.MtListarPedido(IdLocal);
            gvHistorialPedidos.DataBind();
        }
    }
}