using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class UsuarioLista : Form
    {
        private int usuarioActualId;

        public UsuarioLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            usuariosDataGridView.AutoGenerateColumns = false;

            usuariosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 50
            });

            usuariosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Username",
                HeaderText = "Usuario",
                DataPropertyName = "Username",
                Width = 110
            });

            usuariosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreCompleto",
                HeaderText = "Nombre completo",
                DataPropertyName = "NombreCompleto",
                Width = 200
            });

            usuariosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Rol",
                HeaderText = "Rol",
                DataPropertyName = "Rol",
                Width = 110
            });

            usuariosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SucursalNombre",
                HeaderText = "Sucursal",
                DataPropertyName = "SucursalNombre",
                Width = 150
            });

            usuariosDataGridView.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Activo",
                HeaderText = "Activo",
                DataPropertyName = "Activo",
                Width = 70
            });
        }

        private async void UsuarioLista_Load(object sender, EventArgs e)
        {
            var usuario = await AuthServiceProvider.Instance.GetUsuarioAsync();
            usuarioActualId = usuario?.Id ?? 0;

            await CargarAsync();
        }

        private async Task CargarAsync()
        {
            try
            {
                DeshabilitarControles();
                usuariosDataGridView.DataSource = null;

                var lista = (await UsuarioApiClient.GetAllAsync()).ToList();
                usuariosDataGridView.DataSource = lista;

                ResaltarFilas();

                var administradores = lista.Count(u => u.EsAdministrador && u.Activo);
                resultadosLabel.Text = $"{lista.Count} usuarios  ·  {administradores} administradores activos";
            }
            catch (UnauthorizedAccessException)
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar usuarios",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void ResaltarFilas()
        {
            foreach (DataGridViewRow fila in usuariosDataGridView.Rows)
            {
                if (fila.DataBoundItem is not UsuarioDTO usuario)
                    continue;

                if (!usuario.Activo)
                    fila.DefaultCellStyle.ForeColor = SystemColors.GrayText;

                if (usuario.Id == usuarioActualId)
                    fila.DefaultCellStyle.Font = new Font(usuariosDataGridView.Font, FontStyle.Bold);
            }
        }

        private UsuarioDTO? ObtenerSeleccionado()
        {
            if (usuariosDataGridView.SelectedRows.Count == 0)
                return null;

            return usuariosDataGridView.SelectedRows[0].DataBoundItem as UsuarioDTO;
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            using var detalle = new UsuarioDetalle(FormMode.Add, new UsuarioDTO { Rol = "Empleado" }, usuarioActualId);
            if (detalle.ShowDialog() == DialogResult.OK)
                await CargarAsync();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un usuario de la lista.", "Modificar usuario",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DeshabilitarControles();

                var usuario = await UsuarioApiClient.GetAsync(seleccionado.Id);

                using var detalle = new UsuarioDetalle(FormMode.Update, usuario, usuarioActualId);
                detalle.ShowDialog();
            }
            catch (UnauthorizedAccessException)
            {
                Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al abrir el usuario",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }

            await CargarAsync();
        }

        private void usuariosDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                actualizarButton_Click(sender, e);
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            var usuario = ObtenerSeleccionado();
            if (usuario == null)
            {
                MessageBox.Show("Seleccione un usuario de la lista.", "Eliminar usuario",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (usuario.Id == usuarioActualId)
            {
                MessageBox.Show("No puede eliminar su propio usuario.", "Eliminar usuario",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmar = MessageBox.Show(
                $"¿Está seguro que desea eliminar al usuario \"{usuario.Username}\" ({usuario.NombreCompleto})?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
                return;

            try
            {
                DeshabilitarControles();
                await UsuarioApiClient.DeleteAsync(usuario.Id);
            }
            catch (UnauthorizedAccessException)
            {
                Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo eliminar el usuario",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                HabilitarControles();
            }

            await CargarAsync();
        }

        private void cerrarButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DeshabilitarControles()
        {
            usuariosDataGridView.Enabled = false;
            agregarButton.Enabled = false;
            actualizarButton.Enabled = false;
            eliminarButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void HabilitarControles()
        {
            usuariosDataGridView.Enabled = true;

            var hayFilas = usuariosDataGridView.Rows.Count > 0;

            agregarButton.Enabled = true;
            actualizarButton.Enabled = hayFilas;
            eliminarButton.Enabled = hayFilas;

            Cursor = Cursors.Default;
        }
    }
}
