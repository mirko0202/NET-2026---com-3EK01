using DTOs;
using System.Net;
using System.Net.Http.Json;

namespace API.Clients
{
    public class AuthApiClient : BaseApiClient
    {
        public static async Task<LoginResponse> LoginAsync(string username, string password)
        {
            try
            {
                using var client = CreateHttpClient();
                var request = new LoginRequest { Username = username, Password = password };

                var response = await client.PostAsJsonAsync("auth/login", request);

                if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var login = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    return login ?? new LoginResponse { Exito = false, Mensaje = "Respuesta vacía de la API." };
                }

                return new LoginResponse
                {
                    Exito = false,
                    Mensaje = await LeerMensajeDeErrorAsync(response)
                };
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("iniciar sesión", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("iniciar sesión", ex);
            }
        }
    }
}
