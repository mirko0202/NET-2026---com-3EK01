using API.Clients;
using DTOs;
using Microsoft.JSInterop;
using System.Globalization;
using System.Text.Json;

namespace API.Auth.Blazor.WebAssembly
{
    public class BlazorWasmAuthService : IAuthService
    {
        private const string ClaveToken = "tpi_token";
        private const string ClaveExpiracion = "tpi_expiracion";
        private const string ClaveUsuario = "tpi_usuario";

        private readonly IJSRuntime jsRuntime;

        public event Action<bool>? AuthenticationStateChanged;

        public BlazorWasmAuthService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await LeerAsync(ClaveToken);
            var expiracion = await LeerAsync(ClaveExpiracion);

            return !string.IsNullOrEmpty(token)
                && DateTime.TryParse(expiracion, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var vence)
                && DateTime.UtcNow < vence;
        }

        public async Task<string?> GetTokenAsync() =>
            await IsAuthenticatedAsync() ? await LeerAsync(ClaveToken) : null;

        public async Task<UsuarioDTO?> GetUsuarioAsync()
        {
            if (!await IsAuthenticatedAsync())
                return null;

            var json = await LeerAsync(ClaveUsuario);
            return string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<UsuarioDTO>(json);
        }

        public async Task<bool> EsAdministradorAsync() =>
            (await GetUsuarioAsync())?.EsAdministrador == true;

        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            var response = await AuthApiClient.LoginAsync(username, password);

            if (response.Exito && !string.IsNullOrEmpty(response.Token) && response.Usuario != null)
            {
                var expiracion = (response.Expiracion ?? DateTime.UtcNow).ToUniversalTime();

                await EscribirAsync(ClaveToken, response.Token);
                await EscribirAsync(ClaveExpiracion, expiracion.ToString("O", CultureInfo.InvariantCulture));
                await EscribirAsync(ClaveUsuario, JsonSerializer.Serialize(response.Usuario));

                AuthenticationStateChanged?.Invoke(true);
            }

            return response;
        }

        public async Task LogoutAsync()
        {
            var habiaSesion = !string.IsNullOrEmpty(await LeerAsync(ClaveToken));

            await jsRuntime.InvokeVoidAsync("localStorage.removeItem", ClaveToken);
            await jsRuntime.InvokeVoidAsync("localStorage.removeItem", ClaveExpiracion);
            await jsRuntime.InvokeVoidAsync("localStorage.removeItem", ClaveUsuario);

            if (habiaSesion)
                AuthenticationStateChanged?.Invoke(false);
        }

        public async Task CheckTokenExpirationAsync()
        {
            if (!string.IsNullOrEmpty(await LeerAsync(ClaveToken)) && !await IsAuthenticatedAsync())
                await LogoutAsync();
        }

        private async Task<string?> LeerAsync(string clave) =>
            await jsRuntime.InvokeAsync<string?>("localStorage.getItem", clave);

        private async Task EscribirAsync(string clave, string valor) =>
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", clave, valor);
    }
}
