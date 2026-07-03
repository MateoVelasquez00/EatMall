namespace EatMall.Modelo
{
    public class DetallePedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int IdLocal { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public decimal PrecioProducto { get; set; }
        public string NombreLocal { get; set; }
        public string NombreCC { get; set; }
        public string EstadoProducto { get; set; }
    }
}