using System.ComponentModel.DataAnnotations;

namespace Proyecto1.Models
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

    public class Inventario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        // Cantidad En Existencia 
        [Display(Name = "Cantidad En Existensia")]
        public int Stock { get; set; }

        [Display(Name = "Bodega Id")]
        public int BodegaId { get; set; }
        [Display(Name = "Fecha de Ingreso")]
        public DateTime FechaIngreso { get; set; }
        [Display(Name = "Fecha De Vencimiento")]
        public DateTime? FechaVencimiento { get; set; }
        [Display(Name = "Tipo De Licor")]
        public TipoLicor TipoLicor { get; set; }
    }

    /*

      public class Inventario
      {
          public int idArticulo { get; set; }
          public int cantidadExistencia { get; set; }
          public int idbodega { get; set; }
          public DateTime fechaingreso { get; set; }
          public DateTime fechaVencimiento { get; set; }
          public string tipo { get; set; } = null!;
           public Double precio { get; set; }

      }*/

}
