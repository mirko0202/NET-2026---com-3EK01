using DTOs;

namespace Application.Services
{
    public interface IIngresoProductoService
    {
        Task<IngresoProductoDTO?> GetAsync(int id);
        Task<IEnumerable<IngresoProductoDTO>> GetAllAsync(int? productoId, DateTime? desde, DateTime? hasta);
        Task<IngresoProductoDTO> AddAsync(IngresoProductoDTO dto, int usuarioId);
        Task<bool> UpdateAsync(IngresoProductoDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
