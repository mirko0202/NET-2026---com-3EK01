using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class IngredienteService : IIngredienteService
    {
        private readonly IIngredienteRepository ingredienteRepository;

        public IngredienteService(IIngredienteRepository ingredienteRepository)
        {
            this.ingredienteRepository = ingredienteRepository;
        }

        public async Task<IngredienteDTO> AddAsync(IngredienteDTO dto)
        {
            var ingrediente = new Ingrediente(0, dto.Nombre, dto.Descripcion,
                                              dto.Unidad, dto.Stock, dto.StockMin);

            if (await ingredienteRepository.NombreExistsAsync(ingrediente.Nombre))
                throw new InvalidOperationException($"Ya existe un ingrediente con el nombre '{ingrediente.Nombre}'.");

            await ingredienteRepository.AddAsync(ingrediente);

            return ToDTO(ingrediente);
        }

        public async Task<bool> UpdateAsync(IngredienteDTO dto)
        {
            var ingrediente = new Ingrediente(dto.Id, dto.Nombre, dto.Descripcion,
                                              dto.Unidad, dto.Stock, dto.StockMin);

            if (await ingredienteRepository.NombreExistsAsync(ingrediente.Nombre, ingrediente.Id))
                throw new InvalidOperationException($"Ya existe otro ingrediente con el nombre '{ingrediente.Nombre}'.");

            return await ingredienteRepository.UpdateAsync(ingrediente);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await ingredienteRepository.DeleteAsync(id);
        }

        public async Task<IngredienteDTO?> GetAsync(int id)
        {
            var ingrediente = await ingredienteRepository.GetAsync(id);
            return ingrediente == null ? null : ToDTO(ingrediente);
        }

        public async Task<IEnumerable<IngredienteDTO>> GetAllAsync()
        {
            var ingredientes = await ingredienteRepository.GetAllAsync();
            return ingredientes.Select(ToDTO).ToList();
        }

        private static IngredienteDTO ToDTO(Ingrediente i) => new()
        {
            Id = i.Id,
            Nombre = i.Nombre,
            Descripcion = i.Descripcion,
            Unidad = i.Unidad,
            Stock = i.Stock,
            StockMin = i.StockMin,
            BajoStock = i.BajoStock
        };
    }
}
