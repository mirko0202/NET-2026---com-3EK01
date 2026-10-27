using DTOs;
using System.Net.Http.Json;

namespace API.Clients
{
    public class IngresoProductoApiClient : BaseApiClient
    {
        public static async Task<IngresoProductoDTO> GetAsync(int id)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.GetAsync($"ingresos/{id}");
                await VerificarRespuestaAsync(response);

                return await response.Content.ReadFromJsonAsync<IngresoProductoDTO>()
                       ?? throw new Exception($"No se recibieron datos del ingreso {id}.");
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("obtener el ingreso", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("obtener el ingreso", ex);
            }
        }

        public static async Task<IEnumerable<IngresoProductoDTO>> GetAllAsync(int? productoId = null,
                                                                             DateTime? desde = null,
                                                                             DateTime? hasta = null)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();

                var filtros = new List<string>();
                if (productoId.HasValue)
                    filtros.Add($"productoId={productoId.Value}");
                if (desde.HasValue)
                    filtros.Add($"desde={Fechas.ParaQuery(desde.Value)}");
                if (hasta.HasValue)
                    filtros.Add($"hasta={Fechas.ParaQuery(hasta.Value)}");

                var url = filtros.Count == 0 ? "ingresos" : $"ingresos?{string.Join("&", filtros)}";
                var response = await client.GetAsync(url);
                await VerificarRespuestaAsync(response);

                return await response.Content.ReadFromJsonAsync<IEnumerable<IngresoProductoDTO>>()
                       ?? new List<IngresoProductoDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("obtener los ingresos de mercadería", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("obtener los ingresos de mercadería", ex);
            }
        }

        public static async Task<IngresoProductoDTO> AddAsync(IngresoProductoDTO ingreso)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.PostAsJsonAsync("ingresos", ingreso);
                await VerificarRespuestaAsync(response);

                return await response.Content.ReadFromJsonAsync<IngresoProductoDTO>()
                       ?? throw new Exception("No se recibieron datos del ingreso registrado.");
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("registrar el ingreso", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("registrar el ingreso", ex);
            }
        }

        public static async Task UpdateAsync(IngresoProductoDTO ingreso)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.PutAsJsonAsync("ingresos", ingreso);
                await VerificarRespuestaAsync(response);
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("actualizar el ingreso", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("actualizar el ingreso", ex);
            }
        }

        public static async Task DeleteAsync(int id)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.DeleteAsync($"ingresos/{id}");
                await VerificarRespuestaAsync(response);
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("eliminar el ingreso", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("eliminar el ingreso", ex);
            }
        }
    }
}
