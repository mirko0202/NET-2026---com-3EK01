namespace DTOs
{
    public class IngredienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Unidad { get; set; }
        public int Stock { get; set; }
        public int StockMin { get; set; }
        public bool BajoStock { get; set; }
    }
}
