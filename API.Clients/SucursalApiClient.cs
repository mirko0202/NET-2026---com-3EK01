using DTOs;
using System.Net.Http.Json;

namespace API.Clients
{
    public class SucursalApiClient : BaseApiClient
    {
        public static async Task<SucursalDTO> GetAsync(int id)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.GetAsync($"sucursales/{id}");
                await VerificarRespuestaAsync(response);

                return await response.Content.ReadFromJsonAsync<SucursalDTO>()
                       ?? throw new Exception($"No se recibieron datos de la sucursal {id}.");
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("obtener la sucursal", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("obtener la sucursal", ex);
            }
        }

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

        public static async Task AddAsync(SucursalDTO sucursal)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.PostAsJsonAsync("sucursales", sucursal);
                await VerificarRespuestaAsync(response);
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("crear la sucursal", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("crear la sucursal", ex);
            }
        }

        public static async Task UpdateAsync(SucursalDTO sucursal)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.PutAsJsonAsync("sucursales", sucursal);
                await VerificarRespuestaAsync(response);
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("actualizar la sucursal", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("actualizar la sucursal", ex);
            }
        }

        public static async Task DeleteAsync(int id)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.DeleteAsync($"sucursales/{id}");
                await VerificarRespuestaAsync(response);
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("eliminar la sucursal", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("eliminar la sucursal", ex);
            }
        }
    }
}
