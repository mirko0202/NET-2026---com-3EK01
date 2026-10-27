using Data;
using Domain.Model;
using DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IConfiguration configuration;

        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            this.usuarioRepository = usuarioRepository;
            this.configuration = configuration;
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

            var expiracion = DateTime.UtcNow.AddMinutes(ObtenerMinutosDeExpiracion());

            return new LoginResponse
            {
                Exito = true,
                Mensaje = "Autenticación exitosa.",
                Token = GenerarToken(usuario, expiracion),
                Expiracion = expiracion,
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

        private string GenerarToken(Usuario usuario, DateTime expiracion)
        {
            var jwt = configuration.GetSection("JwtSettings");
            var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["SecretKey"]!));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.GivenName, usuario.NombreCompleto),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: expiracion,
                signingCredentials: new SigningCredentials(clave, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private int ObtenerMinutosDeExpiracion() =>
            int.TryParse(configuration["JwtSettings:ExpirationMinutes"], out var minutos) ? minutos : 60;

        private static LoginResponse Fallo(string mensaje) => new()
        {
            Exito = false,
            Mensaje = mensaje
        };
    }
}
