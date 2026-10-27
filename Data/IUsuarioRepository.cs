using Domain.Model;

namespace Data
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByUsernameAsync(string username);
        Task<Usuario?> GetAsync(int id);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task AddAsync(Usuario usuario);
        Task<bool> UpdateAsync(Usuario usuario);
        Task<bool> DeleteAsync(int id);
        Task<bool> UsernameExistsAsync(string username, int? excluirId = null);
        Task<int> ContarAdministradoresActivosAsync();
        Task<bool> TieneMovimientosAsync(int id);
    }
}
