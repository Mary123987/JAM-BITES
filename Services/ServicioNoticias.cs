using System.Net.Http;
using System.Threading.Tasks;
using JAM_BITES.Models;
using Newtonsoft.Json;

namespace JAM_BITES.Services
{
    public class ServicioNoticias : IServicioNoticias
    {
        private readonly HttpClient _httpClient;

        public ServicioNoticias(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RespuestaNoticias> ObtenerNoticiasAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://google-news13.p.rapidapi.com/entertainment?lr=es-PE");
            request.Headers.Add("X-RapidAPI-Key", "3cabee7fedmsh3da221566ef9832p1a6610jsndfe7371f7b94");
            request.Headers.Add("X-RapidAPI-Host", "google-news13.p.rapidapi.com");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var noticias = JsonConvert.DeserializeObject<RespuestaNoticias>(jsonResponse) ?? new RespuestaNoticias();

            return noticias;
        }
    }
}
