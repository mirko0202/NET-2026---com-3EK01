using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly TPIContext context;

        public UsuarioRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task<Usuario?> GetByUsernameAsync(string username)
        {
            return await context.Usuarios
                .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }
    }
}
