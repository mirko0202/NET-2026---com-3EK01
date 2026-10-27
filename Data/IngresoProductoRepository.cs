using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class IngresoProductoRepository : IIngresoProductoRepository
    {
        private readonly TPIContext context;

        public IngresoProductoRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task<IngresoProducto?> GetAsync(int id)
        {
            return await context.IngresosProducto
                .Include(i => i.Producto)
                .Include(i => i.Sucursal)
                .Include(i => i.Usuario)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<IngresoProducto>> GetAllAsync(int? productoId = null, DateTime? desde = null, DateTime? hasta = null)
        {
            var query = context.IngresosProducto
                .Include(i => i.Producto)
                .Include(i => i.Sucursal)
                .Include(i => i.Usuario)
                .AsQueryable();

            if (productoId.HasValue)
                query = query.Where(i => i.ProductoId == productoId.Value);

            if (desde.HasValue)
                query = query.Where(i => i.FechaHora >= desde.Value.Date);

            if (hasta.HasValue)
                query = query.Where(i => i.FechaHora < hasta.Value.Date.AddDays(1));

            return await query
                .OrderByDescending(i => i.FechaHora)
                .ToListAsync();
        }

        public async Task AddAsync(IngresoProducto ingreso)
        {
            context.IngresosProducto.Add(ingreso);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(IngresoProducto ingreso)
        {
            if (context.Entry(ingreso).State == EntityState.Detached)
                return false;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ingreso = await context.IngresosProducto.FindAsync(id);
            if (ingreso == null) return false;

            context.IngresosProducto.Remove(ingreso);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
