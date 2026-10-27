namespace DTOs
{
    public class VentaDTO
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public int UsuarioId { get; set; }
        public string? UsuarioNombre { get; set; }
        public int SucursalId { get; set; }
        public string? SucursalNombre { get; set; }
        public List<DetalleVentaDTO> Detalles { get; set; } = new();

        public int CantidadProductos => Detalles.Sum(d => d.Cantidad);
        public decimal Total => Detalles.Sum(d => d.Subtotal);
    }
}
