using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using EatMall.Logica;
using EatMall.Modelo;

namespace EatMall
{
	public partial class Index : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				CentroComercialL oCentroComercial = new CentroComercialL();
				rptCentrosComerciales.DataSource = oCentroComercial.MtListarCentrosComercial();
				rptCentrosComerciales.DataBind();
				MtCargarDatosIndex();

			}
		}


		private void MtCargarDatosIndex()
		{
			try
			{
				CentroComercialL oCentroComercial = new CentroComercialL();
				var listaCC = oCentroComercial.MtListarCentrosComercial();

				rptCarousel.DataSource = listaCC;
				rptCarousel.DataBind();

				rptCentrosComerciales.DataSource = listaCC;
				rptCentrosComerciales.DataBind();
			}
			catch (Exception ex)
			{
				Response.Write("Error al cargar datos: " + ex.Message);
			}
		}

		[WebMethod]
		public static List<CentroComercial> MtObtenerPuntos()
		{
			CentroComercialL oCentroL = new CentroComercialL();
			return oCentroL.MtListarCentrosComercial();
		}

	}
}