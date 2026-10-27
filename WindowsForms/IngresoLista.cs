using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class IngresoLista : Form
    {
        private bool esAdministrador;
        private List<ProductoDTO> productos = new();
        private List<string> proveedores = new();

        public IngresoLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            ingresosDataGridView.AutoGenerateColumns = false;

            ingresosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "N°",
                DataPropertyName = "Id",
                Width = 50
            });

            ingresosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaHora",
                HeaderText = "Fecha",
                DataPropertyName = "FechaHora",
                Width = 115,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
            });

            ingresosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductoNombre",
                HeaderText = "Producto",
                DataPropertyName = "ProductoNombre",
                Width = 180
            });

            ingresosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                Width = 75,
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            ingresosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Unidad",
                HeaderText = "Unidad",
                DataPropertyName = "Unidad",
                Width = 70
            });

            ingresosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SucursalNombre",
                HeaderText = "Sucursal",
                DataPropertyName = "SucursalNombre",
                Width = 130
            });

            ingresosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Proveedor",
                HeaderText = "Proveedor",
                DataPropertyName = "Proveedor",
                Width = 170
            });

            ingresosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Comprobante",
                HeaderText = "Comprobante",
                DataPropertyName = "Comprobante",
                Width = 110
            });

            ingresosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UsuarioNombre",
                HeaderText = "Registró",
                DataPropertyName = "UsuarioNombre",
                Width = 150
            });
        }

        private async void IngresoLista_Load(object sender, EventArgs e)
        {
            esAdministrador = await AuthServiceProvider.Instance.EsAdministradorAsync();
            AplicarAutorizacion();

            if (!await CargarProductosAsync())
                return;

            await BuscarYCargarAsync();
        }

        private void AplicarAutorizacion()
        {
            agregarButton.Visible = esAdministrador;
            actualizarButton.Visible = esAdministrador;
            eliminarButton.Visible = esAdministrador;

            Text = esAdministrador ? "Ingresos de mercadería" : "Ingresos de mercadería (solo consulta)";
        }

        private async Task<bool> CargarProductosAsync()
        {
            try
            {
                productos = (await ProductoApiClient.GetAllAsync()).OrderBy(p => p.Nombre).ToList();

                var opciones = new List<ProductoDTO> { new() { Id = 0, Nombre = "Todos" } };
                opciones.AddRange(productos);

                productoComboBox.DataSource = opciones;
                productoComboBox.DisplayMember = nameof(ProductoDTO.Nombre);
                productoComboBox.ValueMember = nameof(ProductoDTO.Id);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                Close();
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar los productos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return false;
            }
        }

        private async Task BuscarYCargarAsync()
        {
            DateTime? desde = desdeDateTimePicker.Checked ? desdeDateTimePicker.Value.Date : null;
            DateTime? hasta = hastaDateTimePicker.Checked ? hastaDateTimePicker.Value.Date : null;

            if (desde > hasta)
            {
                MessageBox.Show("La fecha desde no puede ser posterior a la fecha hasta.", "Filtro de fechas",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DeshabilitarControles();
                ingresosDataGridView.DataSource = null;

                int? productoId = productoComboBox.SelectedValue is int id && id > 0 ? id : null;

                var lista = (await IngresoProductoApiClient.GetAllAsync(productoId, desde, hasta)).ToList();
                ingresosDataGridView.DataSource = lista;

                if (productoId == null && desde == null && hasta == null)
                {
                    proveedores = lista.Select(i => i.Proveedor)
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .OrderBy(p => p)
                        .ToList();
                }

                var unidades = lista.Sum(i => i.Cantidad);
                resultadosLabel.Text = lista.Count == 1
                    ? $"1 ingreso  ·  {unidades:N0} unidades"
                    : $"{lista.Count} ingresos  ·  {unidades:N0} unidades";
            }
            catch (UnauthorizedAccessException)
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar los ingresos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private IngresoProductoDTO? ObtenerSeleccionado()
        {
            if (ingresosDataGridView.SelectedRows.Count == 0)
                return null;

            return ingresosDataGridView.SelectedRows[0].DataBoundItem as IngresoProductoDTO;
        }

        private async void buscarButton_Click(object sender, EventArgs e)
        {
            await BuscarYCargarAsync();
        }

        private async void limpiarButton_Click(object sender, EventArgs e)
        {
            productoComboBox.SelectedIndex = 0;
            desdeDateTimePicker.Checked = false;
            hastaDateTimePicker.Checked = false;
            await BuscarYCargarAsync();
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            var nuevo = new IngresoProductoDTO { FechaHora = DateTime.Now };

            using var detalle = new IngresoDetalle(FormMode.Add, nuevo, proveedores);
            if (detalle.ShowDialog() == DialogResult.OK)
                await BuscarYCargarAsync();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un ingreso de la lista.", "Modificar ingreso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DeshabilitarControles();

                var ingreso = await IngresoProductoApiClient.GetAsync(seleccionado.Id);

                using var detalle = new IngresoDetalle(FormMode.Update, ingreso, proveedores);
                detalle.ShowDialog();
            }
            catch (UnauthorizedAccessException)
            {
                Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al abrir el ingreso",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }

            await BuscarYCargarAsync();
        }

        private void ingresosDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && esAdministrador)
                actualizarButton_Click(sender, e);
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            var ingreso = ObtenerSeleccionado();
            if (ingreso == null)
            {
                MessageBox.Show("Seleccione un ingreso de la lista.", "Eliminar ingreso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmar = MessageBox.Show(
                $"¿Eliminar el ingreso N° {ingreso.Id} ({ingreso.Cantidad} {ingreso.Unidad} de \"{ingreso.ProductoNombre}\")?\n\n" +
                "Se descontará esa cantidad del stock actual del producto.",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
                return;

            try
            {
                DeshabilitarControles();
                await IngresoProductoApiClient.DeleteAsync(ingreso.Id);
            }
            catch (UnauthorizedAccessException)
            {
                Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo eliminar el ingreso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                HabilitarControles();
            }

            await BuscarYCargarAsync();
        }

        private void cerrarButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DeshabilitarControles()
        {
            filtrosGroupBox.Enabled = false;
            ingresosDataGridView.Enabled = false;
            agregarButton.Enabled = false;
            actualizarButton.Enabled = false;
            eliminarButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void HabilitarControles()
        {
            filtrosGroupBox.Enabled = true;
            ingresosDataGridView.Enabled = true;

            var hayFilas = ingresosDataGridView.Rows.Count > 0;

            agregarButton.Enabled = esAdministrador;
            actualizarButton.Enabled = esAdministrador && hayFilas;
            eliminarButton.Enabled = esAdministrador && hayFilas;

            Cursor = Cursors.Default;
        }
    }
}
