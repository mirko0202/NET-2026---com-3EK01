namespace DTOs
{
    public class ReporteVentasDTO
    {
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public int? SucursalId { get; set; }
        public string SucursalNombre { get; set; } = "Todas las sucursales";
        public int CantidadVentas { get; set; }
        public int Unidades { get; set; }
        public decimal Total { get; set; }
        public decimal TicketPromedio { get; set; }
        public List<VentasPorSucursalDTO> PorSucursal { get; set; } = new();
        public List<VentasPorMesDTO> PorMes { get; set; } = new();
        public List<ProductoVendidoDTO> ProductosMasVendidos { get; set; } = new();
    }

    public class VentasPorSucursalDTO
    {
        public int SucursalId { get; set; }
        public string SucursalNombre { get; set; } = string.Empty;
        public int CantidadVentas { get; set; }
        public int Unidades { get; set; }
        public decimal Total { get; set; }
    }

    public class VentasPorMesDTO
    {
        public int Anio { get; set; }
        public int Mes { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public int SucursalId { get; set; }
        public string SucursalNombre { get; set; } = string.Empty;
        public decimal Total { get; set; }
    }

    public class ProductoVendidoDTO
    {
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; } = string.Empty;
        public int Unidades { get; set; }
        public decimal Total { get; set; }
    }
}
