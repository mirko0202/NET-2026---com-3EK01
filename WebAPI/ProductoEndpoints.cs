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
            .RequireAuthorization()
            .WithOpenApi();

            app.MapGet("/productos/{id}", async (int id, IProductoService service) =>
            {
                var dto = await service.GetAsync(id);
                return dto == null ? Results.NotFound() : Results.Ok(dto);
            })
            .WithName("GetProducto")
            .Produces<ProductoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization()
            .WithOpenApi();

            app.MapGet("/productos/criteria", async (string? texto, bool? bajoStock, int? categoria,
                                                     IProductoService service) =>
            {
                try
                {
                    var criteria = new ProductoCriteriaDTO { Texto = texto, BajoStock = bajoStock, Categoria = categoria };
                    var dtos = await service.GetByCriteriaAsync(criteria);
                    return Results.Ok(dtos);
                }
                catch (ArgumentException ex)
                {
                    return Errores.BadRequest(ex);
                }
            })
            .WithName("GetProductosByCriteria")
            .Produces<List<ProductoDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization()
            .WithOpenApi();

            app.MapGet("/productos/categorias", (IProductoService service) =>
            {
                return Results.Ok(service.GetCategorias());
            })
            .WithName("GetCategoriasProducto")
            .Produces<List<CategoriaDTO>>(StatusCodes.Status200OK)
            .RequireAuthorization()
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
                    return Errores.BadRequest(ex);
                }
                catch (InvalidOperationException ex)
                {
                    return Errores.BadRequest(ex);
                }
            })
            .WithName("AddProducto")
            .Produces<ProductoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization(Politicas.SoloAdministrador)
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
                    return Errores.BadRequest(ex);
                }
                catch (InvalidOperationException ex)
                {
                    return Errores.BadRequest(ex);
                }
            })
            .WithName("UpdateProducto")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();

            app.MapDelete("/productos/{id}", async (int id, IProductoService service) =>
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
            .WithName("DeleteProducto")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(Politicas.SoloAdministrador)
            .WithOpenApi();
        }
    }
}
