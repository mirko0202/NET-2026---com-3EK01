using DTOs;
using System.Net.Http.Json;

namespace API.Clients
{
    public class ProductoApiClient : BaseApiClient
    {
        public static async Task<ProductoDTO> GetAsync(int id)
        {
            try
            {
                using var client = CreateHttpClient();
                var response = await client.GetAsync($"productos/{id}");

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error al obtener el producto {id}. {await LeerMensajeDeErrorAsync(response)}");

                var producto = await response.Content.ReadFromJsonAsync<ProductoDTO>();
                return producto ?? throw new Exception($"No se recibieron datos del producto {id}.");
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("obtener el producto", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("obtener el producto", ex);
            }
        }

        public static async Task<IEnumerable<ProductoDTO>> GetAllAsync()
        {
            try
            {
                using var client = CreateHttpClient();
                var response = await client.GetAsync("productos");

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error al obtener los productos. {await LeerMensajeDeErrorAsync(response)}");

                return await response.Content.ReadFromJsonAsync<IEnumerable<ProductoDTO>>()
                       ?? new List<ProductoDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("obtener los productos", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("obtener los productos", ex);
            }
        }

        public static async Task<IEnumerable<ProductoDTO>> GetByCriteriaAsync(string? texto, bool bajoStock)
        {
            try
            {
                using var client = CreateHttpClient();

                var query = $"productos/criteria?bajoStock={(bajoStock ? "true" : "false")}";
                if (!string.IsNullOrWhiteSpace(texto))
                    query += $"&texto={Uri.EscapeDataString(texto)}";

                var response = await client.GetAsync(query);

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error al buscar productos. {await LeerMensajeDeErrorAsync(response)}");

                return await response.Content.ReadFromJsonAsync<IEnumerable<ProductoDTO>>()
                       ?? new List<ProductoDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("buscar productos", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("buscar productos", ex);
            }
        }

        public static async Task AddAsync(ProductoDTO producto)
        {
            try
            {
                using var client = CreateHttpClient();
                var response = await client.PostAsJsonAsync("productos", producto);

                if (!response.IsSuccessStatusCode)
                    throw new Exception(await LeerMensajeDeErrorAsync(response));
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("crear el producto", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("crear el producto", ex);
            }
        }

        public static async Task UpdateAsync(ProductoDTO producto)
        {
            try
            {
                using var client = CreateHttpClient();
                var response = await client.PutAsJsonAsync("productos", producto);

                if (!response.IsSuccessStatusCode)
                    throw new Exception(await LeerMensajeDeErrorAsync(response));
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("actualizar el producto", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("actualizar el producto", ex);
            }
        }

        public static async Task DeleteAsync(int id)
        {
            try
            {
                using var client = CreateHttpClient();
                var response = await client.DeleteAsync($"productos/{id}");

                if (!response.IsSuccessStatusCode)
                    throw new Exception(await LeerMensajeDeErrorAsync(response));
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("eliminar el producto", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("eliminar el producto", ex);
            }
        }
    }
}
