using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class VentaRepository : IVentaRepository
    {
        private readonly TPIContext context;

        public VentaRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task<Venta?> GetAsync(int id)
        {
            return await context.Ventas
                .Include(v => v.Usuario)
                .Include(v => v.Sucursal)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Venta>> GetAllAsync()
        {
            return await context.Ventas
                .Include(v => v.Usuario)
                .Include(v => v.Sucursal)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Producto)
                .OrderByDescending(v => v.FechaHora)
                .ToListAsync();
        }

        public async Task AddAsync(Venta venta)
        {
            context.Ventas.Add(venta);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Venta venta)
        {
            if (context.Entry(venta).State == EntityState.Detached)
                return false;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var venta = await context.Ventas.FindAsync(id);
            if (venta == null) return false;

            context.Ventas.Remove(venta);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
