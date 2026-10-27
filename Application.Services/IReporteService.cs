using DTOs;

namespace Application.Services
{
    public interface IReporteService
    {
        Task<ReporteVentasDTO> VentasAsync(DateTime desde, DateTime hasta, int? sucursalId);
        Task<ReporteBajoStockDTO> BajoStockAsync();
        Task<byte[]> VentasPdfAsync(DateTime desde, DateTime hasta, int? sucursalId);
        Task<byte[]> BajoStockPdfAsync();
    }
}
