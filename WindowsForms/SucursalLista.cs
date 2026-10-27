using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class SucursalLista : Form
    {
        private bool esAdministrador;

        public SucursalLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            sucursalesDataGridView.AutoGenerateColumns = false;

            sucursalesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 50
            });

            sucursalesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 190
            });

            sucursalesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Direccion",
                HeaderText = "Dirección",
                DataPropertyName = "Direccion",
                Width = 260
            });

            sucursalesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Telefono",
                HeaderText = "Teléfono",
                DataPropertyName = "Telefono",
                Width = 140
            });

            sucursalesDataGridView.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Activa",
                HeaderText = "Activa",
                DataPropertyName = "Activa",
                Width = 70
            });
        }

        private async void SucursalLista_Load(object sender, EventArgs e)
        {
            esAdministrador = await AuthServiceProvider.Instance.EsAdministradorAsync();
            AplicarAutorizacion();
            await CargarAsync();
        }

        private void AplicarAutorizacion()
        {
            agregarButton.Visible = esAdministrador;
            actualizarButton.Visible = esAdministrador;
            eliminarButton.Visible = esAdministrador;

            Text = esAdministrador ? "Sucursales" : "Sucursales (solo consulta)";
        }

        private async Task CargarAsync()
        {
            try
            {
                DeshabilitarControles();
                sucursalesDataGridView.DataSource = null;

                var lista = (await SucursalApiClient.GetAllAsync()).ToList();
                sucursalesDataGridView.DataSource = lista;

                ResaltarInactivas();

                var activas = lista.Count(s => s.Activa);
                resultadosLabel.Text = $"{lista.Count} sucursales  ·  {activas} activas";
            }
            catch (UnauthorizedAccessException)
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar sucursales",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void ResaltarInactivas()
        {
            foreach (DataGridViewRow fila in sucursalesDataGridView.Rows)
            {
                if (fila.DataBoundItem is SucursalDTO sucursal && !sucursal.Activa)
                    fila.DefaultCellStyle.ForeColor = SystemColors.GrayText;
            }
        }

        private SucursalDTO? ObtenerSeleccionada()
        {
            if (sucursalesDataGridView.SelectedRows.Count == 0)
                return null;

            return sucursalesDataGridView.SelectedRows[0].DataBoundItem as SucursalDTO;
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            using var detalle = new SucursalDetalle(FormMode.Add, new SucursalDTO { Activa = true });
            if (detalle.ShowDialog() == DialogResult.OK)
                await CargarAsync();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            var seleccionada = ObtenerSeleccionada();
            if (seleccionada == null)
            {
                MessageBox.Show("Seleccione una sucursal de la lista.", "Modificar sucursal",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DeshabilitarControles();

                var sucursal = await SucursalApiClient.GetAsync(seleccionada.Id);

                using var detalle = new SucursalDetalle(FormMode.Update, sucursal);
                detalle.ShowDialog();
            }
            catch (UnauthorizedAccessException)
            {
                Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al abrir la sucursal",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }

            await CargarAsync();
        }

        private void sucursalesDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && esAdministrador)
                actualizarButton_Click(sender, e);
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            var sucursal = ObtenerSeleccionada();
            if (sucursal == null)
            {
                MessageBox.Show("Seleccione una sucursal de la lista.", "Eliminar sucursal",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmar = MessageBox.Show(
                $"¿Está seguro que desea eliminar la sucursal \"{sucursal.Nombre}\"?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
                return;

            try
            {
                DeshabilitarControles();
                await SucursalApiClient.DeleteAsync(sucursal.Id);
            }
            catch (UnauthorizedAccessException)
            {
                Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo eliminar la sucursal",
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
            sucursalesDataGridView.Enabled = false;
            agregarButton.Enabled = false;
            actualizarButton.Enabled = false;
            eliminarButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void HabilitarControles()
        {
            sucursalesDataGridView.Enabled = true;

            var hayFilas = sucursalesDataGridView.Rows.Count > 0;

            agregarButton.Enabled = esAdministrador;
            actualizarButton.Enabled = esAdministrador && hayFilas;
            eliminarButton.Enabled = esAdministrador && hayFilas;

            Cursor = Cursors.Default;
        }
    }
}
