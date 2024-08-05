namespace Proyecto2.API.Models
{
    public enum TipoLicor
    {
        Cerveza,
        Tequila,
        Ron,
        Ginebra,
        Whiskey,
        Digestivo,
        AguaArdiente,
        VinoTinto,
        VinoBlanco,
        VinoRosado,
        Champagne
    }

    public partial class Inventario : ModeloBase
    {
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public int? Stock { get; set; }
        public int? BodegaId { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public TipoLicor TipoLicor { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
