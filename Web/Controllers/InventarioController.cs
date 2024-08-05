using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class InventarioController : Controller
    {
       readonly ServicioApiBasico<Inventario> http = new();

        public async Task<Inventario?> BuscarPorId(int Id)
        {
            return await http.BuscarPorId($"api/inventario/{Id}");
        }

        public async Task<ActionResult> Index()
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            return View(await http.Obtener($"api/inventario"));
        }

        public async Task<ActionResult> Details(int id)
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            return View(await BuscarPorId(id));
        }

        public ActionResult Create()
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Inventario inventario)
        {
            return await http.Crear($"api/inventario", inventario) ? RedirectToAction(nameof(Index)) : View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            return View(await BuscarPorId(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Inventario inventario)
        {
            var respuestApi = await http.Editar($"api/inventario/{inventario.Id}", inventario);
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
            var respuestApi = await http.Eliminar($"api/inventario/{id}");
            return respuestApi ? RedirectToAction(nameof(Index)) : View();
        }

        public async Task<IActionResult> BuscarArticulo(string idBuscararticulo)
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;

            List<Inventario> inventarios = await http.Obtener($"api/inventario");
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
