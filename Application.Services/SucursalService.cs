using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class SucursalService : ISucursalService
    {
        private readonly ISucursalRepository sucursalRepository;

        public SucursalService(ISucursalRepository sucursalRepository)
        {
            this.sucursalRepository = sucursalRepository;
        }

        public async Task<SucursalDTO?> GetAsync(int id)
        {
            var sucursal = await sucursalRepository.GetAsync(id);
            return sucursal == null ? null : ToDTO(sucursal);
        }

        public async Task<IEnumerable<SucursalDTO>> GetAllAsync()
        {
            var sucursales = await sucursalRepository.GetAllAsync();
            return sucursales.Select(ToDTO).ToList();
        }

        public async Task<SucursalDTO> AddAsync(SucursalDTO dto)
        {
            var sucursal = new Sucursal(0, dto.Nombre, dto.Direccion, dto.Telefono, dto.Activa);

            if (await sucursalRepository.NombreExistsAsync(sucursal.Nombre))
                throw new InvalidOperationException($"Ya existe una sucursal con el nombre '{sucursal.Nombre}'.");

            await sucursalRepository.AddAsync(sucursal);

            return ToDTO(sucursal);
        }

        public async Task<bool> UpdateAsync(SucursalDTO dto)
        {
            var sucursal = new Sucursal(dto.Id, dto.Nombre, dto.Direccion, dto.Telefono, dto.Activa);

            if (await sucursalRepository.NombreExistsAsync(sucursal.Nombre, sucursal.Id))
                throw new InvalidOperationException($"Ya existe otra sucursal con el nombre '{sucursal.Nombre}'.");

            return await sucursalRepository.UpdateAsync(sucursal);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (await sucursalRepository.TieneMovimientosAsync(id))
                throw new InvalidOperationException(
                    "No se puede eliminar la sucursal porque tiene ventas, ingresos o usuarios asociados. Puede desactivarla.");

            return await sucursalRepository.DeleteAsync(id);
        }

        private static SucursalDTO ToDTO(Sucursal s) => new()
        {
            Id = s.Id,
            Nombre = s.Nombre,
            Direccion = s.Direccion,
            Telefono = s.Telefono,
            Activa = s.Activa
        };
    }
}
