using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class ClienteController : Controller
    {
        ServicioApiBasico<Cliente> http = new();

        public async Task<Cliente?> BuscarPorId(int Id)
        {
            return await http.BuscarPorId($"api/cliente/{Id}");
        }

        // GET: ClienteController
        public async Task<ActionResult> Index()
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            return View(await http.Obtener($"api/cliente"));
        }

        // GET: ClienteController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await BuscarPorId(id));
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Cliente cliente)
        {
            //cliente.Contrasena = "123456";
            return await http.Crear($"api/cliente", cliente) ? RedirectToAction(nameof(Index)) : View();
        }

        // GET: ClienteController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await BuscarPorId(id));
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Cliente cliente)
        {
            var respuestApi = await http.Editar($"api/cliente/{cliente.Id}", cliente);
            return respuestApi ? RedirectToAction(nameof(Index)) : View();
        }

        // GET: ClienteController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            return View(await BuscarPorId(id ?? 0));
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var respuestApi = await http.Eliminar($"api/cliente/{id}");
            return respuestApi ? RedirectToAction(nameof(Index)) : View();
        }

        //para buscar cliente por ID
        public async Task<ActionResult> BuscarCliente(string idbuscarcliente)
        {
            List<Cliente> clientes = await http.Obtener($"api/cliente");
            List<Cliente> resultados = new();

            if (string.IsNullOrEmpty(idbuscarcliente)) return View(clientes);

            resultados = clientes
               .Where(i => i.Identificacion.ToString().Contains(idbuscarcliente, StringComparison.OrdinalIgnoreCase) ||
                           i.NombreCompleto.Contains(idbuscarcliente, StringComparison.OrdinalIgnoreCase))
               .ToList();

            return View(resultados);
        }
    }
}
