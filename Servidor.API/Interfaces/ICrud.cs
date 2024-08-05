namespace Proyecto2.API.Interfaces
{
    public interface ICrud<T>
    {
        void Crear(T model);
        void Actualizar(T model);
        T? Buscar(int id);
        void Eliminar(int id);
        IQueryable<T> Leer(); 
    }   
}
