using DTOs;
using System.Net.Http.Json;

namespace API.Clients
{
    public class UsuarioApiClient : BaseApiClient
    {
        public static async Task<UsuarioDTO> GetAsync(int id)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.GetAsync($"usuarios/{id}");
                await VerificarRespuestaAsync(response);

                return await response.Content.ReadFromJsonAsync<UsuarioDTO>()
                       ?? throw new Exception($"No se recibieron datos del usuario {id}.");
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("obtener el usuario", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("obtener el usuario", ex);
            }
        }

        public static async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.GetAsync("usuarios");
                await VerificarRespuestaAsync(response);

                return await response.Content.ReadFromJsonAsync<IEnumerable<UsuarioDTO>>()
                       ?? new List<UsuarioDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("obtener los usuarios", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("obtener los usuarios", ex);
            }
        }

        public static async Task AddAsync(UsuarioDTO usuario)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.PostAsJsonAsync("usuarios", usuario);
                await VerificarRespuestaAsync(response);
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("crear el usuario", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("crear el usuario", ex);
            }
        }

        public static async Task UpdateAsync(UsuarioDTO usuario)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.PutAsJsonAsync("usuarios", usuario);
                await VerificarRespuestaAsync(response);
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("actualizar el usuario", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("actualizar el usuario", ex);
            }
        }

        public static async Task DeleteAsync(int id)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.DeleteAsync($"usuarios/{id}");
                await VerificarRespuestaAsync(response);
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("eliminar el usuario", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("eliminar el usuario", ex);
            }
        }
    }
}
