using Domain.Model;

namespace Data
{
    public interface IVentaRepository
    {
        Task<Venta?> GetAsync(int id);
        Task<IEnumerable<Venta>> GetAllAsync();
        Task AddAsync(Venta venta);
        Task<bool> UpdateAsync(Venta venta);
        Task<bool> DeleteAsync(int id);
    }
}
