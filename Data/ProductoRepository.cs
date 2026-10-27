using Domain.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly TPIContext context;

        public ProductoRepository(TPIContext context)
        {
            this.context = context;
        }

        public async Task<Producto?> GetAsync(int id)
        {
            return await context.Productos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            return await context.Productos
                .OrderBy(p => p.Nombre)
                .ToListAsync();
        }

        public async Task<IEnumerable<Producto>> GetByCriteriaAsync(ProductoCriteria criteria)
        {
            const string sql = @"
                SELECT Id, Nombre, Descripcion, Unidad, Stock, StockMin
                FROM Productos
                WHERE (@Texto IS NULL
                       OR Nombre LIKE @Texto
                       OR ISNULL(Descripcion, '') LIKE @Texto)
                  AND (@BajoStock = 0 OR Stock <= StockMin)
                ORDER BY Nombre";

            var productos = new List<Producto>();

            using var connection = new SqlConnection(context.Database.GetConnectionString());
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Texto",
                string.IsNullOrWhiteSpace(criteria.Texto)
                    ? DBNull.Value
                    : $"%{criteria.Texto}%");

            command.Parameters.AddWithValue("@BajoStock", criteria.BajoStock == true);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                productos.Add(new Producto(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.IsDBNull(2) ? null : reader.GetString(2),
                    reader.IsDBNull(3) ? null : reader.GetString(3),
                    reader.GetInt32(4),
                    reader.GetInt32(5)));
            }

            return productos;
        }

        public async Task AddAsync(Producto producto)
        {
            context.Productos.Add(producto);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Producto producto)
        {
            var existente = await context.Productos.FindAsync(producto.Id);
            if (existente == null) return false;

            existente.SetNombre(producto.Nombre);
            existente.SetDescripcion(producto.Descripcion);
            existente.SetUnidad(producto.Unidad);
            existente.SetStock(producto.Stock);
            existente.SetStockMin(producto.StockMin);

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var producto = await context.Productos.FindAsync(id);
            if (producto == null) return false;

            context.Productos.Remove(producto);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> NombreExistsAsync(string nombre, int? excluirId = null)
        {
            var query = context.Productos.Where(p => p.Nombre.ToLower() == nombre.ToLower());
            if (excluirId.HasValue)
                query = query.Where(p => p.Id != excluirId.Value);

            return await query.AnyAsync();
        }

        public async Task<bool> TieneVentasAsync(int id)
        {
            return await context.DetallesVenta.AnyAsync(d => d.ProductoId == id);
        }
    }
}
