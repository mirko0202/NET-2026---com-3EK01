using DTOs;

namespace Application.Services
{
    public interface IIngredienteService
    {
        Task<IngredienteDTO> AddAsync(IngredienteDTO dto);
        Task<bool> UpdateAsync(IngredienteDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<IngredienteDTO?> GetAsync(int id);
        Task<IEnumerable<IngredienteDTO>> GetAllAsync();
    }
}
