namespace Proyecto1.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public string Provincia { get; set; } = null!;
        public string Canton { get; set; } = null!;
        public string Distrito { get; set; } = null!;
        public decimal CantidadDineroTotal { get; set; }
        public decimal CantidadDineroUltimoAno { get; set; }
        public decimal CantidadDineroUltimos6Meses { get; set; }
        public string Contrasena { get; set; } = null!;
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
