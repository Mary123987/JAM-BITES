using System;
using System.Net.Http;
using System.Threading.Tasks;
using JAM_BITES.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;

namespace JAM_BITES.Services
{
    public class ServicioNoticias : IServicioNoticias
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private int _requestCount;
        private DateTime _requestWindowStart;
        private const int _requestLimit = 25; // Límite de solicitudes por ventana
        private const int _timeWindowInSeconds = 60; // Ventana de tiempo en segundos

        public ServicioNoticias(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
            _requestCount = 0;
            _requestWindowStart = DateTime.UtcNow;
        }

        public async Task<RespuestaNoticias> ObtenerNoticiasAsync()
        {
            // Controlar la ventana de solicitudes
            if (DateTime.UtcNow > _requestWindowStart.AddSeconds(_timeWindowInSeconds))
            {
                _requestCount = 0; // Reiniciar el contador después de que la ventana haya pasado
                _requestWindowStart = DateTime.UtcNow;
            }

            if (_requestCount >= _requestLimit)
            {
                throw new Exception("Límite de solicitudes alcanzado. Intente nuevamente más tarde."); // Manejo simple de límites
            }

            // Definir la clave de caché
            string cacheKey = "NoticiasCache";
            if (_cache.TryGetValue(cacheKey, out RespuestaNoticias cachedResponse))
            {
                return cachedResponse; // Retorna la respuesta de caché
            }

            var request = new HttpRequestMessage(HttpMethod.Get, "https://google-news13.p.rapidapi.com/entertainment?lr=es-PE");
            request.Headers.Add("X-RapidAPI-Key", "a71e17a7d3msheacbb6c349b32b9p16d102jsn992796c45650");
            request.Headers.Add("X-RapidAPI-Host", "google-news13.p.rapidapi.com");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var noticias = JsonConvert.DeserializeObject<RespuestaNoticias>(jsonResponse) ?? new RespuestaNoticias();

            // Almacenar en caché con un tiempo de expiración de 10 minutos
            _cache.Set(cacheKey, noticias, TimeSpan.FromMinutes(10));

            _requestCount++; // Incrementar el contador de solicitudes
            return noticias;
        }
    }
}
