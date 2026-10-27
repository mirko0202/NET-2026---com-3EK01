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
            usuarioStatusLabel.Text = $"Usuario: {usuario.Username}  |  Rol: {usuario.Rol}  |  " +
                                      $"Sucursal: {usuario.SucursalNombre ?? "todas"}";

            AplicarAutorizacion(usuario.EsAdministrador);
        }

        private void AplicarAutorizacion(bool esAdministrador)
        {
            administracionToolStripMenuItem.Visible = esAdministrador;
            reportesToolStripMenuItem.Visible = esAdministrador;

            permisosLabel.Text = esAdministrador
                ? "Perfil Administrador: gestiona productos, ingredientes, ventas, ingresos de mercadería, " +
                  "sucursales y usuarios, y consulta los reportes."
                : "Perfil Empleado: consulta el stock y el historial de ingresos, y registra ventas.";
        }

        private async void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await AbrirAsync(new ProductoLista());
        }

        private async void ingredientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await AbrirAsync(new IngredienteLista());
        }

        private async void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await AbrirAsync(new VentaLista());
        }

        private async void ingresosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await AbrirAsync(new IngresoLista());
        }

        private async void sucursalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await AbrirAsync(new SucursalLista());
        }

        private async void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await AbrirAsync(new UsuarioLista());
        }

        private async void reporteVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await AbrirAsync(new ReporteVentas());
        }

        private async void reporteBajoStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await AbrirAsync(new ReporteBajoStock());
        }

        private async Task AbrirAsync(Form formulario)
        {
            using (formulario)
                formulario.ShowDialog();

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
