using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class IngredienteRepository : IIngredienteRepository
    {
        private readonly TPIContext context;

        public IngredienteRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task<Ingrediente?> GetAsync(int id)
        {
            return await context.Ingredientes.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Ingrediente>> GetAllAsync()
        {
            return await context.Ingredientes
                .OrderBy(i => i.Nombre)
                .ToListAsync();
        }

        public async Task AddAsync(Ingrediente ingrediente)
        {
            context.Ingredientes.Add(ingrediente);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Ingrediente ingrediente)
        {
            var existente = await context.Ingredientes.FindAsync(ingrediente.Id);
            if (existente == null) return false;

            existente.SetNombre(ingrediente.Nombre);
            existente.SetDescripcion(ingrediente.Descripcion);
            existente.SetUnidad(ingrediente.Unidad);
            existente.SetStock(ingrediente.Stock);
            existente.SetStockMin(ingrediente.StockMin);

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ingrediente = await context.Ingredientes.FindAsync(id);
            if (ingrediente == null) return false;

            context.Ingredientes.Remove(ingrediente);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> NombreExistsAsync(string nombre, int? excluirId = null)
        {
            var query = context.Ingredientes.Where(i => i.Nombre.ToLower() == nombre.ToLower());
            if (excluirId.HasValue)
                query = query.Where(i => i.Id != excluirId.Value);

            return await query.AnyAsync();
        }
    }
}
