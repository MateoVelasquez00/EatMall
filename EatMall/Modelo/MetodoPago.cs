namespace EatMall.Modelo
{
    public class MetodoPago
    {
        public int Id { get; set; }
        public string NombreMetodo { get; set; }
        public bool Estado { get; set; }
        public int IdLocal { get; set; }
        public string Imagen { get; set; }
    }
}