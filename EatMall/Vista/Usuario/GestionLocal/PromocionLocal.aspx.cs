using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EatMall.Logica;
using EatMall.Modelo;

namespace EatMall.Vista.Usuario.GestionLocal
{
	public partial class PromocionLocal : System.Web.UI.Page
	{
		PromocionL opromocionL = new PromocionL();

		protected void Page_Load(object sender, EventArgs e)
		{
			UsuarioLogin usuario = Session["Usuario"] as UsuarioLogin;

			if (usuario == null)
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
			UsuarioLogin usuario = Session["Usuario"] as UsuarioLogin;
			List<Promocion> listaPromos = opromocionL.MtListarPromocionesPorLocal(usuario.IdLocal);

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