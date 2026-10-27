using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class IngredienteDetalle : Form
    {
        private readonly IngredienteDTO ingrediente;
        private readonly FormMode mode;

        public IngredienteDetalle(FormMode mode, IngredienteDTO ingrediente)
        {
            InitializeComponent();

            this.mode = mode;
            this.ingrediente = ingrediente;

            AplicarModo();
            MostrarIngrediente();
        }

        private void AplicarModo()
        {
            var esAlta = mode == FormMode.Add;

            Text = esAlta ? "Nuevo ingrediente" : "Modificar ingrediente";

            idLabel.Visible = !esAlta;
            idTextBox.Visible = !esAlta;
        }

        private void MostrarIngrediente()
        {
            idTextBox.Text = ingrediente.Id.ToString();
            nombreTextBox.Text = ingrediente.Nombre;
            descripcionTextBox.Text = ingrediente.Descripcion ?? string.Empty;
            unidadComboBox.Text = ingrediente.Unidad ?? string.Empty;
            stockNumericUpDown.Value = Math.Clamp(ingrediente.Stock, 0, (int)stockNumericUpDown.Maximum);
            stockMinNumericUpDown.Value = Math.Clamp(ingrediente.StockMin, 0, (int)stockMinNumericUpDown.Maximum);
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidarIngrediente())
                return;

            try
            {
                DeshabilitarControles();

                ingrediente.Nombre = nombreTextBox.Text.Trim();
                ingrediente.Descripcion = string.IsNullOrWhiteSpace(descripcionTextBox.Text)
                    ? null : descripcionTextBox.Text.Trim();
                ingrediente.Unidad = string.IsNullOrWhiteSpace(unidadComboBox.Text)
                    ? null : unidadComboBox.Text.Trim();
                ingrediente.Stock = (int)stockNumericUpDown.Value;
                ingrediente.StockMin = (int)stockMinNumericUpDown.Value;

                if (mode == FormMode.Update)
                    await IngredienteApiClient.UpdateAsync(ingrediente);
                else
                    await IngredienteApiClient.AddAsync(ingrediente);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (UnauthorizedAccessException)
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar el ingrediente",
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

        private bool ValidarIngrediente()
        {
            errorProvider.SetError(nombreTextBox, string.Empty);
            errorProvider.SetError(descripcionTextBox, string.Empty);
            errorProvider.SetError(unidadComboBox, string.Empty);

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
            descripcionTextBox.Enabled = false;
            unidadComboBox.Enabled = false;
            stockNumericUpDown.Enabled = false;
            stockMinNumericUpDown.Enabled = false;
            aceptarButton.Enabled = false;
            cancelarButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void HabilitarControles()
        {
            nombreTextBox.Enabled = true;
            descripcionTextBox.Enabled = true;
            unidadComboBox.Enabled = true;
            stockNumericUpDown.Enabled = true;
            stockMinNumericUpDown.Enabled = true;
            aceptarButton.Enabled = true;
            cancelarButton.Enabled = true;
            Cursor = Cursors.Default;
        }
    }
}
