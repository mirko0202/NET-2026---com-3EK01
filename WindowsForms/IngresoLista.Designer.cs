namespace WindowsForms
{
    partial class IngresoLista
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
            productoLabel = new Label();
            productoComboBox = new ComboBox();
            desdeLabel = new Label();
            desdeDateTimePicker = new DateTimePicker();
            hastaLabel = new Label();
            hastaDateTimePicker = new DateTimePicker();
            buscarButton = new Button();
            limpiarButton = new Button();
            ingresosDataGridView = new DataGridView();
            agregarButton = new Button();
            actualizarButton = new Button();
            eliminarButton = new Button();
            cerrarButton = new Button();
            resultadosLabel = new Label();
            filtrosGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ingresosDataGridView).BeginInit();
            SuspendLayout();
            //
            // filtrosGroupBox
            //
            filtrosGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filtrosGroupBox.Controls.Add(productoLabel);
            filtrosGroupBox.Controls.Add(productoComboBox);
            filtrosGroupBox.Controls.Add(desdeLabel);
            filtrosGroupBox.Controls.Add(desdeDateTimePicker);
            filtrosGroupBox.Controls.Add(hastaLabel);
            filtrosGroupBox.Controls.Add(hastaDateTimePicker);
            filtrosGroupBox.Controls.Add(buscarButton);
            filtrosGroupBox.Controls.Add(limpiarButton);
            filtrosGroupBox.Location = new Point(14, 12);
            filtrosGroupBox.Name = "filtrosGroupBox";
            filtrosGroupBox.Size = new Size(1072, 70);
            filtrosGroupBox.TabIndex = 0;
            filtrosGroupBox.TabStop = false;
            filtrosGroupBox.Text = "Historial de ingresos";
            //
            // productoLabel
            //
            productoLabel.AutoSize = true;
            productoLabel.Location = new Point(16, 31);
            productoLabel.Name = "productoLabel";
            productoLabel.Size = new Size(59, 15);
            productoLabel.TabIndex = 0;
            productoLabel.Text = "Producto:";
            //
            // productoComboBox
            //
            productoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            productoComboBox.FormattingEnabled = true;
            productoComboBox.Location = new Point(81, 28);
            productoComboBox.Name = "productoComboBox";
            productoComboBox.Size = new Size(220, 23);
            productoComboBox.TabIndex = 1;
            //
            // desdeLabel
            //
            desdeLabel.AutoSize = true;
            desdeLabel.Location = new Point(325, 31);
            desdeLabel.Name = "desdeLabel";
            desdeLabel.Size = new Size(42, 15);
            desdeLabel.TabIndex = 2;
            desdeLabel.Text = "Desde:";
            //
            // desdeDateTimePicker
            //
            desdeDateTimePicker.Checked = false;
            desdeDateTimePicker.CustomFormat = "dd/MM/yyyy";
            desdeDateTimePicker.Format = DateTimePickerFormat.Custom;
            desdeDateTimePicker.Location = new Point(373, 28);
            desdeDateTimePicker.Name = "desdeDateTimePicker";
            desdeDateTimePicker.ShowCheckBox = true;
            desdeDateTimePicker.Size = new Size(130, 23);
            desdeDateTimePicker.TabIndex = 3;
            //
            // hastaLabel
            //
            hastaLabel.AutoSize = true;
            hastaLabel.Location = new Point(523, 31);
            hastaLabel.Name = "hastaLabel";
            hastaLabel.Size = new Size(40, 15);
            hastaLabel.TabIndex = 4;
            hastaLabel.Text = "Hasta:";
            //
            // hastaDateTimePicker
            //
            hastaDateTimePicker.Checked = false;
            hastaDateTimePicker.CustomFormat = "dd/MM/yyyy";
            hastaDateTimePicker.Format = DateTimePickerFormat.Custom;
            hastaDateTimePicker.Location = new Point(569, 28);
            hastaDateTimePicker.Name = "hastaDateTimePicker";
            hastaDateTimePicker.ShowCheckBox = true;
            hastaDateTimePicker.Size = new Size(130, 23);
            hastaDateTimePicker.TabIndex = 5;
            //
            // buscarButton
            //
            buscarButton.Location = new Point(730, 26);
            buscarButton.Name = "buscarButton";
            buscarButton.Size = new Size(100, 28);
            buscarButton.TabIndex = 6;
            buscarButton.Text = "Buscar";
            buscarButton.UseVisualStyleBackColor = true;
            buscarButton.Click += buscarButton_Click;
            //
            // limpiarButton
            //
            limpiarButton.Location = new Point(840, 26);
            limpiarButton.Name = "limpiarButton";
            limpiarButton.Size = new Size(100, 28);
            limpiarButton.TabIndex = 7;
            limpiarButton.Text = "Limpiar";
            limpiarButton.UseVisualStyleBackColor = true;
            limpiarButton.Click += limpiarButton_Click;
            //
            // ingresosDataGridView
            //
            ingresosDataGridView.AllowUserToAddRows = false;
            ingresosDataGridView.AllowUserToDeleteRows = false;
            ingresosDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ingresosDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ingresosDataGridView.Location = new Point(14, 92);
            ingresosDataGridView.MultiSelect = false;
            ingresosDataGridView.Name = "ingresosDataGridView";
            ingresosDataGridView.ReadOnly = true;
            ingresosDataGridView.RowHeadersVisible = false;
            ingresosDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ingresosDataGridView.Size = new Size(1072, 380);
            ingresosDataGridView.TabIndex = 1;
            ingresosDataGridView.CellDoubleClick += ingresosDataGridView_CellDoubleClick;
            //
            // agregarButton
            //
            agregarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            agregarButton.Location = new Point(686, 490);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(140, 32);
            agregarButton.TabIndex = 2;
            agregarButton.Text = "Registrar ingreso";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            //
            // actualizarButton
            //
            actualizarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            actualizarButton.Enabled = false;
            actualizarButton.Location = new Point(832, 490);
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
            eliminarButton.Location = new Point(958, 490);
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
            // IngresoLista
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 540);
            Controls.Add(resultadosLabel);
            Controls.Add(cerrarButton);
            Controls.Add(eliminarButton);
            Controls.Add(actualizarButton);
            Controls.Add(agregarButton);
            Controls.Add(ingresosDataGridView);
            Controls.Add(filtrosGroupBox);
            MinimumSize = new Size(1000, 480);
            Name = "IngresoLista";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ingresos de mercadería";
            Load += IngresoLista_Load;
            filtrosGroupBox.ResumeLayout(false);
            filtrosGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ingresosDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox filtrosGroupBox;
        private Label productoLabel;
        private ComboBox productoComboBox;
        private Label desdeLabel;
        private DateTimePicker desdeDateTimePicker;
        private Label hastaLabel;
        private DateTimePicker hastaDateTimePicker;
        private Button buscarButton;
        private Button limpiarButton;
        private DataGridView ingresosDataGridView;
        private Button agregarButton;
        private Button actualizarButton;
        private Button eliminarButton;
        private Button cerrarButton;
        private Label resultadosLabel;
    }
}
