namespace Proyecto1.Models
{
    public enum EstadoPedido
    {
        EnProceso,
        Facturado,
        PorEntregar,
        Entregado
    }

    public class Pedido
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public int ClienteId { get; set; }
        public int Cantidad { get; set; }
        public decimal CostoSinIVA { get; set; }
        public decimal CostoTotal { get; set; }
        public DateTime FechaPedido { get; set; }
        public EstadoPedido Estado { get; set; }
        public Inventario? Producto { get; set; }
        public Cliente? Cliente { get; set; }

        public void CalcularCostoTotal()
        {
            CostoSinIVA = (Producto?.Precio ?? 0) * Cantidad;
            CostoTotal = CostoSinIVA * 1.13M;
        }
    }
}
