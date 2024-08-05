using Microsoft.EntityFrameworkCore;
using Proyecto2.API.Interfaces;
using Proyecto2.API.Models;

namespace Proyecto2.API.Datos
{
    public class DatosEnMemoria<T> : ICrud<T> where T : ModeloBase
    {

        protected readonly DbSet<T> _modelos;
        private MokkilicoresDbContext _context;

        public DatosEnMemoria(MokkilicoresDbContext context)
        {
            _context = context;
            _modelos = context.Set<T>();
        }

        public void Actualizar(T model)
        {
            _context.Entry<T>(model).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public T? Buscar(int id) => _modelos.SingleOrDefault(x => x.Id == id);

        public void Crear(T model)
        {
            if (!_context.Database.CanConnect())
            {
                _context.Database.EnsureCreated();
            }
            _context.Entry<T>(model).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            _context.Database.EnsureDeleted();
            _modelos.Remove(Buscar(id)!);
            _context.SaveChanges();
        }

        public IQueryable<T> Leer() => _modelos;
    }
}
