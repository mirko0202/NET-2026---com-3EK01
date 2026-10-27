using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SucursalRepository : ISucursalRepository
    {
        private readonly TPIContext context;

        public SucursalRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task<Sucursal?> GetAsync(int id)
        {
            return await context.Sucursales.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Sucursal>> GetAllAsync()
        {
            return await context.Sucursales
                .OrderBy(s => s.Nombre)
                .ToListAsync();
        }
    }
}
