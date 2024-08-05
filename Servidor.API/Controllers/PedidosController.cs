using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2.API.Interfaces;
using Proyecto2.API.Models;

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

        [HttpGet]
        public ActionResult<List<Pedido>> Get()
        {
            return _memoria.Leer().Include(p => p.Producto).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Pedido> Get(int id)
        {
            var inventario = _memoria.Buscar(id);

            if (inventario == null) return NotFound();

            return Ok(inventario);
        }

        [HttpPost]
        public ActionResult<bool> Post(Pedido modelo)
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

        [HttpPut("{id}")]
        public ActionResult<bool> Put(int id, Pedido modelo)
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

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            _memoria.Eliminar(id);

            return Ok(true);
        }
    }
}
