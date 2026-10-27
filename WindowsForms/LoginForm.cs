using API.Clients;

namespace WindowsForms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void ingresarButton_Click(object sender, EventArgs e)
        {
            if (!ValidarEntrada())
                return;

            try
            {
                DeshabilitarControles();

                var login = await AuthServiceProvider.Instance.LoginAsync(
                    usuarioTextBox.Text.Trim(), passwordTextBox.Text);

                if (login.Exito)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(login.Mensaje, "Error de autenticación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    passwordTextBox.Clear();
                    passwordTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool ValidarEntrada()
        {
            errorProvider.SetError(usuarioTextBox, string.Empty);
            errorProvider.SetError(passwordTextBox, string.Empty);

            var esValido = true;

            if (string.IsNullOrWhiteSpace(usuarioTextBox.Text))
            {
                errorProvider.SetError(usuarioTextBox, "El usuario es requerido");
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                errorProvider.SetError(passwordTextBox, "La contraseña es requerida");
                esValido = false;
            }

            return esValido;
        }

        private void DeshabilitarControles()
        {
            usuarioTextBox.Enabled = false;
            passwordTextBox.Enabled = false;
            ingresarButton.Enabled = false;
            cancelarButton.Enabled = false;
            ingresarButton.Text = "Ingresando...";
            Cursor = Cursors.WaitCursor;
        }

        private void HabilitarControles()
        {
            usuarioTextBox.Enabled = true;
            passwordTextBox.Enabled = true;
            ingresarButton.Enabled = true;
            cancelarButton.Enabled = true;
            ingresarButton.Text = "Iniciar sesión";
            Cursor = Cursors.Default;
        }
    }
}
