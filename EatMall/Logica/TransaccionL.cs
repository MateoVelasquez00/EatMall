using EatMall.Datos;
using EatMall.Modelo;

namespace EatMall.Logica
{
    public class TransaccionL
    {
        private TransaccionD datos = new TransaccionD();

        public int ProcesarPago(Transaccion oTransaccion, int idPedido)
        {
            int idTransaccion = datos.InsertarTransaccion(oTransaccion);
            datos.InsertarPedidoTransaccion(idPedido, idTransaccion);
            return idTransaccion;
        }
    }
}