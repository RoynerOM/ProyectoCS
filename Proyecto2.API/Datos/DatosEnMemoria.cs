using Proyecto2.API.Interfaces;
using Proyecto2.API.Models;

namespace Proyecto2.API.Datos
{
    public class DatosEnMemoria<T> : ICrud<T> where T : ModeloBase
    {
        private readonly List<T> _datos;

        public DatosEnMemoria(List<T> datosIniciales)
        {
            this._datos = datosIniciales;
        }

        public void Actualizar(T model)
        {
            var index = _datos.FindIndex(x => x.Id == model.Id);
            if (index >= 0) _datos[index] = model;
        }

        public T? Buscar(int id) => _datos.SingleOrDefault(x => x.Id == id);

        public void Crear(T model)
        {
            int maxId = _datos.Max(x => x.Id);
            model.Id = maxId+1;
            _datos.Add(model);
        }

        public void Eliminar(int id)
        {
            var modelo = _datos.SingleOrDefault(x => x.Id == id);
            if (modelo != null) _datos.Remove(modelo);
        }

        public List<T> Leer() => _datos;
    }
}
