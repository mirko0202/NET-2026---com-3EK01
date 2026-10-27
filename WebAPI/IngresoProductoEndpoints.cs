using Application.Services;
using DTOs;
using System.Security.Claims;

namespace WebAPI
{
    public static class IngresoProductoEndpoints
    {
        public static void MapIngresoProductoEndpoints(this WebApplication app)
        {
            app.MapGet("/ingresos", async (int? productoId, DateTime? desde, DateTime? hasta,
                                           IIngresoProductoService service) =>
            {
                try
                {
                    var dtos = await service.GetAllAsync(productoId, desde, hasta);
                    return Results.Ok(dtos);
                }
                catch (ArgumentException ex)
                {
                    return Errores.BadRequest(ex);
                }
            })
            .WithName("GetAllIngresos")
            .WithSummary("Historial de ingresos de mercadería, filtrable por producto y período.")
            .Produces<List<IngresoProductoDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization()
            .WithOpenApi();

            app.MapGet("/ingresos/{id}", async (int id, IIngresoProductoService service) =>
            {
                var dto = await service.GetAsync(id);
                return dto == null ? Results.NotFound() : Results.Ok(dto);
            })
            .WithName("GetIngreso")
            .Produces<IngresoProductoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization()
            .WithOpenApi();

            app.MapPost("/ingresos", async (IngresoProductoDTO dto, ClaimsPrincipal usuario,
                                            IIngresoProductoService service) =>
            {
                if (usuario.UsuarioId() is not int usuarioId)
                    return Results.Unauthorized();

                try
                {
                    var creado = await service.AddAsync(dto, usuarioId);
                    return Results.Created($"/ingresos/{creado.Id}", creado);
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
            .WithName("AddIngreso")
            .Produces<IngresoProductoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();

            app.MapPut("/ingresos", async (IngresoProductoDTO dto, IIngresoProductoService service) =>
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
            .WithName("UpdateIngreso")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();

            app.MapDelete("/ingresos/{id}", async (int id, IIngresoProductoService service) =>
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
            .WithName("DeleteIngreso")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();
        }
    }
}
