using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class ReporteEndpoints
    {
        public static void MapReporteEndpoints(this WebApplication app)
        {
            app.MapGet("/reportes/ventas", async (DateTime desde, DateTime hasta, int? sucursalId,
                                                  IReporteService service) =>
            {
                try
                {
                    var reporte = await service.VentasAsync(desde, hasta, sucursalId);
                    return Results.Ok(reporte);
                }
                catch (ArgumentException ex)
                {
                    return Errores.BadRequest(ex);
                }
            })
            .WithName("ReporteVentas")
            .WithSummary("Ventas por período y sucursal: totales, evolución mensual y productos más vendidos.")
            .Produces<ReporteVentasDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();

            app.MapGet("/reportes/ventas/pdf", async (DateTime desde, DateTime hasta, int? sucursalId,
                                                      IReporteService service) =>
            {
                try
                {
                    var pdf = await service.VentasPdfAsync(desde, hasta, sucursalId);
                    return Results.File(pdf, "application/pdf", $"ventas_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.pdf");
                }
                catch (ArgumentException ex)
                {
                    return Errores.BadRequest(ex);
                }
            })
            .WithName("ReporteVentasPdf")
            .Produces(StatusCodes.Status200OK, contentType: "application/pdf")
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();

            app.MapGet("/reportes/bajo-stock", async (IReporteService service) =>
            {
                var reporte = await service.BajoStockAsync();
                return Results.Ok(reporte);
            })
            .WithName("ReporteBajoStock")
            .WithSummary("Productos e ingredientes por debajo del stock mínimo.")
            .Produces<ReporteBajoStockDTO>(StatusCodes.Status200OK)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();

            app.MapGet("/reportes/bajo-stock/pdf", async (IReporteService service) =>
            {
                var pdf = await service.BajoStockPdfAsync();
                return Results.File(pdf, "application/pdf", $"bajo_stock_{DateTime.Now:yyyyMMdd}.pdf");
            })
            .WithName("ReporteBajoStockPdf")
            .Produces(StatusCodes.Status200OK, contentType: "application/pdf")
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();
        }
    }
}
