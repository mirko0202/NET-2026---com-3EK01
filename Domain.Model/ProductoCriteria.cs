namespace Domain.Model
{
    public class ProductoCriteria
    {
        public string? Texto { get; private set; }
        public bool? BajoStock { get; private set; }

        public ProductoCriteria(string? texto, bool? bajoStock)
        {
            Texto = texto?.Trim();
            BajoStock = bajoStock;
        }
    }
}
