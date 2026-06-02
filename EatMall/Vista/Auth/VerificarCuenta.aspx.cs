using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EatMall.Datos;
using EatMall.Logica;
using EatMall.Modelo;

namespace EatMall.Vista.Auth
{
	public partial class VerificarCuenta : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Session["DatosRegistro"] == null)
				{
					Response.Redirect("Registro.aspx");
				}
			}
		}

		protected void btnVerificar_Click(object sender, EventArgs e)
		{
			string codigoIngresado = txtCodigo.Text.Trim();
			string codigoCorrecto = Session["CodigoEsperado"]?.ToString();
			Cliente datosParaGuardar = (Cliente)Session["DatosRegistro"];

			if (datosParaGuardar == null)
			{
				ScriptManager.RegisterStartupScript(this, GetType(), "alert",
					"Swal.fire('Sesión Expirada', 'Por favor, regístrate de nuevo.', 'error').then(() => { window.location.href = 'Registro.aspx'; });", true);
				return;
			}

			if (codigoIngresado == codigoCorrecto && !string.IsNullOrEmpty(codigoIngresado))
			{
				try
				{
					RegistroL logica = new RegistroL();
					if (logica.MtInsertarClienteFinal(datosParaGuardar))
					{
						Session.Remove("DatosRegistro");
						Session.Remove("CodigoEsperado");

						string scriptExito = @"Swal.fire({ 
                    title: '¡Bienvenido!', 
                    text: 'Tu cuenta ha sido creada con éxito.', 
                    icon: 'success',
                    confirmButtonColor: '#f58220'
                }).then((result) => { window.location.href = 'Login.aspx'; });";

						ScriptManager.RegisterStartupScript(this, GetType(), "alertExito", scriptExito, true);
					}
				}
				catch (Exception ex)
				{
					string msg = ex.Message.Replace("'", "").Replace("\n", " ");
					ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"Swal.fire('Error Técnico', '{msg}', 'error');", true);
				}
			}
			else
			{
				ScriptManager.RegisterStartupScript(this, GetType(), "alert",
					"Swal.fire({ title: 'Código Incorrecto', text: 'El código no coincide.', icon: 'warning', confirmButtonColor: '#f58220' });", true);
			}
		}
	}
}
