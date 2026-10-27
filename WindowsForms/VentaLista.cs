using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class VentaLista : Form
    {
        private bool esAdministrador;

        public VentaLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            ventasDataGridView.AutoGenerateColumns = false;

            ventasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "N°",
                DataPropertyName = "Id",
                Width = 60
            });

            ventasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaHora",
                HeaderText = "Fecha y hora",
                DataPropertyName = "FechaHora",
                Width = 140,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
            });

            ventasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SucursalNombre",
                HeaderText = "Sucursal",
                DataPropertyName = "SucursalNombre",
                Width = 190
            });

            ventasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UsuarioNombre",
                HeaderText = "Vendedor",
                DataPropertyName = "UsuarioNombre",
                Width = 210
            });

            ventasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CantidadProductos",
                HeaderText = "Unidades",
                DataPropertyName = "CantidadProductos",
                Width = 100,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            ventasDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total",
                DataPropertyName = "Total",
                Width = 150,
                DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            detallesDataGridView.AutoGenerateColumns = false;

            detallesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductoNombre",
                HeaderText = "Producto",
                DataPropertyName = "ProductoNombre",
                Width = 300
            });

            detallesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                Width = 100,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            detallesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrecioUnitario",
                HeaderText = "Precio unitario",
                DataPropertyName = "PrecioUnitario",
                Width = 160,
                DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            detallesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Subtotal",
                HeaderText = "Subtotal",
                DataPropertyName = "Subtotal",
                Width = 160,
                DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }

        private async void VentaLista_Load(object sender, EventArgs e)
        {
            esAdministrador = await AuthServiceProvider.Instance.EsAdministradorAsync();
            AplicarAutorizacion();
            await CargarAsync();
        }

        private void AplicarAutorizacion()
        {
            actualizarButton.Visible = esAdministrador;
            eliminarButton.Visible = esAdministrador;

            Text = esAdministrador ? "Ventas" : "Ventas (registro y consulta)";
        }

        private async Task CargarAsync(int? ventaSeleccionadaId = null)
        {
            try
            {
                DeshabilitarControles();
                ventasDataGridView.DataSource = null;

                var lista = (await VentaApiClient.GetAllAsync()).ToList();
                ventasDataGridView.DataSource = lista;

                SeleccionarVenta(ventaSeleccionadaId);
                MostrarDetalleSeleccionado();

                resultadosLabel.Text = lista.Count == 1
                    ? "1 venta registrada"
                    : $"{lista.Count} ventas registradas";
            }
            catch (UnauthorizedAccessException)
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar ventas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void SeleccionarVenta(int? ventaId)
        {
            if (ventaId == null)
                return;

            foreach (DataGridViewRow fila in ventasDataGridView.Rows)
            {
                if (fila.DataBoundItem is VentaDTO venta && venta.Id == ventaId)
                {
                    fila.Selected = true;
                    ventasDataGridView.FirstDisplayedScrollingRowIndex = fila.Index;
                    return;
                }
            }
        }

        private void ventasDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            MostrarDetalleSeleccionado();
        }

        private void MostrarDetalleSeleccionado()
        {
            var venta = ObtenerSeleccionada();

            detallesDataGridView.DataSource = venta?.Detalles.ToList();

            detalleTituloLabel.Text = venta == null
                ? "Productos de la venta seleccionada"
                : $"Productos de la venta N° {venta.Id}   ·   {venta.SucursalNombre}   ·   Total {venta.Total:C2}";
        }

        private VentaDTO? ObtenerSeleccionada()
        {
            if (ventasDataGridView.SelectedRows.Count == 0)
                return null;

            return ventasDataGridView.SelectedRows[0].DataBoundItem as VentaDTO;
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            int? registradaId = null;

            using (var detalle = new VentaDetalle(FormMode.Add, new VentaDTO()))
            {
                if (detalle.ShowDialog() == DialogResult.OK)
                    registradaId = detalle.VentaGuardada?.Id;
            }

            await CargarAsync(registradaId);
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            var seleccionada = ObtenerSeleccionada();
            if (seleccionada == null)
            {
                MessageBox.Show("Seleccione una venta de la lista.", "Modificar venta",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DeshabilitarControles();

                var venta = await VentaApiClient.GetAsync(seleccionada.Id);

                using var detalle = new VentaDetalle(FormMode.Update, venta);
                detalle.ShowDialog();
            }
            catch (UnauthorizedAccessException)
            {
                Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al abrir la venta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }

            await CargarAsync(seleccionada.Id);
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            var venta = ObtenerSeleccionada();
            if (venta == null)
            {
                MessageBox.Show("Seleccione una venta de la lista.", "Eliminar venta",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmar = MessageBox.Show(
                $"¿Está seguro que desea eliminar la venta N° {venta.Id} por {venta.Total:C2}?\n\n" +
                "Se repondrá el stock de los productos vendidos.",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
                return;

            try
            {
                DeshabilitarControles();
                await VentaApiClient.DeleteAsync(venta.Id);
            }
            catch (UnauthorizedAccessException)
            {
                Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al eliminar la venta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }

            await CargarAsync();
        }

        private void cerrarButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DeshabilitarControles()
        {
            ventasDataGridView.Enabled = false;
            agregarButton.Enabled = false;
            actualizarButton.Enabled = false;
            eliminarButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void HabilitarControles()
        {
            ventasDataGridView.Enabled = true;

            var hayFilas = ventasDataGridView.Rows.Count > 0;

            agregarButton.Enabled = true;
            actualizarButton.Enabled = esAdministrador && hayFilas;
            eliminarButton.Enabled = esAdministrador && hayFilas;

            Cursor = Cursors.Default;
        }
    }
}
