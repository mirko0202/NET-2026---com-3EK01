using Data;
using Domain.Model;
using DTOs;
using System.Globalization;

namespace Application.Services
{
    public class ReporteService : IReporteService
    {
        private static readonly CultureInfo cultura = new("es-AR");

        private readonly IVentaRepository ventaRepository;
        private readonly IProductoRepository productoRepository;
        private readonly IIngredienteRepository ingredienteRepository;
        private readonly ISucursalRepository sucursalRepository;

        public ReporteService(IVentaRepository ventaRepository,
                              IProductoRepository productoRepository,
                              IIngredienteRepository ingredienteRepository,
                              ISucursalRepository sucursalRepository)
        {
            this.ventaRepository = ventaRepository;
            this.productoRepository = productoRepository;
            this.ingredienteRepository = ingredienteRepository;
            this.sucursalRepository = sucursalRepository;
        }

        public async Task<ReporteVentasDTO> VentasAsync(DateTime desde, DateTime hasta, int? sucursalId)
        {
            if (desde.Date > hasta.Date)
                throw new ArgumentException("La fecha desde no puede ser posterior a la fecha hasta.");

            if ((hasta.Date - desde.Date).TotalDays > 731)
                throw new ArgumentException("El período del reporte no puede superar los dos años.");

            List<Sucursal> sucursales;
            var nombreSucursal = "Todas las sucursales";

            if (sucursalId.HasValue)
            {
                var sucursal = await sucursalRepository.GetAsync(sucursalId.Value)
                    ?? throw new ArgumentException("La sucursal indicada no existe.");

                sucursales = new List<Sucursal> { sucursal };
                nombreSucursal = sucursal.Nombre;
            }
            else
            {
                sucursales = (await sucursalRepository.GetAllAsync()).ToList();
            }

            var ventas = (await ventaRepository.GetByPeriodoAsync(desde, hasta, sucursalId)).ToList();

            var reporte = new ReporteVentasDTO
            {
                Desde = desde.Date,
                Hasta = hasta.Date,
                SucursalId = sucursalId,
                SucursalNombre = nombreSucursal,
                CantidadVentas = ventas.Count,
                Unidades = ventas.Sum(v => v.Detalles.Sum(d => d.Cantidad)),
                Total = ventas.Sum(v => v.Total)
            };

            reporte.TicketPromedio = ventas.Count == 0 ? 0 : decimal.Round(reporte.Total / ventas.Count, 2);

            reporte.PorSucursal = sucursales
                .Select(s =>
                {
                    var propias = ventas.Where(v => v.SucursalId == s.Id).ToList();
                    return new
                    {
                        s.Activa,
                        Resumen = new VentasPorSucursalDTO
                        {
                            SucursalId = s.Id,
                            SucursalNombre = s.Nombre,
                            CantidadVentas = propias.Count,
                            Unidades = propias.Sum(v => v.Detalles.Sum(d => d.Cantidad)),
                            Total = propias.Sum(v => v.Total)
                        }
                    };
                })
                .Where(x => x.Activa || x.Resumen.CantidadVentas > 0)
                .Select(x => x.Resumen)
                .OrderByDescending(x => x.Total)
                .ToList();

            var meses = new List<DateTime>();
            for (var mes = new DateTime(desde.Year, desde.Month, 1); mes <= hasta.Date; mes = mes.AddMonths(1))
                meses.Add(mes);

            reporte.PorMes = (
                from mes in meses
                from sucursal in reporte.PorSucursal
                select new VentasPorMesDTO
                {
                    Anio = mes.Year,
                    Mes = mes.Month,
                    Periodo = NombreDelMes(mes),
                    SucursalId = sucursal.SucursalId,
                    SucursalNombre = sucursal.SucursalNombre,
                    Total = ventas
                        .Where(v => v.SucursalId == sucursal.SucursalId &&
                                    v.FechaHora.Year == mes.Year && v.FechaHora.Month == mes.Month)
                        .Sum(v => v.Total)
                })
                .ToList();

            reporte.ProductosMasVendidos = ventas
                .SelectMany(v => v.Detalles)
                .GroupBy(d => d.ProductoId)
                .Select(g => new ProductoVendidoDTO
                {
                    ProductoId = g.Key,
                    ProductoNombre = g.First().Producto?.Nombre ?? $"Producto {g.Key}",
                    Unidades = g.Sum(d => d.Cantidad),
                    Total = g.Sum(d => d.Subtotal)
                })
                .OrderByDescending(p => p.Total)
                .Take(5)
                .ToList();

            return reporte;
        }

        public async Task<ReporteBajoStockDTO> BajoStockAsync()
        {
            var productos = await productoRepository.GetByCriteriaAsync(new ProductoCriteria(null, true));
            var ingredientes = (await ingredienteRepository.GetAllAsync()).Where(i => i.BajoStock);

            var items = productos
                .Select(p => new ItemBajoStockDTO
                {
                    Tipo = "Producto",
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Categoria = p.Categoria.Nombre(),
                    Unidad = p.Unidad,
                    Stock = p.Stock,
                    StockMin = p.StockMin,
                    Faltante = Math.Max(0, p.StockMin - p.Stock)
                })
                .Concat(ingredientes.Select(i => new ItemBajoStockDTO
                {
                    Tipo = "Ingrediente",
                    Id = i.Id,
                    Nombre = i.Nombre,
                    Unidad = i.Unidad,
                    Stock = i.Stock,
                    StockMin = i.StockMin,
                    Faltante = Math.Max(0, i.StockMin - i.Stock)
                }))
                .OrderBy(i => i.Tipo)
                .ThenByDescending(i => i.Faltante)
                .ThenBy(i => i.Nombre)
                .ToList();

            return new ReporteBajoStockDTO
            {
                GeneradoEl = DateTime.Now,
                CantidadProductos = items.Count(i => i.Tipo == "Producto"),
                CantidadIngredientes = items.Count(i => i.Tipo == "Ingrediente"),
                Items = items
            };
        }

        public async Task<byte[]> VentasPdfAsync(DateTime desde, DateTime hasta, int? sucursalId) =>
            ReportesPdf.Ventas(await VentasAsync(desde, hasta, sucursalId));

        public async Task<byte[]> BajoStockPdfAsync() =>
            ReportesPdf.BajoStock(await BajoStockAsync());

        private static string NombreDelMes(DateTime mes)
        {
            var nombre = cultura.DateTimeFormat.GetMonthName(mes.Month);
            return $"{char.ToUpper(nombre[0], cultura)}{nombre[1..]} {mes.Year}";
        }
    }
}
