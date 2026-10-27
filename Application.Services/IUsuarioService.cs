using DTOs;

namespace Application.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO?> GetAsync(int id);
        Task<IEnumerable<UsuarioDTO>> GetAllAsync();
        Task<UsuarioDTO> AddAsync(UsuarioDTO dto);
        Task<bool> UpdateAsync(UsuarioDTO dto, int usuarioActualId);
        Task<bool> DeleteAsync(int id, int usuarioActualId);
    }
}
