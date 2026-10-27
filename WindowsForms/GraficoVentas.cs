using DTOs;
using System.Globalization;

namespace WindowsForms
{
    internal static class GraficoVentas
    {
        private static readonly string[] colores = { "#1F4E79", "#2E86C1", "#85C1E9", "#117A65", "#B9770E", "#7D3C98" };

        public static Image Generar(ReporteVentasDTO reporte, int ancho, int alto, double escala)
        {
            var plot = new ScottPlot.Plot { ScaleFactor = escala };
            Dibujar(plot, reporte);

            using var stream = new MemoryStream(plot.GetImageBytes(ancho, alto, ScottPlot.ImageFormat.Png));
            using var imagen = Image.FromStream(stream);
            return new Bitmap(imagen);
        }

        private static void Dibujar(ScottPlot.Plot plot, ReporteVentasDTO reporte)
        {
            var meses = reporte.PorMes
                .Select(m => new { m.Anio, m.Mes, m.Periodo })
                .Distinct()
                .OrderBy(m => m.Anio).ThenBy(m => m.Mes)
                .ToList();

            var sucursales = reporte.PorSucursal;
            var anchoGrupo = 0.8;
            var anchoBarra = anchoGrupo / Math.Max(1, sucursales.Count);
            var barras = new List<ScottPlot.Bar>();

            for (var m = 0; m < meses.Count; m++)
            {
                for (var s = 0; s < sucursales.Count; s++)
                {
                    var total = reporte.PorMes
                        .Where(x => x.Anio == meses[m].Anio && x.Mes == meses[m].Mes && x.SucursalId == sucursales[s].SucursalId)
                        .Sum(x => x.Total);

                    barras.Add(new ScottPlot.Bar
                    {
                        Position = m - anchoGrupo / 2 + anchoBarra * (s + 0.5),
                        Value = (double)total,
                        Size = anchoBarra * 0.92,
                        FillColor = Color(s),
                        LineWidth = 0
                    });
                }
            }

            if (barras.Count > 0)
                plot.Add.Bars(barras);

            plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(
                Enumerable.Range(0, meses.Count).Select(i => (double)i).ToArray(),
                meses.Select(m => m.Periodo).ToArray());

            plot.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.NumericAutomatic
            {
                LabelFormatter = valor => valor.ToString("C0", CultureInfo.CurrentCulture)
            };

            if (reporte.CantidadVentas == 0)
            {
                plot.Title("Ventas por mes y sucursal (sin ventas en el período)");
                plot.Axes.SetLimitsY(0, 1000);
            }
            else
            {
                plot.Title("Ventas por mes y sucursal");
                plot.Axes.Margins(bottom: 0, top: 0.1);
            }

            for (var s = 0; s < sucursales.Count; s++)
            {
                plot.Legend.ManualItems.Add(new ScottPlot.LegendItem
                {
                    LabelText = sucursales[s].SucursalNombre,
                    FillColor = Color(s)
                });
            }

            plot.Legend.Orientation = ScottPlot.Orientation.Horizontal;
            plot.ShowLegend(ScottPlot.Edge.Bottom);
        }

        private static ScottPlot.Color Color(int indice) =>
            ScottPlot.Color.FromHex(colores[indice % colores.Length]);
    }
}
