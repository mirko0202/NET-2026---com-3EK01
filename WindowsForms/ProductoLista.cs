using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class ProductoLista : Form
    {
        private bool esAdministrador;
        private List<CategoriaDTO> categorias = new();

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
                Width = 190
            });

            productosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CategoriaNombre",
                HeaderText = "Categoría",
                DataPropertyName = "CategoriaNombre",
                Width = 100
            });

            productosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion",
                Width = 250
            });

            productosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Unidad",
                HeaderText = "Unidad",
                DataPropertyName = "Unidad",
                Width = 80
            });

            productosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                HeaderText = "Precio",
                DataPropertyName = "Precio",
                Width = 100,
                DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            productosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                HeaderText = "Stock",
                DataPropertyName = "Stock",
                Width = 60,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            productosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StockMin",
                HeaderText = "Stock mín.",
                DataPropertyName = "StockMin",
                Width = 75,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            productosDataGridView.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "BajoStock",
                HeaderText = "Bajo stock",
                DataPropertyName = "BajoStock",
                Width = 80
            });
        }

        private async void ProductoLista_Load(object sender, EventArgs e)
        {
            esAdministrador = await AuthServiceProvider.Instance.EsAdministradorAsync();
            AplicarAutorizacion();

            if (!await CargarCategoriasAsync())
                return;

            await BuscarYCargarAsync();
        }

        private async Task<bool> CargarCategoriasAsync()
        {
            try
            {
                categorias = (await ProductoApiClient.GetCategoriasAsync()).ToList();

                var opciones = new List<CategoriaDTO> { new() { Id = 0, Nombre = "Todas" } };
                opciones.AddRange(categorias);

                categoriaComboBox.DataSource = opciones;
                categoriaComboBox.DisplayMember = nameof(CategoriaDTO.Nombre);
                categoriaComboBox.ValueMember = nameof(CategoriaDTO.Id);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                Close();
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar las categorías",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return false;
            }
        }

        private int? CategoriaSeleccionada =>
            categoriaComboBox.SelectedValue is int id && id > 0 ? id : null;

        private void AplicarAutorizacion()
        {
            agregarButton.Visible = esAdministrador;
            actualizarButton.Visible = esAdministrador;
            eliminarButton.Visible = esAdministrador;

            Text = esAdministrador ? "Productos" : "Productos (solo consulta)";
        }

        private async Task BuscarYCargarAsync()
        {
            try
            {
                DeshabilitarControles();
                productosDataGridView.DataSource = null;

                var texto = buscarTextBox.Text.Trim();
                var soloBajoStock = bajoStockCheckBox.Checked;
                var categoria = CategoriaSeleccionada;

                IEnumerable<ProductoDTO> productos;
                if (string.IsNullOrWhiteSpace(texto) && !soloBajoStock && categoria == null)
                    productos = await ProductoApiClient.GetAllAsync();
                else
                    productos = await ProductoApiClient.GetByCriteriaAsync(texto, soloBajoStock, categoria);

                var lista = productos.ToList();
                productosDataGridView.DataSource = lista;

                ResaltarBajoStock();

                resultadosLabel.Text = lista.Count == 1
                    ? "1 producto encontrado"
                    : $"{lista.Count} productos encontrados";
            }
            catch (UnauthorizedAccessException)
            {
                Close();
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
            categoriaComboBox.SelectedIndex = 0;
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
            using var detalle = new ProductoDetalle(FormMode.Add, new ProductoDTO(), categorias);
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

                using var detalle = new ProductoDetalle(FormMode.Update, producto, categorias);
                detalle.ShowDialog();
            }
            catch (UnauthorizedAccessException)
            {
                Close();
                return;
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
            catch (UnauthorizedAccessException)
            {
                Close();
                return;
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

            agregarButton.Enabled = esAdministrador;
            actualizarButton.Enabled = esAdministrador && hayFilas;
            eliminarButton.Enabled = esAdministrador && hayFilas;

            Cursor = Cursors.Default;
        }
    }
}
