using System;

namespace EatMall.Modelo
{
    public class Transaccion
    {
		public int Id { get; set; }
		public int IdPedido { get; set; }
		public int IdMetodoPago { get; set; }
		public decimal Monto { get; set; }
		public string Referencia { get; set; }
		public string Estado { get; set; }
		public DateTime FechaTransaccion { get; set; }
		public string PayuTransaccionId { get; set; }
		public string PayuCodigoReferencia { get; set; }
	}
}