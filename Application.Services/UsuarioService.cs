using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ISucursalRepository sucursalRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, ISucursalRepository sucursalRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.sucursalRepository = sucursalRepository;
        }

        public async Task<UsuarioDTO?> GetAsync(int id)
        {
            var usuario = await usuarioRepository.GetAsync(id);
            return usuario == null ? null : Mapeos.ToDTO(usuario);
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await usuarioRepository.GetAllAsync();
            return usuarios.Select(Mapeos.ToDTO).ToList();
        }

        public async Task<UsuarioDTO> AddAsync(UsuarioDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("La contraseña es requerida para un usuario nuevo.");

            await ValidarSucursalAsync(dto.SucursalId);

            var usuario = new Usuario(0, dto.Username, dto.NombreCompleto, dto.Password,
                                      ObtenerRol(dto.Rol), dto.Activo, dto.SucursalId);

            if (await usuarioRepository.UsernameExistsAsync(usuario.Username))
                throw new InvalidOperationException($"Ya existe un usuario con el nombre '{usuario.Username}'.");

            await usuarioRepository.AddAsync(usuario);

            var creado = await usuarioRepository.GetAsync(usuario.Id);
            return Mapeos.ToDTO(creado!);
        }

        public async Task<bool> UpdateAsync(UsuarioDTO dto, int usuarioActualId)
        {
            var usuario = await usuarioRepository.GetAsync(dto.Id);
            if (usuario == null) return false;

            var esUnoMismo = usuario.Id == usuarioActualId;
            var eraAdministrador = usuario.EsAdministrador;
            var eraAdministradorActivo = usuario.EsAdministrador && usuario.Activo;

            usuario.SetUsername(dto.Username);
            usuario.SetNombreCompleto(dto.NombreCompleto);
            usuario.SetRol(ObtenerRol(dto.Rol));
            usuario.SetActivo(dto.Activo);
            usuario.SetSucursalId(dto.SucursalId);

            if (!string.IsNullOrWhiteSpace(dto.Password))
                usuario.SetPassword(dto.Password);

            if (esUnoMismo && !usuario.Activo)
                throw new InvalidOperationException("No puede desactivar su propio usuario.");

            if (esUnoMismo && eraAdministrador && !usuario.EsAdministrador)
                throw new InvalidOperationException("No puede quitarse a sí mismo el rol de administrador.");

            var dejaDeSerAdministradorActivo = eraAdministradorActivo && !(usuario.EsAdministrador && usuario.Activo);

            if (dejaDeSerAdministradorActivo && await usuarioRepository.ContarAdministradoresActivosAsync() <= 1)
                throw new InvalidOperationException("Debe quedar al menos un administrador activo.");

            if (await usuarioRepository.UsernameExistsAsync(usuario.Username, usuario.Id))
                throw new InvalidOperationException($"Ya existe otro usuario con el nombre '{usuario.Username}'.");

            await ValidarSucursalAsync(usuario.SucursalId);

            return await usuarioRepository.UpdateAsync(usuario);
        }

        public async Task<bool> DeleteAsync(int id, int usuarioActualId)
        {
            var usuario = await usuarioRepository.GetAsync(id);
            if (usuario == null) return false;

            if (usuario.Id == usuarioActualId)
                throw new InvalidOperationException("No puede eliminar su propio usuario.");

            if (usuario.EsAdministrador && usuario.Activo &&
                await usuarioRepository.ContarAdministradoresActivosAsync() <= 1)
                throw new InvalidOperationException("Debe quedar al menos un administrador activo.");

            if (await usuarioRepository.TieneMovimientosAsync(id))
                throw new InvalidOperationException(
                    "No se puede eliminar el usuario porque registró ventas o ingresos de mercadería. Puede desactivarlo.");

            return await usuarioRepository.DeleteAsync(id);
        }

        private async Task ValidarSucursalAsync(int? sucursalId)
        {
            if (sucursalId.HasValue && await sucursalRepository.GetAsync(sucursalId.Value) == null)
                throw new ArgumentException("La sucursal indicada no existe.");
        }

        private static RolUsuario ObtenerRol(string rol)
        {
            if (Enum.TryParse<RolUsuario>(rol, true, out var resultado) &&
                Enum.IsDefined(typeof(RolUsuario), resultado) &&
                !int.TryParse(rol, out _))
                return resultado;

            throw new ArgumentException("El rol debe ser Administrador o Empleado.");
        }
    }
}
