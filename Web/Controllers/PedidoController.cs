using Proyecto1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto1.Controllers
{
    public class PedidoController : Controller
    {
        readonly ServicioApiBasico<Pedido> api = new();
        readonly ServicioApiBasico<Inventario> apiInventatro = new();

        public async Task<Pedido?> BuscarPorId(int Id)=> await api.BuscarPorId($"api/pedidos/{Id}");
        
        public async Task<ActionResult> Index()
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            return View(await api.Obtener($"api/pedidos"));
        }

        public async Task<ActionResult> Details(int id)
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            return View(await BuscarPorId(id));
        }

        public async Task<ActionResult> Create()
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            ViewData["inventarios"] = await apiInventatro.Obtener($"api/inventario");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Pedido pedido)
        {
            pedido.ClienteId = SesionGuardada.Info!.ClienteId;
            return await api.Crear($"api/pedidos", pedido) ? RedirectToAction(nameof(Index)) : View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
          
           
            List<Inventario> inventarios = await apiInventatro.Obtener($"api/inventario");

            ViewData["inventarios"] = inventarios;

            return View(await BuscarPorId(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Pedido pedido)
        {
            var respuestApi = await api.Editar($"api/pedidos/{pedido.Id}", pedido);
            return respuestApi ? RedirectToAction(nameof(Index)) : View();
        }

        public async Task<ActionResult> Delete(int? id)
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            return View(await BuscarPorId(id ?? 0));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var respuestApi = await api.Eliminar($"api/pedidos/{id}");
            return respuestApi ? RedirectToAction(nameof(Index)) : View();
        }


        public async Task<IActionResult> BuscarPedido(string? pedidoId)
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            List<Pedido> pedidos = await api.Obtener($"api/pedidos");
            List<Pedido> resultados = new();

            if (string.IsNullOrEmpty(pedidoId)) return View(pedidos);

            resultados = pedidos
               .Where(i => i.Id.ToString() == pedidoId ||
                           Enum.GetName(i.Estado)!.Contains(pedidoId, StringComparison.OrdinalIgnoreCase))
               .ToList();

            return View(resultados);
        }
    }
}
