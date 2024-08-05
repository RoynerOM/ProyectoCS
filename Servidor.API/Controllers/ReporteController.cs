using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2.API.Interfaces;
using Proyecto2.API.Models;

namespace Proyecto2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly ICrud<Pedido> _memoriaPedido;
        private readonly ICrud<Cliente> _memoriaCliente;

        public ReporteController(ICrud<Pedido> memoriaPedido, ICrud<Cliente> memoriaCliente)
        {
            _memoriaPedido = memoriaPedido;
            _memoriaCliente = memoriaCliente;
        }

        public DateTime Fecha(DateTime date) => DateTime.Parse(date.Date.ToString());

        [HttpGet("PedidosDiarios/{fecha}")]
        public ActionResult<List<Pedido>> GetPedidosDiarios(DateTime fecha)
        {
            var pedidos = _memoriaPedido.Leer().Include(x => x.Producto).Where(x => x.FechaPedido.Date == fecha.Date);
            return Ok(pedidos.ToList());
        }

        [HttpGet("PedidosMensuales/{anno}/{mes}")]
        public ActionResult<List<Pedido>> GetPedidosMensuales(int anno, int mes)
        {
            var pedidos = _memoriaPedido.Leer().Where(x => x.FechaPedido.Date.Year == anno && x.FechaPedido.Date.Month == mes).ToList();
            return Ok(pedidos);
        }

        [HttpGet("PedidosTrimestrales/{anno}/{trimestre}")]
        public ActionResult<List<Pedido>> GetPedidosTrimestrales(int anno, int trimestre)
        {
            int mesInicial = (trimestre - 1) * 3 + 1;

            int mesFinal = mesInicial + 2;

            var pedidos = _memoriaPedido.Leer()
                       .Where(p => p.FechaPedido.Year == anno && p.FechaPedido.Month >= mesInicial && p.FechaPedido.Month <= mesFinal)
                       .ToList();

            return Ok(pedidos);
        }

        [HttpGet("PedidosPorBodega/{bodegaId}")]
        public ActionResult<List<Pedido>> GetPedidosPorBodega(int bodegaId)
        {
            var pedidos = _memoriaPedido.Leer().Where(x => x.Producto!.BodegaId == bodegaId).ToList();
            return Ok(pedidos);
        }

        [HttpGet("PedidosPorCliente/{clienteId}/{inicio}/{final}")]
        public ActionResult<List<Pedido>> GetPedidosPorCliente(int clienteId, DateTime? inicio, DateTime? final)
        {
            var pedidos = _memoriaPedido.Leer().Where(p => p.ClienteId == clienteId);

            if (inicio.HasValue)
            {
                pedidos = pedidos.Where(p => p.FechaPedido >= inicio.Value.Date);
            }
            if (final.HasValue)
            {
                pedidos = pedidos.Where(p => p.FechaPedido <= final.Value.Date);
            }
            return Ok(pedidos);
        }

        [HttpGet("ClientesMasPedidos/{anno}/{mes}")]
        public ActionResult<List<Reporte>> GetClientesMasPedidos(int anno, int mes)
        {
            var pedidos = _memoriaPedido.Leer().Where(p => p.FechaPedido.Year == anno && p.FechaPedido.Month == mes);
            var reporte = pedidos.GroupBy(p => p.ClienteId)
                                 .Select(g => new Reporte
                                 {
                                     ClienteId = g.Key,
                                     TotalMonto = g.Sum(p => (decimal)(p.Cantidad * p.CostoTotal)!)
                                 }).ToList();
            return Ok(reporte);
        }
    }
}
