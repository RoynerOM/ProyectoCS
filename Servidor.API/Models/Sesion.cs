namespace Proyecto2.API.Models;

public partial class Sesion : ModeloBase
{
    public Sesion(int ClienteId)
    {
        this.ClienteId = ClienteId;
    }

    public int ClienteId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
