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
                .Include(u => u.Sucursal)
                .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<Usuario?> GetAsync(int id)
        {
            return await context.Usuarios
                .Include(u => u.Sucursal)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await context.Usuarios
                .Include(u => u.Sucursal)
                .OrderBy(u => u.NombreCompleto)
                .ToListAsync();
        }

        public async Task AddAsync(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Usuario usuario)
        {
            if (context.Entry(usuario).State == EntityState.Detached)
                return false;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            context.Usuarios.Remove(usuario);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UsernameExistsAsync(string username, int? excluirId = null)
        {
            var query = context.Usuarios.Where(u => u.Username.ToLower() == username.ToLower());
            if (excluirId.HasValue)
                query = query.Where(u => u.Id != excluirId.Value);

            return await query.AnyAsync();
        }

        public async Task<int> ContarAdministradoresActivosAsync()
        {
            return await context.Usuarios.CountAsync(u => u.Rol == RolUsuario.Administrador && u.Activo);
        }

        public async Task<bool> TieneMovimientosAsync(int id)
        {
            return await context.Ventas.AnyAsync(v => v.UsuarioId == id)
                || await context.IngresosProducto.AnyAsync(i => i.UsuarioId == id);
        }
    }
}
