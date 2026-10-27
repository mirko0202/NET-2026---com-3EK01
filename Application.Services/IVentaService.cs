using DTOs;

namespace Application.Services
{
    public interface IVentaService
    {
        Task<VentaDTO?> GetAsync(int id);
        Task<IEnumerable<VentaDTO>> GetAllAsync();
        Task<VentaDTO> AddAsync(VentaDTO dto, int usuarioId);
        Task<bool> UpdateAsync(VentaDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
