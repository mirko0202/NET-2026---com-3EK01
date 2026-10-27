using Microsoft.JSInterop;

namespace Blazor.WebAssembly
{
    public static class Descargas
    {
        public static async Task DescargarPdfAsync(IJSRuntime js, string nombre, byte[] contenido)
        {
            using var stream = new MemoryStream(contenido);
            using var referencia = new DotNetStreamReference(stream);
            await js.InvokeVoidAsync("tpi.descargarArchivo", nombre, referencia, "application/pdf");
        }
    }
}
