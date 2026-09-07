using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class ProductoEndpoints
    {
        public static void MapProductoEndpoints(this WebApplication app)
        {
            app.MapGet("/productos", async (IProductoService service) =>
            {
                var dtos = await service.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllProductos")
            .Produces<List<ProductoDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/productos/{id}", async (int id, IProductoService service) =>
            {
                var dto = await service.GetAsync(id);
                return dto == null ? Results.NotFound() : Results.Ok(dto);
            })
            .WithName("GetProducto")
            .Produces<ProductoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/productos/criteria", async (string? texto, bool? bajoStock,
                                                     IProductoService service) =>
            {
                var criteria = new ProductoCriteriaDTO { Texto = texto, BajoStock = bajoStock };
                var dtos = await service.GetByCriteriaAsync(criteria);
                return Results.Ok(dtos);
            })
            .WithName("GetProductosByCriteria")
            .Produces<List<ProductoDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/productos", async (ProductoDTO dto, IProductoService service) =>
            {
                try
                {
                    var creado = await service.AddAsync(dto);
                    return Results.Created($"/productos/{creado.Id}", creado);
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
            .WithName("AddProducto")
            .Produces<ProductoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/productos", async (ProductoDTO dto, IProductoService service) =>
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
            .WithName("UpdateProducto")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapDelete("/productos/{id}", async (int id, IProductoService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteProducto")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
