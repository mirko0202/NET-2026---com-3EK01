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
        }
    }
}
