using Domain.Model;

namespace Data
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByUsernameAsync(string username);
    }
}
