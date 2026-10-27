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

        public async Task AddAsync(Sucursal sucursal)
        {
            context.Sucursales.Add(sucursal);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Sucursal sucursal)
        {
            var existente = await context.Sucursales.FindAsync(sucursal.Id);
            if (existente == null) return false;

            existente.SetNombre(sucursal.Nombre);
            existente.SetDireccion(sucursal.Direccion);
            existente.SetTelefono(sucursal.Telefono);
            existente.SetActiva(sucursal.Activa);

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sucursal = await context.Sucursales.FindAsync(id);
            if (sucursal == null) return false;

            context.Sucursales.Remove(sucursal);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> NombreExistsAsync(string nombre, int? excluirId = null)
        {
            var query = context.Sucursales.Where(s => s.Nombre.ToLower() == nombre.ToLower());
            if (excluirId.HasValue)
                query = query.Where(s => s.Id != excluirId.Value);

            return await query.AnyAsync();
        }

        public async Task<bool> TieneMovimientosAsync(int id)
        {
            return await context.Ventas.AnyAsync(v => v.SucursalId == id)
                || await context.IngresosProducto.AnyAsync(i => i.SucursalId == id)
                || await context.Usuarios.AnyAsync(u => u.SucursalId == id);
        }
    }
}
