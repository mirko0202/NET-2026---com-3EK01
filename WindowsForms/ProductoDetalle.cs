using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class ProductoDetalle : Form
    {
        private readonly ProductoDTO producto;
        private readonly FormMode mode;
        private readonly List<CategoriaDTO> categorias;

        public ProductoDetalle(FormMode mode, ProductoDTO producto, IEnumerable<CategoriaDTO> categorias)
        {
            InitializeComponent();

            this.mode = mode;
            this.producto = producto;
            this.categorias = categorias.ToList();
        }

        private void ProductoDetalle_Load(object sender, EventArgs e)
        {
            categoriaComboBox.DataSource = categorias;
            categoriaComboBox.DisplayMember = nameof(CategoriaDTO.Nombre);
            categoriaComboBox.ValueMember = nameof(CategoriaDTO.Id);

            AplicarModo();
            MostrarProducto();
        }

        private void AplicarModo()
        {
            var esAlta = mode == FormMode.Add;

            Text = esAlta ? "Nuevo producto" : "Modificar producto";

            idLabel.Visible = !esAlta;
            idTextBox.Visible = !esAlta;
        }

        private void MostrarProducto()
        {
            idTextBox.Text = producto.Id.ToString();
            nombreTextBox.Text = producto.Nombre;
            descripcionTextBox.Text = producto.Descripcion ?? string.Empty;
            unidadComboBox.Text = producto.Unidad ?? string.Empty;
            precioNumericUpDown.Value = Math.Clamp(producto.Precio, 0, precioNumericUpDown.Maximum);
            stockNumericUpDown.Value = Math.Clamp(producto.Stock, 0, (int)stockNumericUpDown.Maximum);
            stockMinNumericUpDown.Value = Math.Clamp(producto.StockMin, 0, (int)stockMinNumericUpDown.Maximum);

            if (producto.Categoria > 0)
                categoriaComboBox.SelectedValue = producto.Categoria;
            else
                categoriaComboBox.SelectedIndex = -1;
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidarProducto())
                return;

            try
            {
                DeshabilitarControles();

                producto.Nombre = nombreTextBox.Text.Trim();
                producto.Descripcion = string.IsNullOrWhiteSpace(descripcionTextBox.Text)
                    ? null : descripcionTextBox.Text.Trim();
                producto.Unidad = string.IsNullOrWhiteSpace(unidadComboBox.Text)
                    ? null : unidadComboBox.Text.Trim();
                producto.Categoria = (int)categoriaComboBox.SelectedValue!;
                producto.Precio = precioNumericUpDown.Value;
                producto.Stock = (int)stockNumericUpDown.Value;
                producto.StockMin = (int)stockMinNumericUpDown.Value;

                if (mode == FormMode.Update)
                    await ProductoApiClient.UpdateAsync(producto);
                else
                    await ProductoApiClient.AddAsync(producto);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (UnauthorizedAccessException)
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar el producto",
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

        private bool ValidarProducto()
        {
            errorProvider.SetError(nombreTextBox, string.Empty);
            errorProvider.SetError(categoriaComboBox, string.Empty);
            errorProvider.SetError(descripcionTextBox, string.Empty);
            errorProvider.SetError(unidadComboBox, string.Empty);
            errorProvider.SetError(precioNumericUpDown, string.Empty);

            var esValido = true;

            if (string.IsNullOrWhiteSpace(nombreTextBox.Text))
            {
                errorProvider.SetError(nombreTextBox, "El nombre es requerido");
                esValido = false;
            }
            else if (nombreTextBox.Text.Trim().Length > 100)
            {
                errorProvider.SetError(nombreTextBox, "El nombre no puede superar los 100 caracteres");
                esValido = false;
            }

            if (categoriaComboBox.SelectedValue == null)
            {
                errorProvider.SetError(categoriaComboBox, "Seleccione la categoría del producto");
                esValido = false;
            }

            if (precioNumericUpDown.Value <= 0)
            {
                errorProvider.SetError(precioNumericUpDown, "El precio debe ser mayor a 0");
                esValido = false;
            }

            if (descripcionTextBox.Text.Length > 255)
            {
                errorProvider.SetError(descripcionTextBox, "La descripción no puede superar los 255 caracteres");
                esValido = false;
            }

            if (unidadComboBox.Text.Trim().Length > 20)
            {
                errorProvider.SetError(unidadComboBox, "La unidad no puede superar los 20 caracteres");
                esValido = false;
            }

            return esValido;
        }

        private void DeshabilitarControles()
        {
            nombreTextBox.Enabled = false;
            categoriaComboBox.Enabled = false;
            descripcionTextBox.Enabled = false;
            unidadComboBox.Enabled = false;
            precioNumericUpDown.Enabled = false;
            stockNumericUpDown.Enabled = false;
            stockMinNumericUpDown.Enabled = false;
            aceptarButton.Enabled = false;
            cancelarButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void HabilitarControles()
        {
            nombreTextBox.Enabled = true;
            categoriaComboBox.Enabled = true;
            descripcionTextBox.Enabled = true;
            unidadComboBox.Enabled = true;
            precioNumericUpDown.Enabled = true;
            stockNumericUpDown.Enabled = true;
            stockMinNumericUpDown.Enabled = true;
            aceptarButton.Enabled = true;
            cancelarButton.Enabled = true;
            Cursor = Cursors.Default;
        }
    }
}
