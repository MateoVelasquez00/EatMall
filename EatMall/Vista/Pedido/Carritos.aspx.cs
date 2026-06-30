using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json; 
using EatMall.Datos;
using EatMall.Logica;
using EatMall.Modelo;

namespace EatMall.Vista.Pedido
{
    public partial class Carritos : System.Web.UI.Page
    {
        private PedidoL pedidoL = new PedidoL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                for (int i = 8; i <= 20; i++)
                {
                    string hora = i.ToString("D2") + ":00";
                    ddlHoraEntrega.Items.Add(new ListItem(hora, hora));
                }
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Vista/Auth/Login.aspx");
                return;
            }

            try
            {
                
                string jsonCarrito = CarritoData.Value;

                if (!string.IsNullOrEmpty(jsonCarrito) && jsonCarrito != "[]")
                {
                    
                    var items = JsonConvert.DeserializeObject<List<EatMall.Modelo.Carrito>>(jsonCarrito);

                    if (items != null && items.Count > 0)
                    {
                        UsuarioLogin oUser = (UsuarioLogin)Session["Usuario"];
                        string horaSeleccionada = ddlHoraEntrega.SelectedValue;
                        int idLocal = Session["IdLocal"] != null ? (int)Session["IdLocal"] : 0;

                       
                        Modelo.Pedido pedido = pedidoL.ConfirmarPedido(items, oUser.Id, horaSeleccionada);

                        
                        decimal totalAcumulado = 0;
                        foreach (var item in items)
                        {
                            totalAcumulado += (item.Precio * item.Cantidad);
                        }

                        Session["Total"] = totalAcumulado.ToString("N2");
                        Session["IdPedido"] = pedido.Id;
                        Session["CodigoPedido"] = pedido.CodigoPedido;
                        Session["HoraEntrega"] = horaSeleccionada;


                        string scriptLimpieza = @"localStorage.removeItem('carrito'); 
                                                 window.location='" + ResolveUrl("~/Vista/Pago/MetodosPago.aspx") + "';";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "FinalizarPedido", scriptLimpieza, true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al procesar el carrito de EatMall: " + ex.Message);
            }


            Response.Redirect("~/Vista/Pago/MetodosPago.aspx");
        }
    }
}