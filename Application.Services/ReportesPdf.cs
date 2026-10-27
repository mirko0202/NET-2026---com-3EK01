using DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace Application.Services
{
    internal static class ReportesPdf
    {
        private static readonly CultureInfo cultura = new("es-AR");

        private static readonly string[] coloresSucursales = { "#1F4E79", "#2E86C1", "#85C1E9", "#117A65", "#B9770E", "#7D3C98" };

        static ReportesPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public static byte[] Ventas(ReporteVentasDTO reporte)
        {
            var grafico = GraficoVentasPorMes(reporte);

            return Document.Create(documento =>
            {
                documento.Page(pagina =>
                {
                    ConfigurarPagina(pagina);

                    pagina.Header().Element(c => Encabezado(c,
                        "Reporte de ventas por período y sucursal",
                        $"Período: {reporte.Desde:dd/MM/yyyy} al {reporte.Hasta:dd/MM/yyyy}   ·   {reporte.SucursalNombre}"));

                    pagina.Content().PaddingVertical(12).Column(columna =>
                    {
                        columna.Spacing(14);

                        columna.Item().Row(fila =>
                        {
                            fila.Spacing(8);
                            fila.RelativeItem().Element(c => Indicador(c, "Ventas", reporte.CantidadVentas.ToString("N0", cultura)));
                            fila.RelativeItem().Element(c => Indicador(c, "Unidades vendidas", reporte.Unidades.ToString("N0", cultura)));
                            fila.RelativeItem().Element(c => Indicador(c, "Total facturado", reporte.Total.ToString("C0", cultura)));
                            fila.RelativeItem().Element(c => Indicador(c, "Ticket promedio", reporte.TicketPromedio.ToString("C0", cultura)));
                        });

                        if (reporte.CantidadVentas == 0)
                        {
                            columna.Item().PaddingTop(20).AlignCenter()
                                .Text("No se registraron ventas en el período seleccionado.").Italic();
                            return;
                        }

                        columna.Item().Image(grafico);

                        columna.Item().Text("Ventas por sucursal").FontSize(12).Bold();
                        columna.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(3);
                                c.RelativeColumn();
                                c.RelativeColumn();
                                c.RelativeColumn(2);
                                c.RelativeColumn();
                            });

                            tabla.Header(h =>
                            {
                                h.Cell().Element(CeldaEncabezado).Text("Sucursal");
                                h.Cell().Element(CeldaEncabezado).AlignRight().Text("Ventas");
                                h.Cell().Element(CeldaEncabezado).AlignRight().Text("Unidades");
                                h.Cell().Element(CeldaEncabezado).AlignRight().Text("Total");
                                h.Cell().Element(CeldaEncabezado).AlignRight().Text("% del total");
                            });

                            foreach (var sucursal in reporte.PorSucursal)
                            {
                                var porcentaje = reporte.Total == 0 ? 0 : sucursal.Total / reporte.Total;

                                tabla.Cell().Element(Celda).Text(sucursal.SucursalNombre);
                                tabla.Cell().Element(Celda).AlignRight().Text(sucursal.CantidadVentas.ToString("N0", cultura));
                                tabla.Cell().Element(Celda).AlignRight().Text(sucursal.Unidades.ToString("N0", cultura));
                                tabla.Cell().Element(Celda).AlignRight().Text(sucursal.Total.ToString("C2", cultura));
                                tabla.Cell().Element(Celda).AlignRight().Text(porcentaje.ToString("P1", cultura));
                            }
                        });

                        columna.Item().Text("Productos más vendidos").FontSize(12).Bold();
                        columna.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(4);
                                c.RelativeColumn();
                                c.RelativeColumn(2);
                            });

                            tabla.Header(h =>
                            {
                                h.Cell().Element(CeldaEncabezado).Text("Producto");
                                h.Cell().Element(CeldaEncabezado).AlignRight().Text("Unidades");
                                h.Cell().Element(CeldaEncabezado).AlignRight().Text("Total");
                            });

                            foreach (var producto in reporte.ProductosMasVendidos)
                            {
                                tabla.Cell().Element(Celda).Text(producto.ProductoNombre);
                                tabla.Cell().Element(Celda).AlignRight().Text(producto.Unidades.ToString("N0", cultura));
                                tabla.Cell().Element(Celda).AlignRight().Text(producto.Total.ToString("C2", cultura));
                            }
                        });
                    });

                    pagina.Footer().Element(PiePagina);
                });
            }).GeneratePdf();
        }

        public static byte[] BajoStock(ReporteBajoStockDTO reporte)
        {
            return Document.Create(documento =>
            {
                documento.Page(pagina =>
                {
                    ConfigurarPagina(pagina);

                    pagina.Header().Element(c => Encabezado(c,
                        "Reporte de stock bajo el mínimo",
                        "Productos e ingredientes con stock igual o inferior al mínimo"));

                    pagina.Content().PaddingVertical(12).Column(columna =>
                    {
                        columna.Spacing(14);

                        columna.Item().Row(fila =>
                        {
                            fila.Spacing(8);
                            fila.RelativeItem().Element(c => Indicador(c, "Productos bajo el mínimo", reporte.CantidadProductos.ToString("N0", cultura)));
                            fila.RelativeItem().Element(c => Indicador(c, "Ingredientes bajo el mínimo", reporte.CantidadIngredientes.ToString("N0", cultura)));
                        });

                        if (reporte.Items.Count == 0)
                        {
                            columna.Item().PaddingTop(20).AlignCenter()
                                .Text("No hay productos ni ingredientes por debajo del stock mínimo.").Italic();
                            return;
                        }

                        columna.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(2);
                                c.RelativeColumn(4);
                                c.RelativeColumn(2);
                                c.RelativeColumn(2);
                                c.ConstantColumn(50);
                                c.ConstantColumn(58);
                                c.ConstantColumn(62);
                            });

                            tabla.Header(h =>
                            {
                                h.Cell().Element(CeldaEncabezado).Text("Tipo");
                                h.Cell().Element(CeldaEncabezado).Text("Nombre");
                                h.Cell().Element(CeldaEncabezado).Text("Categoría");
                                h.Cell().Element(CeldaEncabezado).Text("Unidad");
                                h.Cell().Element(CeldaEncabezado).AlignRight().Text("Stock");
                                h.Cell().Element(CeldaEncabezado).AlignRight().Text("Mínimo");
                                h.Cell().Element(CeldaEncabezado).AlignRight().Text("Faltante");
                            });

                            foreach (var item in reporte.Items)
                            {
                                tabla.Cell().Element(Celda).Text(item.Tipo);
                                tabla.Cell().Element(Celda).Text(item.Nombre);
                                tabla.Cell().Element(Celda).Text(item.Categoria ?? "-");
                                tabla.Cell().Element(Celda).Text(item.Unidad ?? "-");
                                tabla.Cell().Element(Celda).AlignRight().Text(item.Stock.ToString("N0", cultura));
                                tabla.Cell().Element(Celda).AlignRight().Text(item.StockMin.ToString("N0", cultura));
                                tabla.Cell().Element(Celda).AlignRight().Text(item.Faltante.ToString("N0", cultura))
                                    .FontColor(item.Faltante > 0 ? Colors.Red.Darken2 : Colors.Black).Bold();
                            }
                        });

                        columna.Item().Text("Faltante: unidades necesarias para volver al stock mínimo.")
                            .FontSize(8).FontColor(Colors.Grey.Darken1);
                    });

                    pagina.Footer().Element(PiePagina);
                });
            }).GeneratePdf();
        }

        public static byte[] GraficoVentasPorMes(ReporteVentasDTO reporte, int ancho = 1000, int alto = 430)
        {
            var plot = new ScottPlot.Plot();

            var meses = reporte.PorMes
                .Select(m => new { m.Anio, m.Mes, m.Periodo })
                .Distinct()
                .OrderBy(m => m.Anio).ThenBy(m => m.Mes)
                .ToList();

            var sucursales = reporte.PorSucursal.Select(s => s.SucursalId).ToList();
            var cantidad = Math.Max(1, sucursales.Count);
            var anchoGrupo = 0.8;
            var anchoBarra = anchoGrupo / cantidad;
            var barras = new List<ScottPlot.Bar>();

            for (var m = 0; m < meses.Count; m++)
            {
                for (var s = 0; s < sucursales.Count; s++)
                {
                    var total = reporte.PorMes
                        .Where(x => x.Anio == meses[m].Anio && x.Mes == meses[m].Mes && x.SucursalId == sucursales[s])
                        .Sum(x => x.Total);

                    barras.Add(new ScottPlot.Bar
                    {
                        Position = m - anchoGrupo / 2 + anchoBarra * (s + 0.5),
                        Value = (double)total,
                        Size = anchoBarra * 0.92,
                        FillColor = ScottPlot.Color.FromHex(coloresSucursales[s % coloresSucursales.Length]),
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
                LabelFormatter = valor => valor.ToString("C0", cultura)
            };

            plot.Axes.Margins(bottom: 0, top: 0.1);
            plot.Title("Ventas por mes y sucursal");

            for (var s = 0; s < reporte.PorSucursal.Count; s++)
            {
                plot.Legend.ManualItems.Add(new ScottPlot.LegendItem
                {
                    LabelText = reporte.PorSucursal[s].SucursalNombre,
                    FillColor = ScottPlot.Color.FromHex(coloresSucursales[s % coloresSucursales.Length])
                });
            }

            plot.Legend.Orientation = ScottPlot.Orientation.Horizontal;
            plot.ShowLegend(ScottPlot.Edge.Bottom);

            return plot.GetImageBytes(ancho, alto, ScottPlot.ImageFormat.Png);
        }

        private static void ConfigurarPagina(PageDescriptor pagina)
        {
            pagina.Size(PageSizes.A4);
            pagina.Margin(32);
            pagina.DefaultTextStyle(t => t.FontSize(10));
        }

        private static void Encabezado(IContainer contenedor, string titulo, string subtitulo)
        {
            contenedor.BorderBottom(1).BorderColor(Colors.Grey.Lighten1).PaddingBottom(8).Column(columna =>
            {
                columna.Item().Text("Gestión de Stock · Tienda gastronómica").FontSize(9).FontColor(Colors.Grey.Darken1);
                columna.Item().Text(titulo).FontSize(16).Bold().FontColor("#1F4E79");
                columna.Item().Row(fila =>
                {
                    fila.RelativeItem().Text(subtitulo).FontSize(10).FontColor(Colors.Grey.Darken2);
                    fila.AutoItem().AlignBottom().PaddingLeft(12)
                        .Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(8).FontColor(Colors.Grey.Darken1);
                });
            });
        }

        private static void Indicador(IContainer contenedor, string titulo, string valor)
        {
            contenedor.Border(1).BorderColor(Colors.Grey.Lighten2).Background(Colors.Grey.Lighten4)
                .Padding(8).Column(columna =>
                {
                    columna.Item().Text(titulo).FontSize(8).FontColor(Colors.Grey.Darken1);
                    columna.Item().Text(valor).FontSize(14).Bold();
                });
        }

        private static IContainer CeldaEncabezado(IContainer contenedor) =>
            contenedor.Background("#1F4E79").PaddingVertical(5).PaddingHorizontal(6).DefaultTextStyle(t => t.FontColor(Colors.White).Bold());

        private static IContainer Celda(IContainer contenedor) =>
            contenedor.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(4).PaddingHorizontal(6);

        private static void PiePagina(IContainer contenedor)
        {
            contenedor.AlignCenter().Text(texto =>
            {
                texto.DefaultTextStyle(t => t.FontSize(8).FontColor(Colors.Grey.Darken1));
                texto.Span("Página ");
                texto.CurrentPageNumber();
                texto.Span(" de ");
                texto.TotalPages();
            });
        }
    }
}
