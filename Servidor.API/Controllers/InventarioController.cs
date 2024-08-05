using Microsoft.AspNetCore.Mvc;
using Proyecto2.API.Interfaces;
using Proyecto2.API.Models;

namespace Proyecto2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly ICrud<Inventario> _memoria;

        public InventarioController(ICrud<Inventario> memoria)
        {
            _memoria = memoria;
        }

        [HttpGet]
        public ActionResult<List<Inventario>> Get()
        {
            return _memoria.Leer().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Inventario> Get(int id)
        {
            var inventario = _memoria.Buscar(id);

            if (inventario == null) return NotFound();

            return Ok(inventario);
        }

        [HttpPost]
        public ActionResult<bool> Post(Inventario modelo)
        {
            _memoria.Crear(modelo);
            return Ok(true);
        }

        [HttpPut("{id}")]
        public ActionResult<bool> Put(int id, Inventario modelo)
        {
            if (id != modelo.Id)
            {
                return BadRequest();
            }

            _memoria.Actualizar(modelo);

            return Ok(true);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            _memoria.Eliminar(id);

            return Ok(true);
        }
    }
}
