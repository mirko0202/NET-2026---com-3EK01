using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class IngredienteLista : Form
    {
        private bool esAdministrador;

        public IngredienteLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            ingredientesDataGridView.AutoGenerateColumns = false;

            ingredientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 50
            });

            ingredientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 200
            });

            ingredientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion",
                Width = 260
            });

            ingredientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Unidad",
                HeaderText = "Unidad",
                DataPropertyName = "Unidad",
                Width = 90
            });

            ingredientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                HeaderText = "Stock",
                DataPropertyName = "Stock",
                Width = 70
            });

            ingredientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StockMin",
                HeaderText = "Stock mín.",
                DataPropertyName = "StockMin",
                Width = 80
            });

            ingredientesDataGridView.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "BajoStock",
                HeaderText = "Bajo stock",
                DataPropertyName = "BajoStock",
                Width = 90
            });
        }

        private async void IngredienteLista_Load(object sender, EventArgs e)
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

            Text = esAdministrador ? "Ingredientes" : "Ingredientes (solo consulta)";
        }

        private async Task CargarAsync()
        {
            try
            {
                DeshabilitarControles();
                ingredientesDataGridView.DataSource = null;

                var lista = (await IngredienteApiClient.GetAllAsync()).ToList();
                ingredientesDataGridView.DataSource = lista;

                ResaltarBajoStock();

                resultadosLabel.Text = lista.Count == 1
                    ? "1 ingrediente"
                    : $"{lista.Count} ingredientes";
            }
            catch (UnauthorizedAccessException)
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar ingredientes",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void ResaltarBajoStock()
        {
            foreach (DataGridViewRow fila in ingredientesDataGridView.Rows)
            {
                if (fila.DataBoundItem is IngredienteDTO ingrediente && ingrediente.BajoStock)
                {
                    fila.DefaultCellStyle.BackColor = Color.MistyRose;
                    fila.DefaultCellStyle.SelectionBackColor = Color.IndianRed;
                }
            }
        }

        private IngredienteDTO? ObtenerSeleccionado()
        {
            if (ingredientesDataGridView.SelectedRows.Count == 0)
                return null;

            return ingredientesDataGridView.SelectedRows[0].DataBoundItem as IngredienteDTO;
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            using var detalle = new IngredienteDetalle(FormMode.Add, new IngredienteDTO());
            detalle.ShowDialog();

            await CargarAsync();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un ingrediente de la lista.", "Modificar ingrediente",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DeshabilitarControles();

                var ingrediente = await IngredienteApiClient.GetAsync(seleccionado.Id);

                using var detalle = new IngredienteDetalle(FormMode.Update, ingrediente);
                detalle.ShowDialog();
            }
            catch (UnauthorizedAccessException)
            {
                Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al abrir el ingrediente",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }

            await CargarAsync();
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            var ingrediente = ObtenerSeleccionado();
            if (ingrediente == null)
            {
                MessageBox.Show("Seleccione un ingrediente de la lista.", "Eliminar ingrediente",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmar = MessageBox.Show(
                $"¿Está seguro que desea eliminar el ingrediente \"{ingrediente.Nombre}\"?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
                return;

            try
            {
                DeshabilitarControles();
                await IngredienteApiClient.DeleteAsync(ingrediente.Id);
            }
            catch (UnauthorizedAccessException)
            {
                Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al eliminar el ingrediente",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ingredientesDataGridView.Enabled = false;
            agregarButton.Enabled = false;
            actualizarButton.Enabled = false;
            eliminarButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void HabilitarControles()
        {
            ingredientesDataGridView.Enabled = true;

            var hayFilas = ingredientesDataGridView.Rows.Count > 0;

            agregarButton.Enabled = esAdministrador;
            actualizarButton.Enabled = esAdministrador && hayFilas;
            eliminarButton.Enabled = esAdministrador && hayFilas;

            Cursor = Cursors.Default;
        }
    }
}
