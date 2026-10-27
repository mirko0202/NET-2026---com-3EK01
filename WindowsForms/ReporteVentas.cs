using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class ReporteVentas : Form
    {
        private ReporteVentasDTO? reporte;

        public ReporteVentas()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            porSucursalDataGridView.AutoGenerateColumns = false;

            porSucursalDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Sucursal",
                DataPropertyName = "Sucursal",
                FillWeight = 34
            });

            porSucursalDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Ventas",
                DataPropertyName = "Ventas",
                FillWeight = 14,
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            porSucursalDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Unidades",
                DataPropertyName = "Unidades",
                FillWeight = 16,
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            porSucursalDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Total",
                DataPropertyName = "Total",
                FillWeight = 22,
                DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            porSucursalDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "% del total",
                DataPropertyName = "Porcentaje",
                FillWeight = 14,
                DefaultCellStyle = { Format = "P1", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            productosDataGridView.AutoGenerateColumns = false;

            productosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "#",
                DataPropertyName = "Puesto",
                FillWeight = 8
            });

            productosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Producto",
                DataPropertyName = "Producto",
                FillWeight = 48
            });

            productosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Unidades",
                DataPropertyName = "Unidades",
                FillWeight = 18,
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            productosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Total",
                DataPropertyName = "Total",
                FillWeight = 26,
                DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }

        private async void ReporteVentas_Load(object sender, EventArgs e)
        {
            var hoy = DateTime.Today;
            desdeDateTimePicker.Value = new DateTime(hoy.Year, hoy.Month, 1).AddMonths(-2);
            hastaDateTimePicker.Value = hoy;

            try
            {
                var opciones = new List<SucursalDTO> { new() { Id = 0, Nombre = "Todas las sucursales" } };
                opciones.AddRange(await SucursalApiClient.GetAllAsync());

                sucursalComboBox.DataSource = opciones;
                sucursalComboBox.DisplayMember = nameof(SucursalDTO.Nombre);
                sucursalComboBox.ValueMember = nameof(SucursalDTO.Id);
            }
            catch (UnauthorizedAccessException)
            {
                Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar las sucursales",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            await GenerarAsync();
        }

        private int? SucursalSeleccionada =>
            sucursalComboBox.SelectedValue is int id && id > 0 ? id : null;

        private bool ValidarPeriodo()
        {
            if (desdeDateTimePicker.Value.Date <= hastaDateTimePicker.Value.Date)
                return true;

            MessageBox.Show("La fecha desde no puede ser posterior a la fecha hasta.", "Período inválido",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        private async Task GenerarAsync()
        {
            if (!ValidarPeriodo())
                return;

            try
            {
                DeshabilitarControles();

                reporte = await ReporteApiClient.GetVentasAsync(
                    desdeDateTimePicker.Value.Date, hastaDateTimePicker.Value.Date, SucursalSeleccionada);

                MostrarReporte(reporte);
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

        private void MostrarReporte(ReporteVentasDTO datos)
        {
            ventasValorLabel.Text = datos.CantidadVentas.ToString("N0");
            unidadesValorLabel.Text = datos.Unidades.ToString("N0");
            totalValorLabel.Text = datos.Total.ToString("C2");
            ticketValorLabel.Text = datos.TicketPromedio.ToString("C2");

            DibujarGrafico();

            porSucursalDataGridView.DataSource = datos.PorSucursal
                .Select(s => new
                {
                    Sucursal = s.SucursalNombre,
                    Ventas = s.CantidadVentas,
                    s.Unidades,
                    s.Total,
                    Porcentaje = datos.Total == 0 ? 0 : s.Total / datos.Total
                })
                .ToList();

            productosDataGridView.DataSource = datos.ProductosMasVendidos
                .Select((p, indice) => new
                {
                    Puesto = indice + 1,
                    Producto = p.ProductoNombre,
                    p.Unidades,
                    p.Total
                })
                .ToList();

            porSucursalDataGridView.ClearSelection();
            productosDataGridView.ClearSelection();

            generadoLabel.Text = $"{datos.SucursalNombre}  ·  del {datos.Desde:dd/MM/yyyy} al {datos.Hasta:dd/MM/yyyy}" +
                                 $"  ·  generado {DateTime.Now:dd/MM/yyyy HH:mm}";
        }

        private void DibujarGrafico()
        {
            var ancho = graficoPictureBox.ClientSize.Width;
            var alto = graficoPictureBox.ClientSize.Height;

            if (reporte == null || ancho < 50 || alto < 50)
                return;

            var anterior = graficoPictureBox.Image;
            graficoPictureBox.Image = GraficoVentas.Generar(reporte, ancho, alto, graficoPictureBox.DeviceDpi / 96.0);
            anterior?.Dispose();
        }

        private void graficoPictureBox_SizeChanged(object sender, EventArgs e)
        {
            DibujarGrafico();
        }

        private async void generarButton_Click(object sender, EventArgs e)
        {
            await GenerarAsync();
        }

        private async void exportarButton_Click(object sender, EventArgs e)
        {
            if (!ValidarPeriodo())
                return;

            var desde = desdeDateTimePicker.Value.Date;
            var hasta = hastaDateTimePicker.Value.Date;
            var sucursalId = SucursalSeleccionada;

            try
            {
                DeshabilitarControles();

                await ArchivoPdf.ExportarAsync(this, $"ventas_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.pdf",
                    () => ReporteApiClient.GetVentasPdfAsync(desde, hasta, sucursalId));
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
            filtrosGroupBox.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void HabilitarControles()
        {
            filtrosGroupBox.Enabled = true;
            exportarButton.Enabled = reporte != null;
            Cursor = Cursors.Default;
        }
    }
}
