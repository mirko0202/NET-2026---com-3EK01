using System.Diagnostics;

namespace WindowsForms
{
    internal static class ArchivoPdf
    {
        public static async Task<bool> ExportarAsync(IWin32Window owner, string nombreSugerido, Func<Task<byte[]>> generar)
        {
            using var dialogo = new SaveFileDialog
            {
                Title = "Exportar reporte a PDF",
                Filter = "Documento PDF (*.pdf)|*.pdf",
                FileName = nombreSugerido,
                OverwritePrompt = true
            };

            if (dialogo.ShowDialog(owner) != DialogResult.OK)
                return false;

            var contenido = await generar();
            await File.WriteAllBytesAsync(dialogo.FileName, contenido);

            var abrir = MessageBox.Show(owner,
                $"El reporte se guardó en:\n{dialogo.FileName}\n\n¿Desea abrirlo ahora?",
                "Exportar PDF", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (abrir == DialogResult.Yes)
                Process.Start(new ProcessStartInfo(dialogo.FileName) { UseShellExecute = true });

            return true;
        }
    }
}
