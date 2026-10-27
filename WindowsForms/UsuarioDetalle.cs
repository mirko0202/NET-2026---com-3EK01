using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class UsuarioDetalle : Form
    {
        private readonly UsuarioDTO usuario;
        private readonly FormMode mode;
        private readonly bool esUnoMismo;

        public UsuarioDetalle(FormMode mode, UsuarioDTO usuario, int usuarioActualId)
        {
            InitializeComponent();

            this.mode = mode;
            this.usuario = usuario;
            esUnoMismo = mode == FormMode.Update && usuario.Id == usuarioActualId;
        }

        private async void UsuarioDetalle_Load(object sender, EventArgs e)
        {
            try
            {
                DeshabilitarControles();
                AplicarModo();
                await CargarSucursalesAsync();
                MostrarUsuario();
            }
            catch (UnauthorizedAccessException)
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar los datos del usuario",
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

            Text = esAlta ? "Nuevo usuario" : $"Modificar usuario \"{usuario.Username}\"";

            idLabel.Visible = !esAlta;
            idTextBox.Visible = !esAlta;

            passwordGroupBox.Text = esAlta ? "Contraseña" : "Cambiar contraseña (opcional)";
            passwordAyudaLabel.Text = esAlta
                ? "Mínimo 6 caracteres."
                : "Dejar en blanco para mantener la contraseña actual.";
        }

        private async Task CargarSucursalesAsync()
        {
            var opciones = new List<SucursalDTO> { new() { Id = 0, Nombre = "(Sin sucursal asignada)" } };
            opciones.AddRange((await SucursalApiClient.GetAllAsync())
                .Where(s => s.Activa || s.Id == usuario.SucursalId));

            sucursalComboBox.DataSource = opciones;
            sucursalComboBox.DisplayMember = nameof(SucursalDTO.Nombre);
            sucursalComboBox.ValueMember = nameof(SucursalDTO.Id);
        }

        private void MostrarUsuario()
        {
            idTextBox.Text = usuario.Id.ToString();
            usernameTextBox.Text = usuario.Username;
            nombreCompletoTextBox.Text = usuario.NombreCompleto;
            rolComboBox.SelectedItem = usuario.EsAdministrador ? "Administrador" : usuario.Rol;
            sucursalComboBox.SelectedValue = usuario.SucursalId ?? 0;
            activoCheckBox.Checked = usuario.Activo;
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (!ValidarUsuario())
                return;

            try
            {
                DeshabilitarControles();

                var sucursalId = (int)sucursalComboBox.SelectedValue!;

                usuario.Username = usernameTextBox.Text.Trim();
                usuario.NombreCompleto = nombreCompletoTextBox.Text.Trim();
                usuario.Rol = (string)rolComboBox.SelectedItem!;
                usuario.SucursalId = sucursalId == 0 ? null : sucursalId;
                usuario.Activo = activoCheckBox.Checked;
                usuario.Password = string.IsNullOrEmpty(passwordTextBox.Text) ? null : passwordTextBox.Text;

                if (mode == FormMode.Update)
                    await UsuarioApiClient.UpdateAsync(usuario);
                else
                    await UsuarioApiClient.AddAsync(usuario);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (UnauthorizedAccessException)
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar el usuario",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                usuario.Password = null;
                HabilitarControles();
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidarUsuario()
        {
            errorProvider.SetError(usernameTextBox, string.Empty);
            errorProvider.SetError(nombreCompletoTextBox, string.Empty);
            errorProvider.SetError(rolComboBox, string.Empty);
            errorProvider.SetError(activoCheckBox, string.Empty);
            errorProvider.SetError(passwordTextBox, string.Empty);
            errorProvider.SetError(confirmarTextBox, string.Empty);

            var esValido = true;
            var username = usernameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                errorProvider.SetError(usernameTextBox, "El nombre de usuario es requerido");
                esValido = false;
            }
            else if (username.Length < 3)
            {
                errorProvider.SetError(usernameTextBox, "El nombre de usuario debe tener al menos 3 caracteres");
                esValido = false;
            }
            else if (username.Any(char.IsWhiteSpace))
            {
                errorProvider.SetError(usernameTextBox, "El nombre de usuario no puede contener espacios");
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(nombreCompletoTextBox.Text))
            {
                errorProvider.SetError(nombreCompletoTextBox, "El nombre completo es requerido");
                esValido = false;
            }

            if (rolComboBox.SelectedItem == null)
            {
                errorProvider.SetError(rolComboBox, "Seleccione el rol del usuario");
                esValido = false;
            }
            else if (esUnoMismo && usuario.EsAdministrador && (string)rolComboBox.SelectedItem != "Administrador")
            {
                errorProvider.SetError(rolComboBox, "No puede quitarse a sí mismo el rol de administrador");
                esValido = false;
            }

            if (esUnoMismo && !activoCheckBox.Checked)
            {
                errorProvider.SetError(activoCheckBox, "No puede desactivar su propio usuario");
                esValido = false;
            }

            var password = passwordTextBox.Text;
            var requierePassword = mode == FormMode.Add;

            if (requierePassword && string.IsNullOrEmpty(password))
            {
                errorProvider.SetError(passwordTextBox, "La contraseña es requerida para un usuario nuevo");
                esValido = false;
            }
            else if (password.Length > 0 && password.Length < 6)
            {
                errorProvider.SetError(passwordTextBox, "La contraseña debe tener al menos 6 caracteres");
                esValido = false;
            }
            else if (password != confirmarTextBox.Text)
            {
                errorProvider.SetError(confirmarTextBox, "Las contraseñas no coinciden");
                esValido = false;
            }

            return esValido;
        }

        private void DeshabilitarControles()
        {
            usernameTextBox.Enabled = false;
            nombreCompletoTextBox.Enabled = false;
            rolComboBox.Enabled = false;
            sucursalComboBox.Enabled = false;
            activoCheckBox.Enabled = false;
            passwordGroupBox.Enabled = false;
            aceptarButton.Enabled = false;
            cancelarButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void HabilitarControles()
        {
            usernameTextBox.Enabled = true;
            nombreCompletoTextBox.Enabled = true;
            rolComboBox.Enabled = true;
            sucursalComboBox.Enabled = true;
            activoCheckBox.Enabled = true;
            passwordGroupBox.Enabled = true;
            aceptarButton.Enabled = true;
            cancelarButton.Enabled = true;
            Cursor = Cursors.Default;
        }
    }
}
