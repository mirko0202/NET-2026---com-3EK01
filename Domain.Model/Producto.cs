namespace Domain.Model
{
    public class Producto
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public string? Descripcion { get; private set; }
        public string? Unidad { get; private set; }
        public int Stock { get; private set; }
        public int StockMin { get; private set; }
        public decimal Precio { get; private set; }
        public CategoriaProducto Categoria { get; private set; }

        public bool BajoStock => Stock <= StockMin;

        public Producto(int id, string nombre, string? descripcion, string? unidad,
                        int stock, int stockMin, decimal precio, CategoriaProducto categoria)
        {
            SetId(id);
            SetNombre(nombre);
            SetDescripcion(descripcion);
            SetUnidad(unidad);
            SetStock(stock);
            SetStockMin(stockMin);
            SetPrecio(precio);
            SetCategoria(categoria);
        }

        private Producto() { }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor o igual a 0.", nameof(id));
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombre));
            if (nombre.Length > 100)
                throw new ArgumentException("El nombre no puede superar los 100 caracteres.", nameof(nombre));
            Nombre = nombre.Trim();
        }

        public void SetDescripcion(string? descripcion)
        {
            if (descripcion != null && descripcion.Length > 255)
                throw new ArgumentException("La descripción no puede superar los 255 caracteres.", nameof(descripcion));
            Descripcion = descripcion;
        }

        public void SetUnidad(string? unidad)
        {
            if (unidad != null && unidad.Length > 20)
                throw new ArgumentException("La unidad no puede superar los 20 caracteres.", nameof(unidad));
            Unidad = unidad;
        }

        public void SetStock(int stock)
        {
            if (stock < 0)
                throw new ArgumentException("El stock no puede ser negativo.", nameof(stock));
            Stock = stock;
        }

        public void SetStockMin(int stockMin)
        {
            if (stockMin < 0)
                throw new ArgumentException("El stock mínimo no puede ser negativo.", nameof(stockMin));
            StockMin = stockMin;
        }

        public void SetPrecio(decimal precio)
        {
            if (precio < 0)
                throw new ArgumentException("El precio no puede ser negativo.", nameof(precio));
            if (precio > 10_000_000)
                throw new ArgumentException("El precio no puede superar los $ 10.000.000.", nameof(precio));
            Precio = decimal.Round(precio, 2);
        }

        public void SetCategoria(CategoriaProducto categoria)
        {
            if (!Enum.IsDefined(typeof(CategoriaProducto), categoria))
                throw new ArgumentException("La categoría indicada no es válida.", nameof(categoria));
            Categoria = categoria;
        }

        public void DescontarStock(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad a descontar debe ser mayor a 0.", nameof(cantidad));
            if (cantidad > Stock)
                throw new InvalidOperationException(
                    $"Stock insuficiente de '{Nombre}': disponible {Stock}, solicitado {cantidad}.");
            Stock -= cantidad;
        }

        public void ReponerStock(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad a reponer debe ser mayor a 0.", nameof(cantidad));
            Stock += cantidad;
        }
    }
}
