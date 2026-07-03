using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EatMall.Logica;
using EatMall.Modelo;

namespace EatMall.Vista.GestionCajero
{
    public partial class Promociones : System.Web.UI.Page
    {
        PromocionL opromocionL = new PromocionL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Cajero"] == null)
            {
                Response.Redirect("~/Vista/Auth/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarPromociones();
            }
        }

        private void CargarPromociones()
        {
            Modelo.Cajero cajero = (Modelo.Cajero)Session["Cajero"];
            List<Promocion> listaPromos = opromocionL.MtListarPromocionesPorLocal(cajero.IdLocal);

            gvPromociones.DataSource = listaPromos;
            gvPromociones.DataBind();
        }

        protected void gvPromociones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CambiarEstado")
            {
                int idPromocion = Convert.ToInt32(e.CommandArgument);
                bool exito = opromocionL.MtCambiarEstadoPromocion(idPromocion);

                if (exito)
                {
                    CargarPromociones();
                }
            }
        }
    }
}