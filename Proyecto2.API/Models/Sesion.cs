namespace Proyecto2.API.Models
{
    public class Sesion : ModeloBase
    {
        public Sesion(int ClientId)
        {
            this.ClienteId = ClientId;
        }

        public int ClienteId { get; set; }
    }
}
