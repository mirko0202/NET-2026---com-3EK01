using Data;
using DTOs;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository usuarioRepository;

        public AuthService(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return Fallo("Debe indicar usuario y contraseña.");
            }

            var usuario = await usuarioRepository.GetByUsernameAsync(request.Username.Trim());

            if (usuario == null || !usuario.ValidarPassword(request.Password))
                return Fallo("Usuario o contraseña incorrectos.");

            if (!usuario.Activo)
                return Fallo("El usuario se encuentra inactivo.");

            return new LoginResponse
            {
                Exito = true,
                Mensaje = "Autenticación exitosa.",
                Usuario = new UsuarioDTO
                {
                    Id = usuario.Id,
                    Username = usuario.Username,
                    NombreCompleto = usuario.NombreCompleto,
                    Rol = usuario.Rol.ToString(),
                    EsAdministrador = usuario.EsAdministrador
                }
            };
        }

        private static LoginResponse Fallo(string mensaje) => new()
        {
            Exito = false,
            Mensaje = mensaje
        };
    }
}
