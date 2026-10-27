using DTOs;
using System.Net.Http.Json;

namespace API.Clients
{
    public class ReporteApiClient : BaseApiClient
    {
        public static async Task<ReporteVentasDTO> GetVentasAsync(DateTime desde, DateTime hasta, int? sucursalId)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.GetAsync(UrlVentas("reportes/ventas", desde, hasta, sucursalId));
                await VerificarRespuestaAsync(response);

                return await response.Content.ReadFromJsonAsync<ReporteVentasDTO>()
                       ?? throw new Exception("No se recibieron datos del reporte de ventas.");
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("generar el reporte de ventas", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("generar el reporte de ventas", ex);
            }
        }

        public static async Task<byte[]> GetVentasPdfAsync(DateTime desde, DateTime hasta, int? sucursalId)
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.GetAsync(UrlVentas("reportes/ventas/pdf", desde, hasta, sucursalId));
                await VerificarRespuestaAsync(response);

                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("exportar el reporte de ventas", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("exportar el reporte de ventas", ex);
            }
        }

        public static async Task<ReporteBajoStockDTO> GetBajoStockAsync()
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.GetAsync("reportes/bajo-stock");
                await VerificarRespuestaAsync(response);

                return await response.Content.ReadFromJsonAsync<ReporteBajoStockDTO>()
                       ?? throw new Exception("No se recibieron datos del reporte de bajo stock.");
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("generar el reporte de bajo stock", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("generar el reporte de bajo stock", ex);
            }
        }

        public static async Task<byte[]> GetBajoStockPdfAsync()
        {
            try
            {
                using var client = await CreateAuthenticatedHttpClientAsync();
                var response = await client.GetAsync("reportes/bajo-stock/pdf");
                await VerificarRespuestaAsync(response);

                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (HttpRequestException ex)
            {
                throw ErrorDeConexion("exportar el reporte de bajo stock", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw ErrorDeConexion("exportar el reporte de bajo stock", ex);
            }
        }

        private static string UrlVentas(string ruta, DateTime desde, DateTime hasta, int? sucursalId)
        {
            var url = $"{ruta}?desde={Fechas.ParaQuery(desde)}&hasta={Fechas.ParaQuery(hasta)}";
            return sucursalId.HasValue ? $"{url}&sucursalId={sucursalId.Value}" : url;
        }
    }
}
