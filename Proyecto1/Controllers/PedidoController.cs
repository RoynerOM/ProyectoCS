using Proyecto1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Proyecto1.Controllers
{
    public class PedidoController : Controller
    {
        public static int contadordepedidos = 1;

        const string BASE_URL = "http://localhost:5096/";
        public readonly HttpClient api = new();

        public async Task<Pedido?> BuscarPorId(int Id)
        {
            string json = await api.GetStringAsync(BASE_URL + $"api/pedidos/{Id}");

            Pedido? Inventario = JsonConvert.DeserializeObject<Pedido>(json);

            if (Inventario != null)
            {
                return Inventario;
            }

            return null;
        }

        // GET: PedidoController
        public async Task<ActionResult> Index()
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            string json = await api.GetStringAsync(BASE_URL + "api/pedidos");
            List<Pedido> pedidos = JsonConvert.DeserializeObject<List<Pedido>>(json) ?? new List<Pedido>();
            return View(pedidos);
        }

        // GET: PedidoController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            return View(await BuscarPorId(id));
        }

        // GET: PedidoController/Create
        public async Task<ActionResult> Create()
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            string json = await api.GetStringAsync(BASE_URL + "api/inventario");
            List<Inventario> inventarios = JsonConvert.DeserializeObject<List<Inventario>>(json) ?? new List<Inventario>();

            ViewData["inventarios"] = inventarios;

            return View();
        }

        // POST: PedidoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Pedido pedido)
        {
            pedido.ClienteId = SesionGuardada.Info!.ClienteId;
            var respuestApi = await api.PostAsJsonAsync(BASE_URL + $"api/pedidos", pedido);

            if (respuestApi.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: PedidoController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            string json = await api.GetStringAsync(BASE_URL + "api/inventario");
            List<Inventario> inventarios = JsonConvert.DeserializeObject<List<Inventario>>(json) ?? new List<Inventario>();

            ViewData["inventarios"] = inventarios;

            return View(await BuscarPorId(id));
        }

        // POST: PedidoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Pedido pedido)
        {
            var respuestApi = await api.PutAsJsonAsync(BASE_URL + $"api/pedidos/{pedido.Id}", pedido);

            if (respuestApi.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: PedidoController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            return View(await BuscarPorId(id ?? 0));
        }

        // POST: PedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var respuestApi = await api.DeleteAsync(BASE_URL + $"api/pedidos/{id}");

            if (respuestApi.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        //buscar pedido por ID de producto
        public async Task<IActionResult> BuscarPedido(string? pedidoId)
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            string json = await api.GetStringAsync(BASE_URL + "api/pedidos");
            List<Pedido> pedidos = JsonConvert.DeserializeObject<List<Pedido>>(json) ?? new List<Pedido>();
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
