using Application.Services;
using Data;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Stock API - Gestión de Stock",
        Version = "v1",
        Description = "TPI .NET - Entrega 1 (Servicios + persistencia en memoria)."
    });
});

builder.Services.AddSingleton<IProductoRepository, ProductoRepositoryMemoria>();
builder.Services.AddSingleton<IIngredienteRepository, IngredienteRepositoryMemoria>();

builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IIngredienteService, IngredienteService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapProductoEndpoints();
app.MapIngredienteEndpoints();

app.Run();
