using Application.Services;
using DTOs;
using System.Security.Claims;

namespace WebAPI
{
    public static class VentaEndpoints
    {
        public static void MapVentaEndpoints(this WebApplication app)
        {
            app.MapGet("/ventas", async (IVentaService service) =>
            {
                var dtos = await service.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllVentas")
            .Produces<List<VentaDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization()
            .WithOpenApi();

            app.MapGet("/ventas/{id}", async (int id, IVentaService service) =>
            {
                var dto = await service.GetAsync(id);
                return dto == null ? Results.NotFound() : Results.Ok(dto);
            })
            .WithName("GetVenta")
            .Produces<VentaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization()
            .WithOpenApi();

            app.MapPost("/ventas", async (VentaDTO dto, ClaimsPrincipal usuario, IVentaService service) =>
            {
                if (usuario.UsuarioId() is not int usuarioId)
                    return Results.Unauthorized();

                try
                {
                    var creada = await service.AddAsync(dto, usuarioId);
                    return Results.Created($"/ventas/{creada.Id}", creada);
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
            .WithName("AddVenta")
            .Produces<VentaDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization()
            .WithOpenApi();

            app.MapPut("/ventas", async (VentaDTO dto, IVentaService service) =>
            {
                try
                {
                    var found = await service.UpdateAsync(dto);
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
            .WithName("UpdateVenta")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();

            app.MapDelete("/ventas/{id}", async (int id, IVentaService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteVenta")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();
        }
    }
}
