namespace WindowsForms
{
    partial class ProductoLista
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            filtrosGroupBox = new GroupBox();
            buscarLabel = new Label();
            buscarTextBox = new TextBox();
            categoriaLabel = new Label();
            categoriaComboBox = new ComboBox();
            bajoStockCheckBox = new CheckBox();
            buscarButton = new Button();
            limpiarButton = new Button();
            productosDataGridView = new DataGridView();
            agregarButton = new Button();
            actualizarButton = new Button();
            eliminarButton = new Button();
            cerrarButton = new Button();
            resultadosLabel = new Label();
            filtrosGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)productosDataGridView).BeginInit();
            SuspendLayout();
            //
            // filtrosGroupBox
            //
            filtrosGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filtrosGroupBox.Controls.Add(buscarLabel);
            filtrosGroupBox.Controls.Add(buscarTextBox);
            filtrosGroupBox.Controls.Add(categoriaLabel);
            filtrosGroupBox.Controls.Add(categoriaComboBox);
            filtrosGroupBox.Controls.Add(bajoStockCheckBox);
            filtrosGroupBox.Controls.Add(buscarButton);
            filtrosGroupBox.Controls.Add(limpiarButton);
            filtrosGroupBox.Location = new Point(14, 12);
            filtrosGroupBox.Name = "filtrosGroupBox";
            filtrosGroupBox.Size = new Size(1032, 70);
            filtrosGroupBox.TabIndex = 0;
            filtrosGroupBox.TabStop = false;
            filtrosGroupBox.Text = "Búsqueda con filtros";
            //
            // buscarLabel
            //
            buscarLabel.AutoSize = true;
            buscarLabel.Location = new Point(16, 31);
            buscarLabel.Name = "buscarLabel";
            buscarLabel.Size = new Size(45, 15);
            buscarLabel.TabIndex = 0;
            buscarLabel.Text = "Texto:";
            //
            // buscarTextBox
            //
            buscarTextBox.Location = new Point(67, 28);
            buscarTextBox.MaxLength = 100;
            buscarTextBox.Name = "buscarTextBox";
            buscarTextBox.PlaceholderText = "Nombre o descripción...";
            buscarTextBox.Size = new Size(240, 23);
            buscarTextBox.TabIndex = 1;
            buscarTextBox.KeyDown += buscarTextBox_KeyDown;
            //
            // categoriaLabel
            //
            categoriaLabel.AutoSize = true;
            categoriaLabel.Location = new Point(325, 31);
            categoriaLabel.Name = "categoriaLabel";
            categoriaLabel.Size = new Size(61, 15);
            categoriaLabel.TabIndex = 2;
            categoriaLabel.Text = "Categoría:";
            //
            // categoriaComboBox
            //
            categoriaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            categoriaComboBox.FormattingEnabled = true;
            categoriaComboBox.Location = new Point(392, 28);
            categoriaComboBox.Name = "categoriaComboBox";
            categoriaComboBox.Size = new Size(160, 23);
            categoriaComboBox.TabIndex = 3;
            //
            // bajoStockCheckBox
            //
            bajoStockCheckBox.AutoSize = true;
            bajoStockCheckBox.Location = new Point(572, 30);
            bajoStockCheckBox.Name = "bajoStockCheckBox";
            bajoStockCheckBox.Size = new Size(190, 19);
            bajoStockCheckBox.TabIndex = 4;
            bajoStockCheckBox.Text = "Solo productos bajo stock mínimo";
            bajoStockCheckBox.UseVisualStyleBackColor = true;
            //
            // buscarButton
            //
            buscarButton.Location = new Point(806, 26);
            buscarButton.Name = "buscarButton";
            buscarButton.Size = new Size(100, 28);
            buscarButton.TabIndex = 5;
            buscarButton.Text = "Buscar";
            buscarButton.UseVisualStyleBackColor = true;
            buscarButton.Click += buscarButton_Click;
            //
            // limpiarButton
            //
            limpiarButton.Location = new Point(916, 26);
            limpiarButton.Name = "limpiarButton";
            limpiarButton.Size = new Size(100, 28);
            limpiarButton.TabIndex = 6;
            limpiarButton.Text = "Limpiar";
            limpiarButton.UseVisualStyleBackColor = true;
            limpiarButton.Click += limpiarButton_Click;
            //
            // productosDataGridView
            //
            productosDataGridView.AllowUserToAddRows = false;
            productosDataGridView.AllowUserToDeleteRows = false;
            productosDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            productosDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            productosDataGridView.Location = new Point(14, 92);
            productosDataGridView.MultiSelect = false;
            productosDataGridView.Name = "productosDataGridView";
            productosDataGridView.ReadOnly = true;
            productosDataGridView.RowHeadersVisible = false;
            productosDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            productosDataGridView.Size = new Size(1032, 380);
            productosDataGridView.TabIndex = 1;
            //
            // agregarButton
            //
            agregarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            agregarButton.Location = new Point(646, 490);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(120, 32);
            agregarButton.TabIndex = 2;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            //
            // actualizarButton
            //
            actualizarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            actualizarButton.Enabled = false;
            actualizarButton.Location = new Point(772, 490);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(120, 32);
            actualizarButton.TabIndex = 3;
            actualizarButton.Text = "Modificar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            //
            // eliminarButton
            //
            eliminarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            eliminarButton.Enabled = false;
            eliminarButton.Location = new Point(898, 490);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(120, 32);
            eliminarButton.TabIndex = 4;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            //
            // cerrarButton
            //
            cerrarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cerrarButton.Location = new Point(14, 490);
            cerrarButton.Name = "cerrarButton";
            cerrarButton.Size = new Size(120, 32);
            cerrarButton.TabIndex = 5;
            cerrarButton.Text = "Cerrar";
            cerrarButton.UseVisualStyleBackColor = true;
            cerrarButton.Click += cerrarButton_Click;
            //
            // resultadosLabel
            //
            resultadosLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            resultadosLabel.AutoSize = true;
            resultadosLabel.ForeColor = SystemColors.GrayText;
            resultadosLabel.Location = new Point(148, 499);
            resultadosLabel.Name = "resultadosLabel";
            resultadosLabel.Size = new Size(0, 15);
            resultadosLabel.TabIndex = 6;
            //
            // ProductoLista
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1060, 540);
            Controls.Add(resultadosLabel);
            Controls.Add(cerrarButton);
            Controls.Add(eliminarButton);
            Controls.Add(actualizarButton);
            Controls.Add(agregarButton);
            Controls.Add(productosDataGridView);
            Controls.Add(filtrosGroupBox);
            MinimumSize = new Size(1076, 480);
            Name = "ProductoLista";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Productos";
            Load += ProductoLista_Load;
            filtrosGroupBox.ResumeLayout(false);
            filtrosGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)productosDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox filtrosGroupBox;
        private Label buscarLabel;
        private TextBox buscarTextBox;
        private Label categoriaLabel;
        private ComboBox categoriaComboBox;
        private CheckBox bajoStockCheckBox;
        private Button buscarButton;
        private Button limpiarButton;
        private DataGridView productosDataGridView;
        private Button agregarButton;
        private Button actualizarButton;
        private Button eliminarButton;
        private Button cerrarButton;
        private Label resultadosLabel;
    }
}
