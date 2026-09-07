using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace API.Clients
{
    public abstract class BaseApiClient
    {
        private const string UrlPorDefecto = "http://localhost:5080/";

        protected static HttpClient CreateHttpClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(ObtenerUrlBase()) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(30);
            return client;
        }

        private static string ObtenerUrlBase()
        {
            var url = Environment.GetEnvironmentVariable("TPI_API_BASE_URL");
            return string.IsNullOrWhiteSpace(url) ? UrlPorDefecto : url;
        }

        protected static async Task<string> LeerMensajeDeErrorAsync(HttpResponseMessage response)
        {
            try
            {
                var contenido = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(contenido))
                    return $"La API respondió {(int)response.StatusCode} ({response.StatusCode}).";

                using var doc = JsonDocument.Parse(contenido);
                if (doc.RootElement.ValueKind == JsonValueKind.Object &&
                    doc.RootElement.TryGetProperty("error", out var error))
                {
                    return error.GetString() ?? contenido;
                }

                return contenido;
            }
            catch (JsonException)
            {
                return $"La API respondió {(int)response.StatusCode} ({response.StatusCode}).";
            }
        }

        protected static Exception ErrorDeConexion(string accion, Exception ex) =>
            new Exception(
                $"No se pudo conectar con la API para {accion}. " +
                $"Verifique que el proyecto WebAPI esté en ejecución. Detalle: {ex.Message}", ex);
    }
}
