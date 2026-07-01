using System;
using EatMall.Datos;
using EatMall.Modelo;

namespace EatMall.Logica
{
    public class TransaccionL
    {
		private TransaccionD oTransaccionD = new TransaccionD();
		public bool MtCrearPago(Transaccion oTransaccion)
		{
			return oTransaccionD.MtInsertarTransaccion(oTransaccion);
		}

		public void MtActualizarEstadoPago(string referencia, string estado, string payuId)
		{
			try
			{
				oTransaccionD.MtActualizarEstadoTransaccion(referencia, estado, payuId);
			}
			catch (Exception ex)
			{
				throw new Exception("Error al actualizar pago: " + ex.Message);
			}
		}
	}
}