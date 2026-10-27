using Domain.Model;
using DTOs;

namespace Application.Services
{
    internal static class Mapeos
    {
        public static UsuarioDTO ToDTO(Usuario u) => new()
        {
            Id = u.Id,
            Username = u.Username,
            NombreCompleto = u.NombreCompleto,
            Rol = u.Rol.ToString(),
            EsAdministrador = u.EsAdministrador,
            Activo = u.Activo,
            SucursalId = u.SucursalId,
            SucursalNombre = u.Sucursal?.Nombre
        };
    }
}
