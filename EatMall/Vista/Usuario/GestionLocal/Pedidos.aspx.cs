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
    public partial class Pedidos : System.Web.UI.Page
    {
        PedidoL pedidoL = new PedidoL();
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

                int idLocal = usuario.IdLocal;

                CargarPedidos(idLocal);
            }
        }
        private void CargarPedidos(int idLocal)
        {
            gvPedidos.DataSource = pedidoL.MtListarPedido(idLocal);
            gvPedidos.DataBind();
        }
    }
}