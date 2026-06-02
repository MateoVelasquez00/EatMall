using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace EatMall.Logica
{
	public class CodigoVerificacionL
	{
		public void MtEnviarCodigo(string correoDestino, string codigo)
		{
			try
			{
				MailMessage mail = new MailMessage();
				mail.From = new MailAddress("velasquez009mateo@gmail.com", "EatMall Oficial");
				mail.To.Add(correoDestino);
				mail.Subject = "Código de verificación - EatMall";
				mail.Body = $"<h2>Bienvenido a EatMall</h2><p>Tu código de activación es: <b>{codigo}</b></p>";
				mail.IsBodyHtml = true;

				SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

				smtp.Credentials = new NetworkCredential("velasquez009mateo@gmail.com", "mcno ldzv rjhn evvo");
				smtp.EnableSsl = true;
				smtp.Send(mail);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}  
		}
	}
}