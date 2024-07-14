using Microsoft.AspNetCore.Mvc;
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

        //Pedidos Diarios
        [HttpGet("PedidosDiarios/{fecha}")]
        public ActionResult<List<Pedido>> GetPedidosDiarios(DateTime fecha)
        {
            var pedidos = _memoriaPedido.Leer().Where(x => x.FechaPedido.Date == fecha.Date).ToList();
            return Ok(pedidos);
        }
        //Pedidos Mensuales
        [HttpGet("PedidosMensuales/{anno}/{mes}")]
        public ActionResult<List<Pedido>> GetPedidosMensuales(int anno, int mes)
        {
            var pedidos = _memoriaPedido.Leer().Where(x => x.FechaPedido.Date.Year == anno && x.FechaPedido.Date.Month == mes).ToList();
            return Ok(pedidos);
        }
        //Pedidos Trimestrales
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
        //Pedidos Por Cada Bodega
        [HttpGet("PedidosPorBodega/{bodegaId}")]
        public ActionResult<List<Pedido>> GetPedidosPorBodega(int bodegaId)
        {
            var pedidos = _memoriaPedido.Leer().Where(x => x.Producto!.BodegaId == bodegaId).ToList();
            return Ok(pedidos);
        }

        //Pedidos por cliente
        [HttpGet("PedidosPorCliente/{clienteId}/{inicio}/{final}")]
        public ActionResult<List<Pedido>> GetPedidosPorCliente(int clienteId, DateTime? inicio, DateTime? final)
        {
            var pedidos = _memoriaPedido.Leer().Where(p => p.ClienteId == clienteId);

            if (inicio.HasValue)
            {
                pedidos = pedidos.Where(p => p.FechaPedido >= inicio.Value);
            }
            if (final.HasValue)
            {
                pedidos = pedidos.Where(p => p.FechaPedido <= final.Value);
            }
            return Ok(pedidos);
        }

        //Clientes Mas Pedidos
        [HttpGet("ClientesMasPedidos/{anno}/{mes}")]
        public ActionResult<List<Reporte>> GetClientesMasPedidos(int anno, int mes)
        {
            var pedidos = _memoriaPedido.Leer().Where(p => p.FechaPedido.Year == anno && p.FechaPedido.Month == mes);
            var reporte = pedidos.GroupBy(p => p.ClienteId)
                                 .Select(g => new Reporte
                                 {
                                     ClienteId = g.Key,
                                     TotalMonto = g.Sum(p => p.Cantidad * p.CostoTotal)
                                 }).ToList();
            return Ok(reporte);
        }
    }
}
