using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class IngresoDetalle : Form
    {
        private readonly IngresoProductoDTO ingreso;
        private readonly FormMode mode;
        private readonly List<string> proveedores;
        private List<ProductoDTO> productos = new();

        public IngresoDetalle(FormMode mode, IngresoProductoDTO ingreso, IEnumerable<string> proveedores)
        {
            InitializeComponent();

            this.mode = mode;
            this.ingreso = ingreso;
            this.proveedores = proveedores.ToList();
        }

        private ProductoDTO? ProductoSeleccionado => productoComboBox.SelectedItem as ProductoDTO;

        private async void IngresoDetalle_Load(object sender, EventArgs e)
        {
            try
            {
                DeshabilitarControles();
                AplicarModo();
                await CargarDatosAsync();
                await MostrarIngresoAsync();
            }
            catch (UnauthorizedAccessException)
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar los datos del ingreso",
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

            Text = esAlta ? "Registrar ingreso de mercadería" : $"Modificar ingreso N° {ingreso.Id}";

            idLabel.Visible = !esAlta;
            idTextBox.Visible = !esAlta;
            registradoLabel.Visible = !esAlta;
            registradoTextBox.Visible = !esAlta;

            if (esAlta)
            {
                var desplazamiento = registradoTextBox.Bottom - observacionTextBox.Bottom;
                aceptarButton.Top -= desplazamiento;
                cancelarButton.Top -= desplazamiento;
                ClientSize = new Size(ClientSize.Width, ClientSize.Height - desplazamiento);
            }

            fechaDateTimePicker.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);
            proveedorTextBox.AutoCompleteCustomSource.AddRange(proveedores.ToArray());
        }

        private async Task CargarDatosAsync()
        {
            productos = (await ProductoApiClient.GetAllAsync()).OrderBy(p => p.Nombre).ToList();

            productoComboBox.DataSource = productos;
            productoComboBox.DisplayMember = nameof(ProductoDTO.Nombre);
            productoComboBox.ValueMember = nameof(ProductoDTO.Id);
            productoComboBox.SelectedIndex = -1;

            var sucursales = (await SucursalApiClient.GetAllAsync())
                .Where(s => s.Activa || s.Id == ingreso.SucursalId)
                .ToList();

            sucursalComboBox.DataSource = sucursales;
            sucursalComboBox.DisplayMember = nameof(SucursalDTO.Nombre);
            sucursalComboBox.ValueMember = nameof(SucursalDTO.Id);
            sucursalComboBox.SelectedIndex = -1;
        }

        private async Task MostrarIngresoAsync()
        {
            fechaDateTimePicker.Value = ingreso.FechaHora == default ? DateTime.Now : ingreso.FechaHora;

            if (mode == FormMode.Add)
            {
                var usuario = await AuthServiceProvider.Instance.GetUsuarioAsync();
                if (usuario?.SucursalId is int sucursalId &&
                    sucursalComboBox.DataSource is List<SucursalDTO> sucursales &&
                    sucursales.Any(s => s.Id == sucursalId))
                {
                    sucursalComboBox.SelectedValue = sucursalId;
                }

                return;
            }

            idTextBox.Text = ingreso.Id.ToString();
            productoComboBox.SelectedValue = ingreso.ProductoId;
            sucursalComboBox.SelectedValue = ingreso.SucursalId;
            cantidadNumericUpDown.Value = Math.Clamp(ingreso.Cantidad, (int)cantidadNumericUpDown.Minimum,
                                                     (int)cantidadNumericUpDown.Maximum);
            proveedorTextBox.Text = ingreso.Proveedor;
            comprobanteTextBox.Text = ingreso.Comprobante ?? string.Empty;
            observacionTextBox.Text = ingreso.Observacion ?? string.Empty;
            registradoTextBox.Text = ingreso.UsuarioNombre;
        }

        private void productoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var producto = ProductoSeleccionado;

            stockLabel.Text = producto == null
                ? string.Empty
                : $"Stock actual: {producto.Stock:N0} {producto.Unidad}  ·  mínimo {producto.StockMin:N0}".Trim();
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidarIngreso())
                return;

            try
            {
                DeshabilitarControles();

                ingreso.FechaHora = fechaDateTimePicker.Value;
                ingreso.ProductoId = ProductoSeleccionado!.Id;
                ingreso.SucursalId = (int)sucursalComboBox.SelectedValue!;
                ingreso.Cantidad = (int)cantidadNumericUpDown.Value;
                ingreso.Proveedor = proveedorTextBox.Text.Trim();
                ingreso.Comprobante = string.IsNullOrWhiteSpace(comprobanteTextBox.Text)
                    ? null : comprobanteTextBox.Text.Trim();
                ingreso.Observacion = string.IsNullOrWhiteSpace(observacionTextBox.Text)
                    ? null : observacionTextBox.Text.Trim();

                if (mode == FormMode.Update)
                    await IngresoProductoApiClient.UpdateAsync(ingreso);
                else
                    await IngresoProductoApiClient.AddAsync(ingreso);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (UnauthorizedAccessException)
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar el ingreso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidarIngreso()
        {
            errorProvider.SetError(fechaDateTimePicker, string.Empty);
            errorProvider.SetError(productoComboBox, string.Empty);
            errorProvider.SetError(sucursalComboBox, string.Empty);
            errorProvider.SetError(proveedorTextBox, string.Empty);

            var esValido = true;

            if (fechaDateTimePicker.Value > DateTime.Now)
            {
                errorProvider.SetError(fechaDateTimePicker, "La fecha del ingreso no puede ser futura");
                esValido = false;
            }

            if (ProductoSeleccionado == null)
            {
                errorProvider.SetError(productoComboBox, "Seleccione el producto que ingresa");
                esValido = false;
            }

            if (sucursalComboBox.SelectedValue == null)
            {
                errorProvider.SetError(sucursalComboBox, "Seleccione la sucursal que recibe la mercadería");
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(proveedorTextBox.Text))
            {
                errorProvider.SetError(proveedorTextBox, "El proveedor es requerido");
                esValido = false;
            }

            return esValido;
        }

        private void DeshabilitarControles()
        {
            fechaDateTimePicker.Enabled = false;
            productoComboBox.Enabled = false;
            sucursalComboBox.Enabled = false;
            cantidadNumericUpDown.Enabled = false;
            proveedorTextBox.Enabled = false;
            comprobanteTextBox.Enabled = false;
            observacionTextBox.Enabled = false;
            aceptarButton.Enabled = false;
            cancelarButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void HabilitarControles()
        {
            fechaDateTimePicker.Enabled = true;
            productoComboBox.Enabled = true;
            sucursalComboBox.Enabled = true;
            cantidadNumericUpDown.Enabled = true;
            proveedorTextBox.Enabled = true;
            comprobanteTextBox.Enabled = true;
            observacionTextBox.Enabled = true;
            aceptarButton.Enabled = true;
            cancelarButton.Enabled = true;
            Cursor = Cursors.Default;
        }
    }
}
