namespace Domain.Model
{
    public class ProductoCriteria
    {
        public string? Texto { get; private set; }
        public bool? BajoStock { get; private set; }
        public CategoriaProducto? Categoria { get; private set; }

        public ProductoCriteria(string? texto, bool? bajoStock, CategoriaProducto? categoria = null)
        {
            Texto = texto?.Trim();
            BajoStock = bajoStock;

            if (categoria.HasValue && !Enum.IsDefined(typeof(CategoriaProducto), categoria.Value))
                throw new ArgumentException("La categoría indicada no es válida.", nameof(categoria));
            Categoria = categoria;
        }
    }
}
