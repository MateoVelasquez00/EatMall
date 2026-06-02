using System;

namespace EatMall.Modelo
{
    public class Pedido
    {
        public int Id { get; set; }
        public string CodigoPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; }
        public decimal Total { get; set; }
        public string TipoEntrega { get; set; }
        public int IdCliente { get; set; }

        public TimeSpan HoraEntrega { get; set; }
    }
}