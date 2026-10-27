namespace WindowsForms
{
    partial class VentaDetalle
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
            components = new System.ComponentModel.Container();
            datosGroupBox = new GroupBox();
            idLabel = new Label();
            idTextBox = new TextBox();
            fechaLabel = new Label();
            fechaTextBox = new TextBox();
            vendedorLabel = new Label();
            vendedorTextBox = new TextBox();
            sucursalLabel = new Label();
            sucursalComboBox = new ComboBox();
            productosGroupBox = new GroupBox();
            itemsDataGridView = new DataGridView();
            agregarItemButton = new Button();
            modificarItemButton = new Button();
            quitarItemButton = new Button();
            totalLabel = new Label();
            aceptarButton = new Button();
            cancelarButton = new Button();
            errorProvider = new ErrorProvider(components);
            datosGroupBox.SuspendLayout();
            productosGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)itemsDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            //
            // datosGroupBox
            //
            datosGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            datosGroupBox.Controls.Add(idLabel);
            datosGroupBox.Controls.Add(idTextBox);
            datosGroupBox.Controls.Add(fechaLabel);
            datosGroupBox.Controls.Add(fechaTextBox);
            datosGroupBox.Controls.Add(vendedorLabel);
            datosGroupBox.Controls.Add(vendedorTextBox);
            datosGroupBox.Controls.Add(sucursalLabel);
            datosGroupBox.Controls.Add(sucursalComboBox);
            datosGroupBox.Location = new Point(14, 12);
            datosGroupBox.Name = "datosGroupBox";
            datosGroupBox.Size = new Size(752, 108);
            datosGroupBox.TabIndex = 0;
            datosGroupBox.TabStop = false;
            datosGroupBox.Text = "Datos de la venta";
            //
            // idLabel
            //
            idLabel.AutoSize = true;
            idLabel.Location = new Point(16, 32);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(70, 15);
            idLabel.TabIndex = 0;
            idLabel.Text = "N° de venta";
            //
            // idTextBox
            //
            idTextBox.Location = new Point(100, 29);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(80, 23);
            idTextBox.TabIndex = 1;
            idTextBox.TabStop = false;
            //
            // fechaLabel
            //
            fechaLabel.AutoSize = true;
            fechaLabel.Location = new Point(200, 32);
            fechaLabel.Name = "fechaLabel";
            fechaLabel.Size = new Size(38, 15);
            fechaLabel.TabIndex = 2;
            fechaLabel.Text = "Fecha";
            //
            // fechaTextBox
            //
            fechaTextBox.Location = new Point(246, 29);
            fechaTextBox.Name = "fechaTextBox";
            fechaTextBox.ReadOnly = true;
            fechaTextBox.Size = new Size(130, 23);
            fechaTextBox.TabIndex = 3;
            fechaTextBox.TabStop = false;
            //
            // vendedorLabel
            //
            vendedorLabel.AutoSize = true;
            vendedorLabel.Location = new Point(396, 32);
            vendedorLabel.Name = "vendedorLabel";
            vendedorLabel.Size = new Size(57, 15);
            vendedorLabel.TabIndex = 4;
            vendedorLabel.Text = "Vendedor";
            //
            // vendedorTextBox
            //
            vendedorTextBox.Location = new Point(464, 29);
            vendedorTextBox.Name = "vendedorTextBox";
            vendedorTextBox.ReadOnly = true;
            vendedorTextBox.Size = new Size(260, 23);
            vendedorTextBox.TabIndex = 5;
            vendedorTextBox.TabStop = false;
            //
            // sucursalLabel
            //
            sucursalLabel.AutoSize = true;
            sucursalLabel.Location = new Point(16, 70);
            sucursalLabel.Name = "sucursalLabel";
            sucursalLabel.Size = new Size(51, 15);
            sucursalLabel.TabIndex = 6;
            sucursalLabel.Text = "Sucursal";
            //
            // sucursalComboBox
            //
            sucursalComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sucursalComboBox.FormattingEnabled = true;
            sucursalComboBox.Location = new Point(100, 67);
            sucursalComboBox.Name = "sucursalComboBox";
            sucursalComboBox.Size = new Size(276, 23);
            sucursalComboBox.TabIndex = 7;
            sucursalComboBox.SelectedIndexChanged += sucursalComboBox_SelectedIndexChanged;
            //
            // productosGroupBox
            //
            productosGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            productosGroupBox.Controls.Add(itemsDataGridView);
            productosGroupBox.Controls.Add(agregarItemButton);
            productosGroupBox.Controls.Add(modificarItemButton);
            productosGroupBox.Controls.Add(quitarItemButton);
            productosGroupBox.Controls.Add(totalLabel);
            productosGroupBox.Location = new Point(14, 130);
            productosGroupBox.Name = "productosGroupBox";
            productosGroupBox.Size = new Size(752, 404);
            productosGroupBox.TabIndex = 1;
            productosGroupBox.TabStop = false;
            productosGroupBox.Text = "Productos vendidos";
            //
            // itemsDataGridView
            //
            itemsDataGridView.AllowUserToAddRows = false;
            itemsDataGridView.AllowUserToDeleteRows = false;
            itemsDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            itemsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            itemsDataGridView.Location = new Point(14, 26);
            itemsDataGridView.MultiSelect = false;
            itemsDataGridView.Name = "itemsDataGridView";
            itemsDataGridView.ReadOnly = true;
            itemsDataGridView.RowHeadersVisible = false;
            itemsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            itemsDataGridView.Size = new Size(724, 316);
            itemsDataGridView.TabIndex = 0;
            itemsDataGridView.CellDoubleClick += itemsDataGridView_CellDoubleClick;
            //
            // agregarItemButton
            //
            agregarItemButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            agregarItemButton.Location = new Point(14, 356);
            agregarItemButton.Name = "agregarItemButton";
            agregarItemButton.Size = new Size(150, 32);
            agregarItemButton.TabIndex = 1;
            agregarItemButton.Text = "Agregar producto";
            agregarItemButton.UseVisualStyleBackColor = true;
            agregarItemButton.Click += agregarItemButton_Click;
            //
            // modificarItemButton
            //
            modificarItemButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            modificarItemButton.Enabled = false;
            modificarItemButton.Location = new Point(172, 356);
            modificarItemButton.Name = "modificarItemButton";
            modificarItemButton.Size = new Size(150, 32);
            modificarItemButton.TabIndex = 2;
            modificarItemButton.Text = "Modificar producto";
            modificarItemButton.UseVisualStyleBackColor = true;
            modificarItemButton.Click += modificarItemButton_Click;
            //
            // quitarItemButton
            //
            quitarItemButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            quitarItemButton.Enabled = false;
            quitarItemButton.Location = new Point(330, 356);
            quitarItemButton.Name = "quitarItemButton";
            quitarItemButton.Size = new Size(150, 32);
            quitarItemButton.TabIndex = 3;
            quitarItemButton.Text = "Quitar producto";
            quitarItemButton.UseVisualStyleBackColor = true;
            quitarItemButton.Click += quitarItemButton_Click;
            //
            // totalLabel
            //
            totalLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            totalLabel.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            totalLabel.Location = new Point(488, 356);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new Size(250, 32);
            totalLabel.TabIndex = 4;
            totalLabel.Text = "Total: $ 0,00";
            totalLabel.TextAlign = ContentAlignment.MiddleRight;
            //
            // aceptarButton
            //
            aceptarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            aceptarButton.Location = new Point(646, 548);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(120, 32);
            aceptarButton.TabIndex = 2;
            aceptarButton.Text = "Guardar venta";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            //
            // cancelarButton
            //
            cancelarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelarButton.DialogResult = DialogResult.Cancel;
            cancelarButton.Location = new Point(520, 548);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(120, 32);
            cancelarButton.TabIndex = 3;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            //
            // errorProvider
            //
            errorProvider.ContainerControl = this;
            //
            // VentaDetalle
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelarButton;
            ClientSize = new Size(780, 594);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(productosGroupBox);
            Controls.Add(datosGroupBox);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(740, 520);
            Name = "VentaDetalle";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Venta";
            Load += VentaDetalle_Load;
            datosGroupBox.ResumeLayout(false);
            datosGroupBox.PerformLayout();
            productosGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)itemsDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox datosGroupBox;
        private Label idLabel;
        private TextBox idTextBox;
        private Label fechaLabel;
        private TextBox fechaTextBox;
        private Label vendedorLabel;
        private TextBox vendedorTextBox;
        private Label sucursalLabel;
        private ComboBox sucursalComboBox;
        private GroupBox productosGroupBox;
        private DataGridView itemsDataGridView;
        private Button agregarItemButton;
        private Button modificarItemButton;
        private Button quitarItemButton;
        private Label totalLabel;
        private Button aceptarButton;
        private Button cancelarButton;
        private ErrorProvider errorProvider;
    }
}
