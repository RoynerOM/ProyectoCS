using Microsoft.AspNetCore.Mvc;
using Proyecto2.API.Interfaces;
using Proyecto2.API.Models;
using Proyecto2.API.Datos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyecto2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ICrud<Pedido> _memoria;
        private readonly ICrud<Inventario> _memoriaInventario;
        private readonly ICrud<Cliente> _memoriaCliente;

        public PedidosController(ICrud<Pedido> memoria, ICrud<Inventario> memoriaInventario, ICrud<Cliente> memoriaCliente)
        {
            _memoria = memoria;
            _memoriaInventario = memoriaInventario;
            _memoriaCliente = memoriaCliente;
        }

        // GET: api/<PedidosController>
        [HttpGet]
        public ActionResult<List<Pedido>> Get()
        {
            return _memoria.Leer();
        }

        // GET api/<InventarioController>/5
        [HttpGet("{id}")]
        public ActionResult<Pedido> Get(int id)
        {
            var inventario = _memoria.Buscar(id);

            if (inventario == null) return NotFound();

            return Ok(inventario);
        }

        // POST api/<PedidosController>
        [HttpPost]
        public ActionResult<Boolean> Post(Pedido modelo)
        {
            Inventario? producto = _memoriaInventario.Buscar(modelo.ProductoId);
            Cliente? cliente = _memoriaCliente.Buscar(modelo.ClienteId);

            modelo.Producto = producto;
            modelo.FechaPedido = DateTime.Now;
            modelo.CalcularCostoTotal();
            modelo.Cliente = cliente;
            _memoria.Crear(modelo);
            return Ok(true);
        }

        // PUT api/<PedidosController>/5
        [HttpPut("{id}")]
        public ActionResult<Boolean> Put(int id, Pedido modelo)
        {
            if (id != modelo.Id)
            {
                return BadRequest();
            }

            Inventario? producto = _memoriaInventario.Buscar(modelo.ProductoId);
            modelo.Producto = producto;
            modelo.CalcularCostoTotal();
            _memoria.Actualizar(modelo);

            return Ok(true);
        }

        // DELETE api/<PedidosController>/5
        [HttpDelete("{id}")]
        public ActionResult<Boolean> Delete(int id)
        {
            _memoria.Eliminar(id);

            return Ok(true);
        }
    }
}
