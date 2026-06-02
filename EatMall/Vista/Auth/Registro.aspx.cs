using System;
using System.Web.UI;
using EatMall.Logica;
using EatMall.Modelo;

namespace EatMall.Vista.Auth
{
	public partial class Registro : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e) { }

		protected void btnRegistrar_Click(object sender, EventArgs e)
		{

			Cliente oClienteTem = new Cliente()
			{
				Documento = txtDocumento.Text,
				Nombre = txtNombre.Text,
				Apellido = txtApellido.Text,
				Email = txtEmail.Text,
				Telefono = txtTelefono.Text,
				Contraseña = txtPassword.Text,
				Estado = true
			};

			RegistroL oRegistro = new RegistroL();

			if (oRegistro.MtValidarDatosProspecto(oClienteTem))
			{
				string codigoOTP = new Random().Next(100000, 999999).ToString();

				CodigoVerificacionL oEmail = new CodigoVerificacionL();
				oEmail.MtEnviarCodigo(oClienteTem.Email, codigoOTP);

				Session["DatosRegistro"] = oClienteTem;
				Session["CodigoEsperado"] = codigoOTP;

				Response.Redirect("VerificarCuenta.aspx");
			}
		}
		protected void btnVolver_Click(object sender, EventArgs e)
		{
			Response.Redirect("../../Index.aspx");
		}
	}
}