using API.Auth.Blazor.WebAssembly;
using API.Clients;
using Blazor.WebAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;

var cultura = new CultureInfo("es-AR");
cultura.NumberFormat.CurrencySymbol = "$";
cultura.NumberFormat.CurrencyPositivePattern = 2;
cultura.NumberFormat.CurrencyNegativePattern = 9;
CultureInfo.DefaultThreadCurrentCulture = cultura;
CultureInfo.DefaultThreadCurrentUICulture = cultura;
CultureInfo.CurrentCulture = cultura;
CultureInfo.CurrentUICulture = cultura;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<IAuthService, BlazorWasmAuthService>();

var app = builder.Build();

AuthServiceProvider.Register(app.Services.GetRequiredService<IAuthService>());

await app.RunAsync();
