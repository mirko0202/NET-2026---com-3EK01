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
