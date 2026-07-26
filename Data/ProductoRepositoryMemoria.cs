using Domain.Model;

namespace Data
{
    public class ProductoRepositoryMemoria : IProductoRepository
    {
        private readonly List<Producto> _productos = new();
        private readonly object _candado = new();
        private int _siguienteId = 1;

        public ProductoRepositoryMemoria()
        {
            SembrarDatos();
        }

        private void SembrarDatos()
        {
            Agregar(new Producto(0, "Medialuna", "Medialuna de manteca", "unidad", 120, 50));
            Agregar(new Producto(0, "Pan francés", "Pan tipo baguette", "kg", 30, 10));
            Agregar(new Producto(0, "Torta selva negra", "Porción individual", "porción", 8, 10));
            Agregar(new Producto(0, "Café", "Café en grano premium", "kg", 5, 8));
        }

        private void Agregar(Producto producto)
        {
            producto.SetId(_siguienteId++);
            _productos.Add(producto);
        }

        public Task<Producto?> GetAsync(int id)
        {
            lock (_candado)
                return Task.FromResult(_productos.FirstOrDefault(p => p.Id == id));
        }

        public Task<IEnumerable<Producto>> GetAllAsync()
        {
            lock (_candado)
                return Task.FromResult(_productos.ToList().AsEnumerable());
        }

        public Task<IEnumerable<Producto>> GetByCriteriaAsync(ProductoCriteria criteria)
        {
            lock (_candado)
            {
                IEnumerable<Producto> consulta = _productos.ToList();

                if (!string.IsNullOrWhiteSpace(criteria.Texto))
                    consulta = consulta.Where(p =>
                        p.Nombre.Contains(criteria.Texto, StringComparison.OrdinalIgnoreCase) ||
                        (p.Descripcion?.Contains(criteria.Texto, StringComparison.OrdinalIgnoreCase) ?? false));

                if (criteria.BajoStock == true)
                    consulta = consulta.Where(p => p.BajoStock);

                return Task.FromResult(consulta.ToList().AsEnumerable());
            }
        }

        public Task AddAsync(Producto producto)
        {
            lock (_candado)
                Agregar(producto);
            return Task.CompletedTask;
        }

        public Task<bool> UpdateAsync(Producto producto)
        {
            lock (_candado)
            {
                var indice = _productos.FindIndex(p => p.Id == producto.Id);
                if (indice < 0) return Task.FromResult(false);
                _productos[indice] = producto;
                return Task.FromResult(true);
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            lock (_candado)
            {
                var existente = _productos.FirstOrDefault(p => p.Id == id);
                if (existente == null) return Task.FromResult(false);
                _productos.Remove(existente);
                return Task.FromResult(true);
            }
        }
    }
}
