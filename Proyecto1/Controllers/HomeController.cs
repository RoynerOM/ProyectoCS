using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using Proyecto1.Models;
using System.Diagnostics;
using System.Text;

namespace Proyecto1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        ServicioApiBasico<Cliente> http = new();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["SesionId"] = SesionGuardada.Info?.Id;
            ViewData["ClienteId"] = SesionGuardada.Info?.ClienteId;
            return View();
        }

        public IActionResult Login()
        {
            if (SesionGuardada.Info != null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string id, string contrasena)
        {
            var usuario = $"{id} {contrasena}";
            var login = await http.Login($"api/Cliente/Login/{usuario}");

            if (login != null)
            {
                SesionGuardada.Info = login;
                ViewData["SesionId"] = login.Id;
                ViewData["ClienteId"] = login.ClienteId;
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Salir()
        {
            SesionGuardada.Info = null;
            ViewData["SesionId"] = null;
            ViewData["ClienteId"] = null;

            return RedirectToAction(nameof(Login));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
