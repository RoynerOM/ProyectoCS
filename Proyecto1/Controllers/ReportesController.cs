using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class ReportesController : Controller
    {
        ServicioApiBasico<Pedido> http = new();
        ServicioApiBasico<Inventario> httpInventario = new();
        ServicioApiBasico<Reporte> httpReporte = new();

        public async Task<ActionResult> Index(string fecha)
        {

            if (fecha == null)
            {
                return View(new List<Pedido>());
            }

            List<Pedido> pedidosDiarios = await http.Obtener($"api/Reporte/PedidosDiarios/{fecha}");
            return View(pedidosDiarios);
        }

        public async Task<ActionResult> ReporteMensual(int anno, int mes)
        {
            if (mes == 0)
            {
                return View(new List<Pedido>());
            }

            List<Pedido> pedidosDiarios = await http.Obtener($"api/Reporte/PedidosMensuales/{anno}/{mes}");
            return View(pedidosDiarios);
        }

        public async Task<ActionResult> ReporteTrimestral(int anno, int trimestre)
        {
            if (trimestre == 0 || trimestre > 4)
            {
                return View(new List<Pedido>());
            }

            List<Pedido> pedidosDiarios = await http.Obtener($"api/Reporte/PedidosTrimestrales/{anno}/{trimestre}");
            return View(pedidosDiarios);
        }

        public async Task<ActionResult> ReportePorBodega(int bodegaId)
        {
            List<Inventario> inventarios = await httpInventario.Obtener($"api/inventario");

            var bodegas = inventarios.GroupBy(x => x.BodegaId).Select(g => g.Key).ToList();

            ViewData["Bodegas"] = bodegas;

            if (bodegaId == 0)
            {
                return View(new List<Pedido>());
            }

            List<Pedido> pedidosDiarios = await http.Obtener($"api/Reporte/PedidosPorBodega/{bodegaId}");
            return View(pedidosDiarios);
        }

        public async Task<ActionResult> ReportePorCliente(string? inicio, string? final)
        {
            List<Pedido> pedidos;

            var clienteId = SesionGuardada.Info!.ClienteId;

            if (inicio == null)
            {
                return View(new List<Pedido>());
            }

            if (final ==null)
            {
                return View(new List<Pedido>());
            }

            pedidos = await http.Obtener($"api/Reporte/PedidosPorCliente/{clienteId}/{inicio}/{final}");


            return View(pedidos);
        }

        public async Task<ActionResult> ReporteMasPedidos(int anno, int mes)
        {
            if (mes == 0)
            {
                return View(new List<Reporte>());
            }
            return View(await httpReporte.Obtener($"api/Reporte/ClientesMasPedidos/{anno}/{mes}"));
        }
    }
}
