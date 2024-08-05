using Newtonsoft.Json;
using Proyecto1.Models;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;

namespace Proyecto1
{
    public class ServicioApiBasico<T>
    {
        private readonly HttpClient _http;
        private const string BASE_URL = "http://localhost:5096/";

        public ServicioApiBasico()
        {
            _http = new HttpClient();
        }

        public async Task<List<T>> Obtener(string apiRuta)
        {
            return await _http.GetFromJsonAsync<List<T>>(BASE_URL + apiRuta) ?? new List<T>();
        }

        public async Task<T?> BuscarPorId(string apiRuta)
        {
            return await _http.GetFromJsonAsync<T>(BASE_URL + apiRuta);
        }

        public async Task<bool> Crear(string apiRuta, T t)
        {
            var response = await _http.PostAsJsonAsync(BASE_URL + apiRuta, t);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Editar(string apiRuta, T t)
        {
            var response = await _http.PutAsJsonAsync(BASE_URL + apiRuta, t);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Eliminar(string apiRuta)
        {
            var response = await _http.DeleteAsync(BASE_URL + apiRuta);
            return response.IsSuccessStatusCode;
        }

        public async Task<Sesion?> Login(string apiRuta)
        {
            var json = await _http.GetStringAsync(BASE_URL + apiRuta);
            var sesion = JsonConvert.DeserializeObject<Sesion>(json);
            return sesion;
        }

        public async Task<Sesion?> BuscarSesion(Guid sesionId)
        {
            return await _http.GetFromJsonAsync<Sesion>(BASE_URL + $"api/cliente/sesion/{sesionId}");
        }
    }
}
