using Application.Services;
using DTOs;
using System.Security.Claims;

namespace WebAPI
{
    public static class UsuarioEndpoints
    {
        public static void MapUsuarioEndpoints(this WebApplication app)
        {
            app.MapGet("/usuarios", async (IUsuarioService service) =>
            {
                var dtos = await service.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllUsuarios")
            .Produces<List<UsuarioDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();

            app.MapGet("/usuarios/{id}", async (int id, IUsuarioService service) =>
            {
                var dto = await service.GetAsync(id);
                return dto == null ? Results.NotFound() : Results.Ok(dto);
            })
            .WithName("GetUsuario")
            .Produces<UsuarioDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();

            app.MapPost("/usuarios", async (UsuarioDTO dto, IUsuarioService service) =>
            {
                try
                {
                    var creado = await service.AddAsync(dto);
                    return Results.Created($"/usuarios/{creado.Id}", creado);
                }
                catch (ArgumentException ex)
                {
                    return Errores.BadRequest(ex);
                }
                catch (InvalidOperationException ex)
                {
                    return Errores.BadRequest(ex);
                }
            })
            .WithName("AddUsuario")
            .Produces<UsuarioDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();

            app.MapPut("/usuarios", async (UsuarioDTO dto, ClaimsPrincipal usuario, IUsuarioService service) =>
            {
                if (usuario.UsuarioId() is not int usuarioActualId)
                    return Results.Unauthorized();

                try
                {
                    var found = await service.UpdateAsync(dto, usuarioActualId);
                    return found ? Results.NoContent() : Results.NotFound();
                }
                catch (ArgumentException ex)
                {
                    return Errores.BadRequest(ex);
                }
                catch (InvalidOperationException ex)
                {
                    return Errores.BadRequest(ex);
                }
            })
            .WithName("UpdateUsuario")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();

            app.MapDelete("/usuarios/{id}", async (int id, ClaimsPrincipal usuario, IUsuarioService service) =>
            {
                if (usuario.UsuarioId() is not int usuarioActualId)
                    return Results.Unauthorized();

                try
                {
                    var deleted = await service.DeleteAsync(id, usuarioActualId);
                    return deleted ? Results.NoContent() : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    return Errores.BadRequest(ex);
                }
            })
            .WithName("DeleteUsuario")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();
        }
    }
}
