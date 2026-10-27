namespace DTOs
{
    public class ReporteBajoStockDTO
    {
        public DateTime GeneradoEl { get; set; }
        public int CantidadProductos { get; set; }
        public int CantidadIngredientes { get; set; }
        public List<ItemBajoStockDTO> Items { get; set; } = new();
    }

    public class ItemBajoStockDTO
    {
        public string Tipo { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Categoria { get; set; }
        public string? Unidad { get; set; }
        public int Stock { get; set; }
        public int StockMin { get; set; }
        public int Faltante { get; set; }
    }
}
