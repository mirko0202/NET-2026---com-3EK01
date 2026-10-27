using DTOs;

namespace API.Clients
{
    public interface IAuthService
    {
        event Action<bool>? AuthenticationStateChanged;

        Task<bool> IsAuthenticatedAsync();
        Task<string?> GetTokenAsync();
        Task<UsuarioDTO?> GetUsuarioAsync();
        Task<bool> EsAdministradorAsync();
        Task<LoginResponse> LoginAsync(string username, string password);
        Task LogoutAsync();
        Task CheckTokenExpirationAsync();
    }
}
