using DTOs;
using System.Globalization;

namespace WindowsForms
{
    public partial class DetalleVentaItem : Form
    {
        private readonly FormMode mode;
        private readonly List<ProductoDisponible> opciones;

        public DetalleVentaDTO Item { get; }

        public DetalleVentaItem(FormMode mode, DetalleVentaDTO item, List<ProductoDisponible> opciones)
        {
            InitializeComponent();

            cantidadNumericUpDown.TextChanged += valores_ValueChanged;
            precioNumericUpDown.TextChanged += valores_ValueChanged;

            this.mode = mode;
            this.opciones = opciones;
            Item = item;
        }

        private ProductoDisponible? ProductoSeleccionado => productoComboBox.SelectedItem as ProductoDisponible;

        private void DetalleVentaItem_Load(object sender, EventArgs e)
        {
            var esAlta = mode == FormMode.Add;

            Text = esAlta ? "Agregar producto a la venta" : "Modificar producto de la venta";

            productoComboBox.DataSource = opciones;
            productoComboBox.DisplayMember = nameof(ProductoDisponible.Nombre);
            productoComboBox.ValueMember = nameof(ProductoDisponible.Id);
            productoComboBox.Enabled = esAlta;

            if (esAlta)
                productoComboBox.SelectedIndex = -1;
            else
                productoComboBox.SelectedValue = Item.ProductoId;

            cantidadNumericUpDown.Value = Math.Clamp(Item.Cantidad, (int)cantidadNumericUpDown.Minimum,
                                                     (int)cantidadNumericUpDown.Maximum);
            precioNumericUpDown.Value = Math.Clamp(Item.PrecioUnitario, precioNumericUpDown.Minimum,
                                                   precioNumericUpDown.Maximum);

            ActualizarResumen();
        }

        private void productoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mode == FormMode.Add && ProductoSeleccionado is { Precio: > 0 } producto)
            {
                precioNumericUpDown.Value = Math.Clamp(producto.Precio, precioNumericUpDown.Minimum,
                                                       precioNumericUpDown.Maximum);
            }

            ActualizarResumen();
        }

        private void valores_ValueChanged(object? sender, EventArgs e)
        {
            ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            var producto = ProductoSeleccionado;

            stockValorLabel.Text = producto == null
                ? "-"
                : $"{producto.Disponible} {producto.Unidad}".Trim();

            var subtotal = ValorIngresado(cantidadNumericUpDown) * ValorIngresado(precioNumericUpDown);
            subtotalValorLabel.Text = subtotal.ToString("C2");
        }

        private static decimal ValorIngresado(NumericUpDown control) =>
            decimal.TryParse(control.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var valor)
                ? valor
                : control.Value;

        private void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidarItem())
                return;

            var producto = ProductoSeleccionado!;

            Item.ProductoId = producto.Id;
            Item.ProductoNombre = producto.Nombre;
            Item.Cantidad = (int)cantidadNumericUpDown.Value;
            Item.PrecioUnitario = precioNumericUpDown.Value;

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidarItem()
        {
            errorProvider.SetError(productoComboBox, string.Empty);
            errorProvider.SetError(cantidadNumericUpDown, string.Empty);
            errorProvider.SetError(precioNumericUpDown, string.Empty);

            var esValido = true;
            var producto = ProductoSeleccionado;

            if (producto == null)
            {
                errorProvider.SetError(productoComboBox, "Seleccione un producto");
                esValido = false;
            }
            else if (cantidadNumericUpDown.Value > producto.Disponible)
            {
                errorProvider.SetError(cantidadNumericUpDown,
                    $"La cantidad supera el stock disponible ({producto.Disponible})");
                esValido = false;
            }

            if (precioNumericUpDown.Value <= 0)
            {
                errorProvider.SetError(precioNumericUpDown, "El precio unitario debe ser mayor a 0");
                esValido = false;
            }

            return esValido;
        }
    }
}
