using Microsoft.AspNetCore.Mvc;
using Proyecto2.API.Interfaces;
using Proyecto2.API.Models;

namespace Proyecto2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ICrud<Cliente> _memoria;
        private readonly ICrud<Sesion> _memoriaSesion;

        public ClienteController(ICrud<Cliente> memoria, ICrud<Sesion> memoriaSesion)
        {
            _memoria = memoria;
            _memoriaSesion = memoriaSesion;
        }

        // GET: api/<ClienteController>
        [HttpGet]
        public ActionResult<List<Cliente>> Get()
        {
            return _memoria.Leer();
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(int id)
        {
            var inventario = _memoria.Buscar(id);

            if (inventario == null) return NotFound();

            return Ok(inventario);
        }

        // POST api/<ClienteController>
        [HttpPost]
        public ActionResult<Boolean> Post(Cliente modelo)
        {
            _memoria.Crear(modelo);
            return Ok(true);
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public ActionResult<Boolean> Put(int id, Cliente modelo)
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

        [HttpGet("Login/{usuario}")]
        public ActionResult Login(string usuario)
        {
            string id = usuario.Split(" ").First();
            string contra = usuario.Split(" ").Last();

            var cliente = _memoria.Leer().FirstOrDefault(u => u.Identificacion == id && u.Contrasena == contra);

            if (cliente != null)
            {             
                Sesion sesion = new Sesion(cliente.Id);
                _memoriaSesion.Crear(sesion);
                return Ok(sesion);
            }

            return Unauthorized(new { Message = "Email or password is incorrect" });
        }
        // Esta registra la sesion del cliente que esta usando la app, lo correcto es usar claims pero el lider proyecto no quiere usarlo ya es toda la información se guarda en cache o memoria sin base de datos
        // GET api/<ClienteController>/5
        [HttpGet("Sesion/{sesionId}")]
        public ActionResult GetSesion(int sesionId)
        {
            var sesion = _memoriaSesion.Leer().SingleOrDefault(x => x.Id == sesionId);

            if (sesion == null) return NotFound();

            return Ok(sesion);
        }
    }
}
