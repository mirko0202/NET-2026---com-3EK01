using DTOs;
using System.Net.Http.Json;

namespace API.Clients
{
    public class SucursalApiClient : BaseApiClient
    {
        public static async Task<IEnumerable<SucursalDTO>> GetAllAsync()
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.GetAsync("sucursales");
                await VerificarRespuestaAsync(response);

                return await response.Content.ReadFromJsonAsync<IEnumerable<SucursalDTO>>()
                       ?? new List<SucursalDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("obtener las sucursales", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("obtener las sucursales", ex);
            }
        }
    }
}
