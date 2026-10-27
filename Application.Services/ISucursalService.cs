using DTOs;

namespace Application.Services
{
    public interface ISucursalService
    {
        Task<SucursalDTO?> GetAsync(int id);
        Task<IEnumerable<SucursalDTO>> GetAllAsync();
        Task<SucursalDTO> AddAsync(SucursalDTO dto);
        Task<bool> UpdateAsync(SucursalDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
