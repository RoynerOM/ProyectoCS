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

        // GET: api/<InventarioController>
        [HttpGet]
        public ActionResult<List<Inventario>> Get()
        {
            return _memoria.Leer();
        }

        // GET api/<InventarioController>/5
        [HttpGet("{id}")]
        public ActionResult<Inventario> Get(int id)
        {
            var inventario = _memoria.Buscar(id);

            if(inventario == null) return NotFound();

            return Ok(inventario);
        }

        // POST api/<InventarioController>
        [HttpPost]
        public ActionResult<Boolean> Post(Inventario modelo)
        {
            _memoria.Crear(modelo);
            return Ok(true);
        }

        // PUT api/<InventarioController>/5
        [HttpPut("{id}")]
        public ActionResult<Boolean> Put(int id, Inventario modelo)
        {
            if (id != modelo.Id)
            {
                return BadRequest();
            }

            _memoria.Actualizar(modelo);

            return Ok(true);
        }

        // DELETE api/<InventarioController>/5
        [HttpDelete("{id}")]
        public ActionResult<Boolean> Delete(int id)
        {
            _memoria.Eliminar(id);

            return Ok(true);
        }
    }
}
