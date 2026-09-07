using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/auth/login", async (LoginRequest request, IAuthService service) =>
            {
                var response = await service.LoginAsync(request);
                return response.Exito
                    ? Results.Ok(response)
                    : Results.Json(response, statusCode: StatusCodes.Status401Unauthorized);
            })
            .WithName("Login")
            .WithSummary("Autentica un usuario y devuelve sus datos y rol.")
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi();
        }
    }
}
