using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class ReporteBajoStock : Form
    {
        private ReporteBajoStockDTO? reporte;

        public ReporteBajoStock()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            itemsDataGridView.AutoGenerateColumns = false;

            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tipo",
                DataPropertyName = "Tipo",
                Width = 95
            });

            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 200
            });

            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Categoría",
                DataPropertyName = "Categoria",
                Width = 110
            });

            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Unidad",
                DataPropertyName = "Unidad",
                Width = 80
            });

            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Stock",
                DataPropertyName = "Stock",
                Width = 80,
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Mínimo",
                DataPropertyName = "StockMin",
                Width = 80,
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Faltante",
                DataPropertyName = "Faltante",
                Width = 85,
                DefaultCellStyle =
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    ForeColor = Color.DarkRed,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                }
            });
        }

        private async void ReporteBajoStock_Load(object sender, EventArgs e)
        {
            await GenerarAsync();
        }

        private async Task GenerarAsync()
        {
            try
            {
                DeshabilitarControles();
                itemsDataGridView.DataSource = null;

                reporte = await ReporteApiClient.GetBajoStockAsync();
                itemsDataGridView.DataSource = reporte.Items;

                resumenLabel.Text = reporte.Items.Count == 0
                    ? "No hay productos ni ingredientes por debajo del stock mínimo."
                    : $"{reporte.CantidadProductos} productos y {reporte.CantidadIngredientes} ingredientes requieren reposición" +
                      $"  ·  situación al {reporte.GeneradoEl:dd/MM/yyyy HH:mm}";
            }
            catch (UnauthorizedAccessException)
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo generar el reporte",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            await GenerarAsync();
        }

        private async void exportarButton_Click(object sender, EventArgs e)
        {
            try
            {
                DeshabilitarControles();

                await ArchivoPdf.ExportarAsync(this, $"bajo_stock_{DateTime.Now:yyyyMMdd}.pdf",
                    ReporteApiClient.GetBajoStockPdfAsync);
            }
            catch (UnauthorizedAccessException)
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo exportar el reporte",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void cerrarButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DeshabilitarControles()
        {
            actualizarButton.Enabled = false;
            exportarButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void HabilitarControles()
        {
            actualizarButton.Enabled = true;
            exportarButton.Enabled = reporte != null;
            Cursor = Cursors.Default;
        }
    }
}
