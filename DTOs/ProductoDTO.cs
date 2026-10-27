namespace DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Unidad { get; set; }
        public int Stock { get; set; }
        public int StockMin { get; set; }
        public decimal Precio { get; set; }
        public int Categoria { get; set; }
        public string? CategoriaNombre { get; set; }
        public bool BajoStock { get; set; }
    }
}
