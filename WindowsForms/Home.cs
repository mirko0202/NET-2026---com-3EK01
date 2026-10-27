using API.Clients;

namespace WindowsForms
{
    public partial class Home : Form
    {
        public bool CerroSesion { get; private set; }

        public Home()
        {
            InitializeComponent();
        }

        private async void Home_Load(object sender, EventArgs e)
        {
            var usuario = await AuthServiceProvider.Instance.GetUsuarioAsync();
            if (usuario == null)
            {
                Close();
                return;
            }

            bienvenidaLabel.Text = $"Bienvenido, {usuario.NombreCompleto}";
            usuarioStatusLabel.Text = $"Usuario: {usuario.Username}  |  Rol: {usuario.Rol}";

            AplicarAutorizacion(usuario.EsAdministrador);
        }

        private void AplicarAutorizacion(bool esAdministrador)
        {
            permisosLabel.Text = esAdministrador
                ? "Perfil Administrador: gestiona productos, ingredientes y ventas."
                : "Perfil Empleado: consulta el stock y registra ventas.";
        }

        private async void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var productos = new ProductoLista())
                productos.ShowDialog();

            await VerificarSesionAsync();
        }

        private async void ingredientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ingredientes = new IngredienteLista())
                ingredientes.ShowDialog();

            await VerificarSesionAsync();
        }

        private async void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ventas = new VentaLista())
                ventas.ShowDialog();

            await VerificarSesionAsync();
        }

        private async Task VerificarSesionAsync()
        {
            if (await AuthServiceProvider.Instance.IsAuthenticatedAsync())
                return;

            MessageBox.Show(BaseApiClient.MensajeSesionExpirada, "Sesión expirada",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

            CerroSesion = true;
            Close();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmar = MessageBox.Show("¿Desea cerrar la sesión actual?", "Cerrar sesión",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmar == DialogResult.Yes)
            {
                CerroSesion = true;
                Close();
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerroSesion = false;
            Close();
        }
    }
}
