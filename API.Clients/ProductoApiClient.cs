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
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.GetAsync($"productos/{id}");
                await VerificarRespuestaAsync(response);

                return await response.Content.ReadFromJsonAsync<ProductoDTO>()
                       ?? throw new Exception($"No se recibieron datos del producto {id}.");
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
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.GetAsync("productos");
                await VerificarRespuestaAsync(response);

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

        public static async Task<IEnumerable<ProductoDTO>> GetByCriteriaAsync(string? texto, bool bajoStock, int? categoria = null)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();

                var query = $"productos/criteria?bajoStock={(bajoStock ? "true" : "false")}";
                if (!string.IsNullOrWhiteSpace(texto))
                    query += $"&texto={Uri.EscapeDataString(texto)}";
                if (categoria.HasValue)
                    query += $"&categoria={categoria.Value}";

                var response = await client.GetAsync(query);
                await VerificarRespuestaAsync(response);

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

        public static async Task<IEnumerable<CategoriaDTO>> GetCategoriasAsync()
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.GetAsync("productos/categorias");
                await VerificarRespuestaAsync(response);

                return await response.Content.ReadFromJsonAsync<IEnumerable<CategoriaDTO>>()
                       ?? new List<CategoriaDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("obtener las categorías", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("obtener las categorías", ex);
            }
        }

        public static async Task AddAsync(ProductoDTO producto)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.PostAsJsonAsync("productos", producto);
                await VerificarRespuestaAsync(response);
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
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.PutAsJsonAsync("productos", producto);
                await VerificarRespuestaAsync(response);
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
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.DeleteAsync($"productos/{id}");
                await VerificarRespuestaAsync(response);
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
