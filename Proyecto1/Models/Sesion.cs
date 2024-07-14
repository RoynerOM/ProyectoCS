namespace Proyecto1.Models
{
    public class Sesion
    {
        public Sesion(int Id, int ClientId)
        {
            this.Id = Id;
            this.ClienteId = ClientId;
        }

        public int Id { get; set; }
        public int ClienteId { get; set; }
    }

    public static class SesionGuardada
    {
        static public Sesion? Info { get; set; }
    }
}
