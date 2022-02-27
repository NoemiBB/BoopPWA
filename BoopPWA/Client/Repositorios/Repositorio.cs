using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BoopPWA.Client.Repositorios
{
    public class Repositorio : IRepositorio
    {
        private readonly HttpClient httpClient;
        public Repositorio(HttpClient http)
        {
            httpClient = http;
        }
        private JsonSerializerOptions OpcionesPorDefectoJSON =>
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var responseHTTP = await httpClient.GetAsync(url);

            if (responseHTTP.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<T>(responseHTTP, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<T>(response, false, responseHTTP);
            }
            else
            {
                return new HttpResponseWrapper<T>(default, true, responseHTTP);
            }
        } 
        //public async Task<HttpResponseWrapper<T>> Get<T>(string url, Filtros filtro)
        //{
        //    var filtroJSON = JsonSerializer.Serialize(filtro);
        //    var enviarFiltro = new StringContent(filtroJSON, Encoding.UTF8, "application/json");
        //    var responseHTTP = await httpClient.GetAsync(url, enviarFiltro);

        //    if (responseHTTP.IsSuccessStatusCode)
        //    {
        //        var response = await DeserializarRespuesta<T>(responseHTTP, OpcionesPorDefectoJSON);
        //        return new HttpResponseWrapper<T>(response, false, responseHTTP);
        //    }
        //    else
        //    {
        //        return new HttpResponseWrapper<T>(default, true, responseHTTP);
        //    }
        //}
        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<IEnumerable<T>> GetAll<T>(string url)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<T>>(
                await httpClient.GetStreamAsync(url), OpcionesPorDefectoJSON);
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PutAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }
        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var responseHTTP = await httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, !responseHTTP.IsSuccessStatusCode, responseHTTP);
        }
        private async Task<T> DeserializarRespuesta<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString, jsonSerializerOptions);
        }
    }
}

        //public List<Alumno> ObtenerAlumnos()
        //{
        //    return new List<Alumno>()
        //    {
        //        new Alumno() {Id = 1, Nombre = "Pablo",Examenes = { }},
        //        new Alumno() {Id = 2, Nombre = "Silvia",Examenes = { }},
        //        new Alumno() {Id = 3, Nombre = "Sara",Examenes = { }},
        //    };
        //}