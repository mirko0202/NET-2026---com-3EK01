using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            this.productoRepository = productoRepository;
        }

        public async Task<ProductoDTO> AddAsync(ProductoDTO dto)
        {
            var producto = new Producto(0, dto.Nombre, dto.Descripcion, dto.Unidad, dto.Stock,
                                        dto.StockMin, dto.Precio, (CategoriaProducto)dto.Categoria);

            if (await productoRepository.NombreExistsAsync(producto.Nombre))
                throw new InvalidOperationException($"Ya existe un producto con el nombre '{producto.Nombre}'.");

            await productoRepository.AddAsync(producto);

            return ToDTO(producto);
        }

        public async Task<bool> UpdateAsync(ProductoDTO dto)
        {
            var producto = new Producto(dto.Id, dto.Nombre, dto.Descripcion, dto.Unidad, dto.Stock,
                                        dto.StockMin, dto.Precio, (CategoriaProducto)dto.Categoria);

            if (await productoRepository.NombreExistsAsync(producto.Nombre, producto.Id))
                throw new InvalidOperationException($"Ya existe otro producto con el nombre '{producto.Nombre}'.");

            return await productoRepository.UpdateAsync(producto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (await productoRepository.TieneMovimientosAsync(id))
                throw new InvalidOperationException(
                    "No se puede eliminar el producto porque tiene ventas o ingresos de mercadería registrados.");

            return await productoRepository.DeleteAsync(id);
        }

        public async Task<ProductoDTO?> GetAsync(int id)
        {
            var producto = await productoRepository.GetAsync(id);
            return producto == null ? null : ToDTO(producto);
        }

        public async Task<IEnumerable<ProductoDTO>> GetAllAsync()
        {
            var productos = await productoRepository.GetAllAsync();
            return productos.Select(ToDTO).ToList();
        }

        public async Task<IEnumerable<ProductoDTO>> GetByCriteriaAsync(ProductoCriteriaDTO criteriaDTO)
        {
            var categoria = criteriaDTO.Categoria.HasValue
                ? (CategoriaProducto?)criteriaDTO.Categoria.Value
                : null;

            var criteria = new ProductoCriteria(criteriaDTO.Texto, criteriaDTO.BajoStock, categoria);
            var productos = await productoRepository.GetByCriteriaAsync(criteria);
            return productos.Select(ToDTO).ToList();
        }

        public IEnumerable<CategoriaDTO> GetCategorias() =>
            Enum.GetValues<CategoriaProducto>()
                .Select(c => new CategoriaDTO { Id = (int)c, Nombre = c.Nombre() })
                .ToList();

        private static ProductoDTO ToDTO(Producto p) => new()
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            Unidad = p.Unidad,
            Stock = p.Stock,
            StockMin = p.StockMin,
            Precio = p.Precio,
            Categoria = (int)p.Categoria,
            CategoriaNombre = p.Categoria.Nombre(),
            BajoStock = p.BajoStock
        };
    }
}
