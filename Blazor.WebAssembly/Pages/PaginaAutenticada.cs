using API.Clients;
using DTOs;
using Microsoft.AspNetCore.Components;

namespace Blazor.WebAssembly.Pages
{
    public abstract class PaginaAutenticada : ComponentBase
    {
        [Inject] protected IAuthService AuthService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        protected UsuarioDTO? Usuario { get; private set; }

        protected bool EsAdministrador => Usuario?.EsAdministrador == true;

        protected override async Task OnInitializedAsync()
        {
            Usuario = await AuthService.GetUsuarioAsync();

            if (Usuario == null)
            {
                Navigation.NavigateTo("login");
                return;
            }

            await OnAutenticadoAsync();
        }

        protected virtual Task OnAutenticadoAsync() => Task.CompletedTask;

        protected string? MensajeDeError(Exception ex)
        {
            if (ex is UnauthorizedAccessException)
            {
                Navigation.NavigateTo("login?expirada=true");
                return null;
            }

            return ex.Message;
        }
    }
}
