using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class SucursalEndpoints
    {
        public static void MapSucursalEndpoints(this WebApplication app)
        {
            app.MapGet("/sucursales", async (ISucursalService service) =>
            {
                var dtos = await service.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllSucursales")
            .Produces<List<SucursalDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization()
            .WithOpenApi();

            app.MapGet("/sucursales/{id}", async (int id, ISucursalService service) =>
            {
                var dto = await service.GetAsync(id);
                return dto == null ? Results.NotFound() : Results.Ok(dto);
            })
            .WithName("GetSucursal")
            .Produces<SucursalDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization()
            .WithOpenApi();

            app.MapPost("/sucursales", async (SucursalDTO dto, ISucursalService service) =>
            {
                try
                {
                    var creada = await service.AddAsync(dto);
                    return Results.Created($"/sucursales/{creada.Id}", creada);
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
            .WithName("AddSucursal")
            .Produces<SucursalDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();

            app.MapPut("/sucursales", async (SucursalDTO dto, ISucursalService service) =>
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
            .WithName("UpdateSucursal")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();

            app.MapDelete("/sucursales/{id}", async (int id, ISucursalService service) =>
            {
                try
                {
                    var deleted = await service.DeleteAsync(id);
                    return deleted ? Results.NoContent() : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    return Errores.BadRequest(ex);
                }
            })
            .WithName("DeleteSucursal")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();
        }
    }
}
