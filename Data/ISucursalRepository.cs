using Domain.Model;

namespace Data
{
    public interface ISucursalRepository
    {
        Task<Sucursal?> GetAsync(int id);
        Task<IEnumerable<Sucursal>> GetAllAsync();
    }
}
