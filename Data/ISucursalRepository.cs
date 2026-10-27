using Domain.Model;

namespace Data
{
    public interface ISucursalRepository
    {
        Task<Sucursal?> GetAsync(int id);
        Task<IEnumerable<Sucursal>> GetAllAsync();
        Task AddAsync(Sucursal sucursal);
        Task<bool> UpdateAsync(Sucursal sucursal);
        Task<bool> DeleteAsync(int id);
        Task<bool> NombreExistsAsync(string nombre, int? excluirId = null);
        Task<bool> TieneMovimientosAsync(int id);
    }
}
