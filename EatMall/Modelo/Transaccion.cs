namespace EatMall.Modelo
{
    public class Transaccion
    {
        public int Id { get; set; }
        public int IdMetodoPago { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        public System.DateTime FechaTransaccion { get; set; }
    }
}