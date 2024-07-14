using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class InventarioController : Controller
    {
        const string BASE_URL = "http://localhost:5096/";
        public readonly HttpClient api = new();

        public async Task<Inventario?> BuscarPorId(int Id)
        {
            string json = await api.GetStringAsync(BASE_URL + $"api/inventario/{Id}");

            Inventario? Inventario = JsonConvert.DeserializeObject<Inventario>(json);

            if (Inventario != null)
            {
                return Inventario;
            }

            return null;
        }

        // GET: InventarioController
        public async Task<ActionResult> Index()
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            string json = await api.GetStringAsync(BASE_URL + "api/inventario");
            List<Inventario> inventarios = JsonConvert.DeserializeObject<List<Inventario>>(json) ?? new List<Inventario>();
            return View(inventarios);
        }

        // GET: InventarioController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            return View(await BuscarPorId(id));
        }

        // GET: InventarioController/Create
        public ActionResult Create()
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            return View();
        }

        // POST: InventarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Inventario inventario)
        {
            string serializeObject = JsonConvert.SerializeObject(inventario);

            var respuestApi = await api.PostAsJsonAsync(BASE_URL + $"api/inventario", inventario);

            if (respuestApi.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: InventarioController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            return View(await BuscarPorId(id));
        }

        // POST: InventarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Inventario inventario)
        {
            string serializeObject = JsonConvert.SerializeObject(inventario);

            var respuestApi = await api.PutAsJsonAsync(BASE_URL + $"api/inventario/{inventario.Id}", inventario);

            if (respuestApi.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: InventarioController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            return View(await BuscarPorId(id ?? 0));
        }

        // POST: InventarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var respuestApi = await api.DeleteAsync(BASE_URL + $"api/inventario/{id}");

            if (respuestApi.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        //Funcion para buscar articulo de inventario
        public async Task<IActionResult> BuscarArticulo(string idBuscararticulo)
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;

            string json = await api.GetStringAsync(BASE_URL + "api/inventario");
            List<Inventario> inventarios = JsonConvert.DeserializeObject<List<Inventario>>(json) ?? new List<Inventario>();
            List<Inventario> resultados = new();

            if (string.IsNullOrEmpty(idBuscararticulo)) return View(inventarios);

            resultados = inventarios
               .Where(i => i.Id.ToString() == idBuscararticulo ||
                          Enum.GetName(i.TipoLicor)!.Contains(idBuscararticulo, StringComparison.OrdinalIgnoreCase))
               .ToList();

            return View(resultados);
        }
    }
}
