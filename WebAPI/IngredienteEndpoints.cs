using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class IngredienteEndpoints
    {
        public static void MapIngredienteEndpoints(this WebApplication app)
        {
            app.MapGet("/ingredientes", async (IIngredienteService service) =>
            {
                var dtos = await service.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllIngredientes")
            .Produces<List<IngredienteDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/ingredientes/{id}", async (int id, IIngredienteService service) =>
            {
                var dto = await service.GetAsync(id);
                return dto == null ? Results.NotFound() : Results.Ok(dto);
            })
            .WithName("GetIngrediente")
            .Produces<IngredienteDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/ingredientes", async (IngredienteDTO dto, IIngredienteService service) =>
            {
                try
                {
                    var creado = await service.AddAsync(dto);
                    return Results.Created($"/ingredientes/{creado.Id}", creado);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddIngrediente")
            .Produces<IngredienteDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/ingredientes", async (IngredienteDTO dto, IIngredienteService service) =>
            {
                try
                {
                    var found = await service.UpdateAsync(dto);
                    return found ? Results.NoContent() : Results.NotFound();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateIngrediente")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapDelete("/ingredientes/{id}", async (int id, IIngredienteService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteIngrediente")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
