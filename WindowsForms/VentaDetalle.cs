using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class VentaDetalle : Form
    {
        private readonly VentaDTO venta;
        private readonly FormMode mode;
        private readonly List<DetalleVentaDTO> itemsLocales;
        private readonly Dictionary<int, int> cantidadesOriginales;
        private List<ProductoDTO> productos = new();

        public VentaDTO? VentaGuardada { get; private set; }

        public VentaDetalle(FormMode mode, VentaDTO venta)
        {
            InitializeComponent();
            ConfigurarColumnas();

            this.mode = mode;
            this.venta = venta;

            itemsLocales = venta.Detalles.Select(Clonar).ToList();
            cantidadesOriginales = mode == FormMode.Update
                ? venta.Detalles.ToDictionary(d => d.ProductoId, d => d.Cantidad)
                : new Dictionary<int, int>();
        }

        private void ConfigurarColumnas()
        {
            itemsDataGridView.AutoGenerateColumns = false;

            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductoNombre",
                HeaderText = "Producto",
                DataPropertyName = "ProductoNombre",
                Width = 290
            });

            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                Width = 100,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrecioUnitario",
                HeaderText = "Precio unitario",
                DataPropertyName = "PrecioUnitario",
                Width = 150,
                DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Subtotal",
                HeaderText = "Subtotal",
                DataPropertyName = "Subtotal",
                Width = 150,
                DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }

        private async void VentaDetalle_Load(object sender, EventArgs e)
        {
            try
            {
                DeshabilitarControles();
                AplicarModo();
                await CargarDatosAsync();
                await MostrarVentaAsync();
            }
            catch (UnauthorizedAccessException)
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar los datos de la venta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void AplicarModo()
        {
            var esAlta = mode == FormMode.Add;

            Text = esAlta ? "Nueva venta" : $"Modificar venta N° {venta.Id}";

            idLabel.Visible = !esAlta;
            idTextBox.Visible = !esAlta;
        }

        private async Task CargarDatosAsync()
        {
            var sucursales = (await SucursalApiClient.GetAllAsync())
                .Where(s => s.Activa || s.Id == venta.SucursalId)
                .ToList();

            sucursalComboBox.DataSource = sucursales;
            sucursalComboBox.DisplayMember = nameof(SucursalDTO.Nombre);
            sucursalComboBox.ValueMember = nameof(SucursalDTO.Id);
            sucursalComboBox.SelectedIndex = -1;

            productos = (await ProductoApiClient.GetAllAsync()).ToList();
        }

        private async Task MostrarVentaAsync()
        {
            if (mode == FormMode.Add)
            {
                var usuario = await AuthServiceProvider.Instance.GetUsuarioAsync();

                fechaTextBox.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                vendedorTextBox.Text = usuario?.NombreCompleto;

                if (usuario?.SucursalId is int sucursalId &&
                    sucursalComboBox.DataSource is List<SucursalDTO> sucursales &&
                    sucursales.Any(s => s.Id == sucursalId))
                {
                    sucursalComboBox.SelectedValue = sucursalId;
                }
            }
            else
            {
                idTextBox.Text = venta.Id.ToString();
                fechaTextBox.Text = venta.FechaHora.ToString("dd/MM/yyyy HH:mm");
                vendedorTextBox.Text = venta.UsuarioNombre;
                sucursalComboBox.SelectedValue = venta.SucursalId;
            }

            RefrescarItems();
        }

        private void sucursalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sucursalComboBox.SelectedValue != null)
                errorProvider.SetError(sucursalComboBox, string.Empty);
        }

        private void RefrescarItems(int? productoSeleccionadoId = null)
        {
            itemsDataGridView.DataSource = null;
            itemsDataGridView.DataSource = itemsLocales.OrderBy(i => i.ProductoNombre).ToList();

            if (productoSeleccionadoId != null)
            {
                foreach (DataGridViewRow fila in itemsDataGridView.Rows)
                {
                    if (fila.DataBoundItem is DetalleVentaDTO item && item.ProductoId == productoSeleccionadoId)
                    {
                        fila.Selected = true;
                        break;
                    }
                }
            }

            var unidades = itemsLocales.Sum(i => i.Cantidad);
            var total = itemsLocales.Sum(i => i.Subtotal);

            totalLabel.Text = unidades == 0
                ? "Total: $ 0,00"
                : $"{unidades} {(unidades == 1 ? "unidad" : "unidades")}   ·   Total: {total:C2}";

            ActualizarBotonesDeItems();
        }

        private void ActualizarBotonesDeItems()
        {
            var hayItems = itemsLocales.Count > 0;

            modificarItemButton.Enabled = hayItems;
            quitarItemButton.Enabled = hayItems;
        }

        private DetalleVentaDTO? ObtenerItemSeleccionado()
        {
            if (itemsDataGridView.SelectedRows.Count == 0)
                return null;

            return itemsDataGridView.SelectedRows[0].DataBoundItem as DetalleVentaDTO;
        }

        private List<ProductoDisponible> OpcionesDeProducto(int? soloProductoId = null)
        {
            var enUso = itemsLocales.Select(i => i.ProductoId).ToHashSet();

            return productos
                .Where(p => soloProductoId != null ? p.Id == soloProductoId : !enUso.Contains(p.Id))
                .OrderBy(p => p.Nombre)
                .Select(p => new ProductoDisponible(
                    p.Id,
                    p.Nombre,
                    p.Unidad,
                    p.Stock + cantidadesOriginales.GetValueOrDefault(p.Id),
                    p.Precio))
                .ToList();
        }

        private void agregarItemButton_Click(object sender, EventArgs e)
        {
            var opciones = OpcionesDeProducto();
            if (opciones.Count == 0)
            {
                MessageBox.Show("Todos los productos ya figuran en la venta.", "Agregar producto",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var item = new DetalleVentaItem(FormMode.Add, new DetalleVentaDTO(), opciones);
            if (item.ShowDialog() != DialogResult.OK)
                return;

            itemsLocales.Add(item.Item);
            errorProvider.SetError(agregarItemButton, string.Empty);
            RefrescarItems(item.Item.ProductoId);
        }

        private void modificarItemButton_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerItemSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un producto de la venta.", "Modificar producto",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var item = new DetalleVentaItem(FormMode.Update, Clonar(seleccionado),
                                                  OpcionesDeProducto(seleccionado.ProductoId));
            if (item.ShowDialog() != DialogResult.OK)
                return;

            var indice = itemsLocales.FindIndex(i => i.ProductoId == seleccionado.ProductoId);
            itemsLocales[indice] = item.Item;
            RefrescarItems(item.Item.ProductoId);
        }

        private void itemsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                modificarItemButton_Click(sender, e);
        }

        private void quitarItemButton_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerItemSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un producto de la venta.", "Quitar producto",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmar = MessageBox.Show(
                $"¿Quitar \"{seleccionado.ProductoNombre}\" de la venta?",
                "Quitar producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
                return;

            itemsLocales.RemoveAll(i => i.ProductoId == seleccionado.ProductoId);
            RefrescarItems();
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidarVenta())
                return;

            try
            {
                DeshabilitarControles();

                venta.SucursalId = (int)sucursalComboBox.SelectedValue!;
                venta.Detalles = itemsLocales.Select(Clonar).ToList();

                if (mode == FormMode.Update)
                {
                    await VentaApiClient.UpdateAsync(venta);
                    VentaGuardada = venta;
                }
                else
                {
                    VentaGuardada = await VentaApiClient.AddAsync(venta);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (UnauthorizedAccessException)
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar la venta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private bool ValidarVenta()
        {
            errorProvider.SetError(sucursalComboBox, string.Empty);
            errorProvider.SetError(agregarItemButton, string.Empty);

            var esValido = true;

            if (sucursalComboBox.SelectedValue == null)
            {
                errorProvider.SetError(sucursalComboBox, "Seleccione la sucursal donde se realizó la venta");
                esValido = false;
            }

            if (itemsLocales.Count == 0)
            {
                errorProvider.SetError(agregarItemButton, "Agregue al menos un producto a la venta");
                esValido = false;
            }

            return esValido;
        }

        private static DetalleVentaDTO Clonar(DetalleVentaDTO item) => new()
        {
            ProductoId = item.ProductoId,
            ProductoNombre = item.ProductoNombre,
            Cantidad = item.Cantidad,
            PrecioUnitario = item.PrecioUnitario
        };

        private void DeshabilitarControles()
        {
            sucursalComboBox.Enabled = false;
            itemsDataGridView.Enabled = false;
            agregarItemButton.Enabled = false;
            modificarItemButton.Enabled = false;
            quitarItemButton.Enabled = false;
            aceptarButton.Enabled = false;
            cancelarButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void HabilitarControles()
        {
            sucursalComboBox.Enabled = true;
            itemsDataGridView.Enabled = true;
            agregarItemButton.Enabled = true;
            aceptarButton.Enabled = true;
            cancelarButton.Enabled = true;
            ActualizarBotonesDeItems();
            Cursor = Cursors.Default;
        }
    }
}
