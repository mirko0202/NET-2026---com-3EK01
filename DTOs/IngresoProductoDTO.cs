namespace DTOs
{
    public class IngresoProductoDTO
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public int ProductoId { get; set; }
        public string? ProductoNombre { get; set; }
        public string? Unidad { get; set; }
        public int SucursalId { get; set; }
        public string? SucursalNombre { get; set; }
        public int UsuarioId { get; set; }
        public string? UsuarioNombre { get; set; }
        public int Cantidad { get; set; }
        public string Proveedor { get; set; } = string.Empty;
        public string? Comprobante { get; set; }
        public string? Observacion { get; set; }
    }
}
