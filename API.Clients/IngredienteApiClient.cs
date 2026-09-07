using DTOs;
using System.Net.Http.Json;

namespace API.Clients
{
    public class IngredienteApiClient : BaseApiClient
    {
        public static async Task<IngredienteDTO> GetAsync(int id)
        {
            try
            {
                using var client = CreateHttpClient();
                var response = await client.GetAsync($"ingredientes/{id}");

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error al obtener el ingrediente {id}. {await LeerMensajeDeErrorAsync(response)}");

                var ingrediente = await response.Content.ReadFromJsonAsync<IngredienteDTO>();
                return ingrediente ?? throw new Exception($"No se recibieron datos del ingrediente {id}.");
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("obtener el ingrediente", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("obtener el ingrediente", ex);
            }
        }

        public static async Task<IEnumerable<IngredienteDTO>> GetAllAsync()
        {
            try
            {
                using var client = CreateHttpClient();
                var response = await client.GetAsync("ingredientes");

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error al obtener los ingredientes. {await LeerMensajeDeErrorAsync(response)}");

                return await response.Content.ReadFromJsonAsync<IEnumerable<IngredienteDTO>>()
                       ?? new List<IngredienteDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("obtener los ingredientes", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("obtener los ingredientes", ex);
            }
        }

        public static async Task AddAsync(IngredienteDTO ingrediente)
        {
            try
            {
                using var client = CreateHttpClient();
                var response = await client.PostAsJsonAsync("ingredientes", ingrediente);

                if (!response.IsSuccessStatusCode)
                    throw new Exception(await LeerMensajeDeErrorAsync(response));
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("crear el ingrediente", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("crear el ingrediente", ex);
            }
        }

        public static async Task UpdateAsync(IngredienteDTO ingrediente)
        {
            try
            {
                using var client = CreateHttpClient();
                var response = await client.PutAsJsonAsync("ingredientes", ingrediente);

                if (!response.IsSuccessStatusCode)
                    throw new Exception(await LeerMensajeDeErrorAsync(response));
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("actualizar el ingrediente", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("actualizar el ingrediente", ex);
            }
        }

        public static async Task DeleteAsync(int id)
        {
            try
            {
                using var client = CreateHttpClient();
                var response = await client.DeleteAsync($"ingredientes/{id}");

                if (!response.IsSuccessStatusCode)
                    throw new Exception(await LeerMensajeDeErrorAsync(response));
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("eliminar el ingrediente", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("eliminar el ingrediente", ex);
            }
        }
    }
}
