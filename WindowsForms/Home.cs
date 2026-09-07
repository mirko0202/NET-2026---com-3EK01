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

        private void Home_Load(object sender, EventArgs e)
        {
            var usuario = SesionActual.Usuario;
            if (usuario == null)
            {
                Close();
                return;
            }

            bienvenidaLabel.Text = $"Bienvenido, {usuario.NombreCompleto}";
            usuarioStatusLabel.Text = $"Usuario: {usuario.Username}  |  Rol: {usuario.Rol}";

            AplicarAutorizacion();
        }

        private void AplicarAutorizacion()
        {
            var esAdmin = SesionActual.EsAdministrador;

            permisosLabel.Text = esAdmin
                ? "Perfil Administrador: puede consultar y modificar productos e ingredientes."
                : "Perfil Empleado: solo puede consultar el stock de productos e ingredientes.";
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var productos = new ProductoLista();
            productos.ShowDialog();
        }

        private void ingredientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var ingredientes = new IngredienteLista();
            ingredientes.ShowDialog();
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
