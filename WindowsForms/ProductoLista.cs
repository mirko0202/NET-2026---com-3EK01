using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class ProductoLista : Form
    {
        public ProductoLista()
        {
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            productosDataGridView.AutoGenerateColumns = false;

            productosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 50
            });

            productosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 200
            });

            productosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion",
                Width = 260
            });

            productosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Unidad",
                HeaderText = "Unidad",
                DataPropertyName = "Unidad",
                Width = 90
            });

            productosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                HeaderText = "Stock",
                DataPropertyName = "Stock",
                Width = 70
            });

            productosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StockMin",
                HeaderText = "Stock mín.",
                DataPropertyName = "StockMin",
                Width = 80
            });

            productosDataGridView.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "BajoStock",
                HeaderText = "Bajo stock",
                DataPropertyName = "BajoStock",
                Width = 90
            });
        }

        private async void ProductoLista_Load(object sender, EventArgs e)
        {
            AplicarAutorizacion();
            await BuscarYCargarAsync();
        }

        private void AplicarAutorizacion()
        {
            var esAdmin = SesionActual.EsAdministrador;

            agregarButton.Visible = esAdmin;
            actualizarButton.Visible = esAdmin;
            eliminarButton.Visible = esAdmin;

            Text = esAdmin ? "Productos" : "Productos (solo consulta)";
        }

        private async Task BuscarYCargarAsync()
        {
            try
            {
                DeshabilitarControles();
                productosDataGridView.DataSource = null;

                var texto = buscarTextBox.Text.Trim();
                var soloBajoStock = bajoStockCheckBox.Checked;

                IEnumerable<ProductoDTO> productos;
                if (string.IsNullOrWhiteSpace(texto) && !soloBajoStock)
                    productos = await ProductoApiClient.GetAllAsync();
                else
                    productos = await ProductoApiClient.GetByCriteriaAsync(texto, soloBajoStock);

                var lista = productos.ToList();
                productosDataGridView.DataSource = lista;

                ResaltarBajoStock();

                resultadosLabel.Text = lista.Count == 1
                    ? "1 producto encontrado"
                    : $"{lista.Count} productos encontrados";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar productos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }
        }

        private void ResaltarBajoStock()
        {
            foreach (DataGridViewRow fila in productosDataGridView.Rows)
            {
                if (fila.DataBoundItem is ProductoDTO producto && producto.BajoStock)
                {
                    fila.DefaultCellStyle.BackColor = Color.MistyRose;
                    fila.DefaultCellStyle.SelectionBackColor = Color.IndianRed;
                }
            }
        }

        private ProductoDTO? ObtenerSeleccionado()
        {
            if (productosDataGridView.SelectedRows.Count == 0)
                return null;

            return productosDataGridView.SelectedRows[0].DataBoundItem as ProductoDTO;
        }

        private async void buscarButton_Click(object sender, EventArgs e)
        {
            await BuscarYCargarAsync();
        }

        private async void limpiarButton_Click(object sender, EventArgs e)
        {
            buscarTextBox.Clear();
            bajoStockCheckBox.Checked = false;
            await BuscarYCargarAsync();
        }

        private async void buscarTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await BuscarYCargarAsync();
            }
        }

        private async void agregarButton_Click(object sender, EventArgs e)
        {
            using var detalle = new ProductoDetalle(FormMode.Add, new ProductoDTO());
            detalle.ShowDialog();

            await BuscarYCargarAsync();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un producto de la lista.", "Modificar producto",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DeshabilitarControles();

                var producto = await ProductoApiClient.GetAsync(seleccionado.Id);

                using var detalle = new ProductoDetalle(FormMode.Update, producto);
                detalle.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al abrir el producto",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }

            await BuscarYCargarAsync();
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            var producto = ObtenerSeleccionado();
            if (producto == null)
            {
                MessageBox.Show("Seleccione un producto de la lista.", "Eliminar producto",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmar = MessageBox.Show(
                $"¿Está seguro que desea eliminar el producto \"{producto.Nombre}\"?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
                return;

            try
            {
                DeshabilitarControles();
                await ProductoApiClient.DeleteAsync(producto.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al eliminar el producto",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HabilitarControles();
            }

            await BuscarYCargarAsync();
        }

        private void cerrarButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DeshabilitarControles()
        {
            filtrosGroupBox.Enabled = false;
            productosDataGridView.Enabled = false;
            agregarButton.Enabled = false;
            actualizarButton.Enabled = false;
            eliminarButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void HabilitarControles()
        {
            filtrosGroupBox.Enabled = true;
            productosDataGridView.Enabled = true;

            var hayFilas = productosDataGridView.Rows.Count > 0;
            var esAdmin = SesionActual.EsAdministrador;

            agregarButton.Enabled = esAdmin;
            actualizarButton.Enabled = esAdmin && hayFilas;
            eliminarButton.Enabled = esAdmin && hayFilas;

            Cursor = Cursors.Default;
        }
    }
}
