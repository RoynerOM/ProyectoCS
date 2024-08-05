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

        [HttpGet]
        public ActionResult<List<Cliente>> Get()
        {
            return _memoria.Leer().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(int id)
        {
            var inventario = _memoria.Buscar(id);
            if (inventario == null) return NotFound();
            return Ok(inventario);
        }

        [HttpPost]
        public ActionResult<bool> Post(Cliente modelo)
        {
            _memoria.Crear(modelo);
            return Ok(true);
        }

        [HttpPut("{id}")]
        public ActionResult<bool> Put(int id, Cliente modelo)
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

        [HttpGet("Login/{usuario}")]
        public ActionResult Login(string usuario)
        {
            string id = usuario.Split(" ").First();
            string contra = usuario.Split(" ").Last();
            var cliente = _memoria.Leer().FirstOrDefault(u => u.Identificacion == id && u.Contrasena == contra);
            if (cliente != null)
            {             
                Sesion sesion = new(cliente.Id);
                _memoriaSesion.Crear(sesion);
                return Ok(sesion);
            }
            return Unauthorized(new { Message = "Email or password is incorrect" });
        }
       
        [HttpGet("Sesion/{sesionId}")]
        public ActionResult GetSesion(int sesionId)
        {
            var sesion = _memoriaSesion.Buscar(sesionId);
            if (sesion == null) return NotFound();
            return Ok(sesion);
        }
    }
}
