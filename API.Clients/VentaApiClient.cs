using DTOs;
using System.Net.Http.Json;

namespace API.Clients
{
    public class VentaApiClient : BaseApiClient
    {
        public static async Task<VentaDTO> GetAsync(int id)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.GetAsync($"ventas/{id}");
                await VerificarRespuestaAsync(response);

                return await response.Content.ReadFromJsonAsync<VentaDTO>()
                       ?? throw new Exception($"No se recibieron datos de la venta {id}.");
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("obtener la venta", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("obtener la venta", ex);
            }
        }

        public static async Task<IEnumerable<VentaDTO>> GetAllAsync()
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.GetAsync("ventas");
                await VerificarRespuestaAsync(response);

                return await response.Content.ReadFromJsonAsync<IEnumerable<VentaDTO>>()
                       ?? new List<VentaDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("obtener las ventas", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("obtener las ventas", ex);
            }
        }

        public static async Task<VentaDTO> AddAsync(VentaDTO venta)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.PostAsJsonAsync("ventas", venta);
                await VerificarRespuestaAsync(response);

                return await response.Content.ReadFromJsonAsync<VentaDTO>()
                       ?? throw new Exception("No se recibieron datos de la venta registrada.");
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("registrar la venta", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("registrar la venta", ex);
            }
        }

        public static async Task UpdateAsync(VentaDTO venta)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.PutAsJsonAsync("ventas", venta);
                await VerificarRespuestaAsync(response);
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("actualizar la venta", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("actualizar la venta", ex);
            }
        }

        public static async Task DeleteAsync(int id)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.DeleteAsync($"ventas/{id}");
                await VerificarRespuestaAsync(response);
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("eliminar la venta", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("eliminar la venta", ex);
            }
        }
    }
}
