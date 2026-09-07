using Domain.Model;

namespace Data
{
    public interface IIngredienteRepository
    {
        Task<Ingrediente?> GetAsync(int id);
        Task<IEnumerable<Ingrediente>> GetAllAsync();
        Task AddAsync(Ingrediente ingrediente);
        Task<bool> UpdateAsync(Ingrediente ingrediente);
        Task<bool> DeleteAsync(int id);
        Task<bool> NombreExistsAsync(string nombre, int? excluirId = null);
    }
}
