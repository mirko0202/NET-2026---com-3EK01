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
            var producto = new Producto(0, dto.Nombre, dto.Descripcion,
                                        dto.Unidad, dto.Stock, dto.StockMin);

            await productoRepository.AddAsync(producto);

            return ToDTO(producto);
        }

        public async Task<bool> UpdateAsync(ProductoDTO dto)
        {
            var producto = new Producto(dto.Id, dto.Nombre, dto.Descripcion,
                                        dto.Unidad, dto.Stock, dto.StockMin);

            return await productoRepository.UpdateAsync(producto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
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
            var criteria = new ProductoCriteria(criteriaDTO.Texto, criteriaDTO.BajoStock);
            var productos = await productoRepository.GetByCriteriaAsync(criteria);
            return productos.Select(ToDTO).ToList();
        }

        private static ProductoDTO ToDTO(Producto p) => new()
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            Unidad = p.Unidad,
            Stock = p.Stock,
            StockMin = p.StockMin,
            BajoStock = p.BajoStock
        };
    }
}
