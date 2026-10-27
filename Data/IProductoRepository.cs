using Domain.Model;

namespace Data
{
    public interface IProductoRepository
    {
        Task<Producto?> GetAsync(int id);
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<IEnumerable<Producto>> GetByCriteriaAsync(ProductoCriteria criteria);
        Task AddAsync(Producto producto);
        Task<bool> UpdateAsync(Producto producto);
        Task<bool> DeleteAsync(int id);
        Task<bool> NombreExistsAsync(string nombre, int? excluirId = null);
        Task<bool> TieneMovimientosAsync(int id);
    }
}
