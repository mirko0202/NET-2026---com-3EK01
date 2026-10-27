using API.Clients;
using DTOs;

namespace API.Auth.WindowsForms
{
    public class WindowsFormsAuthService : IAuthService
    {
        private string? token;
        private DateTime expiracion;
        private UsuarioDTO? usuario;

        public event Action<bool>? AuthenticationStateChanged;

        public Task<bool> IsAuthenticatedAsync() =>
            Task.FromResult(!string.IsNullOrEmpty(token) && DateTime.UtcNow < expiracion);

        public async Task<string?> GetTokenAsync() =>
            await IsAuthenticatedAsync() ? token : null;

        public async Task<UsuarioDTO?> GetUsuarioAsync() =>
            await IsAuthenticatedAsync() ? usuario : null;

        public async Task<bool> EsAdministradorAsync() =>
            (await GetUsuarioAsync())?.EsAdministrador == true;

        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            var response = await AuthApiClient.LoginAsync(username, password);

            if (response.Exito && !string.IsNullOrEmpty(response.Token) && response.Usuario != null)
            {
                token = response.Token;
                expiracion = response.Expiracion ?? DateTime.UtcNow;
                usuario = response.Usuario;

                AuthenticationStateChanged?.Invoke(true);
            }

            return response;
        }

        public Task LogoutAsync()
        {
            var estabaAutenticado = token != null;

            token = null;
            expiracion = default;
            usuario = null;

            if (estabaAutenticado)
                AuthenticationStateChanged?.Invoke(false);

            return Task.CompletedTask;
        }

        public async Task CheckTokenExpirationAsync()
        {
            if (token != null && !await IsAuthenticatedAsync())
                await LogoutAsync();
        }
    }
}
