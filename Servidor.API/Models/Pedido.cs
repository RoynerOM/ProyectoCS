using System.Text.Json.Serialization;

namespace Proyecto2.API.Models
{
    public enum EstadoPedido
    {
        EnProceso,
        Facturado,
        PorEntregar,
        Entregado
    }

    public partial class Pedido:  ModeloBase
    {
        public int ClienteId { get; set; }
        public int ProductoId { get; set; }
        public int? Cantidad { get; set; }
        public decimal CostoSinIva { get; set; }
        public decimal CostoTotal { get; set; }
        public DateTime FechaPedido { get; set; }
        public EstadoPedido Estado { get; set; }

        [JsonIgnore]
        public virtual Cliente? Cliente { get; set; }
        public virtual Inventario? Producto { get; set; }

        public void CalcularCostoTotal()
        {
            CostoSinIva = (decimal)((Producto?.Precio??0) * Cantidad)!;
            CostoTotal = CostoSinIva * 1.13M;
        }
    }
}
