using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace API.Clients
{
    public abstract class BaseApiClient
    {
        private const string UrlPorDefecto = "http://localhost:5080/";

        public const string MensajeSesionExpirada = "Su sesión expiró. Vuelva a iniciar sesión.";

        protected static async Task<HttpClient> CreateHttpClientAsync()
        {
            var client = new HttpClient { BaseAddress = new Uri(ObtenerUrlBase()) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(30);

            var token = await AuthServiceProvider.Instance.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        protected static async Task<HttpClient> CreateAuthenticatedHttpClientAsync()
        {
            var authService = AuthServiceProvider.Instance;

            await authService.CheckTokenExpirationAsync();

            if (!await authService.IsAuthenticatedAsync())
                throw new UnauthorizedAccessException(MensajeSesionExpirada);

            return await CreateHttpClientAsync();
        }

        protected static async Task VerificarRespuestaAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await AuthServiceProvider.Instance.LogoutAsync();
                throw new UnauthorizedAccessException(MensajeSesionExpirada);
            }

            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new Exception("No tiene permisos para realizar esta operación.");

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception("El registro solicitado no existe o fue eliminado.");

            throw new Exception(await LeerMensajeDeErrorAsync(response));
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
