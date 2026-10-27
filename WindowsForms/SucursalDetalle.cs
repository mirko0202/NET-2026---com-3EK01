using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class SucursalDetalle : Form
    {
        private readonly SucursalDTO sucursal;
        private readonly FormMode mode;

        public SucursalDetalle(FormMode mode, SucursalDTO sucursal)
        {
            InitializeComponent();

            this.mode = mode;
            this.sucursal = sucursal;

            AplicarModo();
            MostrarSucursal();
        }

        private void AplicarModo()
        {
            var esAlta = mode == FormMode.Add;

            Text = esAlta ? "Nueva sucursal" : "Modificar sucursal";

            idLabel.Visible = !esAlta;
            idTextBox.Visible = !esAlta;
        }

        private void MostrarSucursal()
        {
            idTextBox.Text = sucursal.Id.ToString();
            nombreTextBox.Text = sucursal.Nombre;
            direccionTextBox.Text = sucursal.Direccion ?? string.Empty;
            telefonoTextBox.Text = sucursal.Telefono ?? string.Empty;
            activaCheckBox.Checked = sucursal.Activa;
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidarSucursal())
                return;

            if (mode == FormMode.Update && sucursal.Activa && !activaCheckBox.Checked)
            {
                var confirmar = MessageBox.Show(
                    "Una sucursal inactiva no puede registrar ventas ni ingresos de mercadería. ¿Desea desactivarla?",
                    "Desactivar sucursal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmar != DialogResult.Yes)
                    return;
            }

            try
            {
                DeshabilitarControles();

                sucursal.Nombre = nombreTextBox.Text.Trim();
                sucursal.Direccion = string.IsNullOrWhiteSpace(direccionTextBox.Text)
                    ? null : direccionTextBox.Text.Trim();
                sucursal.Telefono = string.IsNullOrWhiteSpace(telefonoTextBox.Text)
                    ? null : telefonoTextBox.Text.Trim();
                sucursal.Activa = activaCheckBox.Checked;

                if (mode == FormMode.Update)
                    await SucursalApiClient.UpdateAsync(sucursal);
                else
                    await SucursalApiClient.AddAsync(sucursal);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (UnauthorizedAccessException)
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar la sucursal",
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

        private bool ValidarSucursal()
        {
            errorProvider.SetError(nombreTextBox, string.Empty);
            errorProvider.SetError(direccionTextBox, string.Empty);
            errorProvider.SetError(telefonoTextBox, string.Empty);

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

            if (direccionTextBox.Text.Trim().Length > 150)
            {
                errorProvider.SetError(direccionTextBox, "La dirección no puede superar los 150 caracteres");
                esValido = false;
            }

            var telefono = telefonoTextBox.Text.Trim();
            if (telefono.Length > 30)
            {
                errorProvider.SetError(telefonoTextBox, "El teléfono no puede superar los 30 caracteres");
                esValido = false;
            }
            else if (telefono.Any(c => !char.IsDigit(c) && " -+()".IndexOf(c) < 0))
            {
                errorProvider.SetError(telefonoTextBox, "El teléfono solo puede contener números, espacios, guiones, + y paréntesis");
                esValido = false;
            }

            return esValido;
        }

        private void DeshabilitarControles()
        {
            nombreTextBox.Enabled = false;
            direccionTextBox.Enabled = false;
            telefonoTextBox.Enabled = false;
            activaCheckBox.Enabled = false;
            aceptarButton.Enabled = false;
            cancelarButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void HabilitarControles()
        {
            nombreTextBox.Enabled = true;
            direccionTextBox.Enabled = true;
            telefonoTextBox.Enabled = true;
            activaCheckBox.Enabled = true;
            aceptarButton.Enabled = true;
            cancelarButton.Enabled = true;
            Cursor = Cursors.Default;
        }
    }
}
