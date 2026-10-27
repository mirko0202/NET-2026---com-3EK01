using System.Security.Claims;

namespace WebAPI
{
    public static class ClaimsExtensions
    {
        public static int? UsuarioId(this ClaimsPrincipal usuario) =>
            int.TryParse(usuario.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;
    }
}
