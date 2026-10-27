using DTOs;

namespace Application.Services
{
    public interface ISucursalService
    {
        Task<SucursalDTO?> GetAsync(int id);
        Task<IEnumerable<SucursalDTO>> GetAllAsync();
    }
}
