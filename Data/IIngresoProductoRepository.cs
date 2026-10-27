using Domain.Model;

namespace Data
{
    public interface IIngresoProductoRepository
    {
        Task<IngresoProducto?> GetAsync(int id);
        Task<IEnumerable<IngresoProducto>> GetAllAsync(int? productoId = null, DateTime? desde = null, DateTime? hasta = null);
        Task AddAsync(IngresoProducto ingreso);
        Task<bool> UpdateAsync(IngresoProducto ingreso);
        Task<bool> DeleteAsync(int id);
    }
}
