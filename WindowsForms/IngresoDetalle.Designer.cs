namespace WindowsForms
{
    partial class IngresoDetalle
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
            idLabel = new Label();
            idTextBox = new TextBox();
            fechaLabel = new Label();
            fechaDateTimePicker = new DateTimePicker();
            productoLabel = new Label();
            productoComboBox = new ComboBox();
            stockLabel = new Label();
            sucursalLabel = new Label();
            sucursalComboBox = new ComboBox();
            cantidadLabel = new Label();
            cantidadNumericUpDown = new NumericUpDown();
            proveedorLabel = new Label();
            proveedorTextBox = new TextBox();
            comprobanteLabel = new Label();
            comprobanteTextBox = new TextBox();
            observacionLabel = new Label();
            observacionTextBox = new TextBox();
            registradoLabel = new Label();
            registradoTextBox = new TextBox();
            aceptarButton = new Button();
            cancelarButton = new Button();
            errorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)cantidadNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            //
            // idLabel
            //
            idLabel.AutoSize = true;
            idLabel.Location = new Point(22, 23);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(70, 15);
            idLabel.TabIndex = 0;
            idLabel.Text = "Ingreso N°";
            //
            // idTextBox
            //
            idTextBox.Location = new Point(132, 20);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(80, 23);
            idTextBox.TabIndex = 1;
            idTextBox.TabStop = false;
            //
            // fechaLabel
            //
            fechaLabel.AutoSize = true;
            fechaLabel.Location = new Point(22, 58);
            fechaLabel.Name = "fechaLabel";
            fechaLabel.Size = new Size(82, 15);
            fechaLabel.TabIndex = 2;
            fechaLabel.Text = "Fecha y hora";
            //
            // fechaDateTimePicker
            //
            fechaDateTimePicker.CustomFormat = "dd/MM/yyyy HH:mm";
            fechaDateTimePicker.Format = DateTimePickerFormat.Custom;
            fechaDateTimePicker.Location = new Point(132, 55);
            fechaDateTimePicker.Name = "fechaDateTimePicker";
            fechaDateTimePicker.Size = new Size(160, 23);
            fechaDateTimePicker.TabIndex = 3;
            //
            // productoLabel
            //
            productoLabel.AutoSize = true;
            productoLabel.Location = new Point(22, 93);
            productoLabel.Name = "productoLabel";
            productoLabel.Size = new Size(56, 15);
            productoLabel.TabIndex = 4;
            productoLabel.Text = "Producto";
            //
            // productoComboBox
            //
            productoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            productoComboBox.FormattingEnabled = true;
            productoComboBox.Location = new Point(132, 90);
            productoComboBox.Name = "productoComboBox";
            productoComboBox.Size = new Size(240, 23);
            productoComboBox.TabIndex = 5;
            productoComboBox.SelectedIndexChanged += productoComboBox_SelectedIndexChanged;
            //
            // stockLabel
            //
            stockLabel.AutoSize = true;
            stockLabel.ForeColor = SystemColors.GrayText;
            stockLabel.Location = new Point(132, 117);
            stockLabel.Name = "stockLabel";
            stockLabel.Size = new Size(0, 15);
            stockLabel.TabIndex = 6;
            //
            // sucursalLabel
            //
            sucursalLabel.AutoSize = true;
            sucursalLabel.Location = new Point(22, 145);
            sucursalLabel.Name = "sucursalLabel";
            sucursalLabel.Size = new Size(51, 15);
            sucursalLabel.TabIndex = 7;
            sucursalLabel.Text = "Sucursal";
            //
            // sucursalComboBox
            //
            sucursalComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sucursalComboBox.FormattingEnabled = true;
            sucursalComboBox.Location = new Point(132, 142);
            sucursalComboBox.Name = "sucursalComboBox";
            sucursalComboBox.Size = new Size(240, 23);
            sucursalComboBox.TabIndex = 8;
            //
            // cantidadLabel
            //
            cantidadLabel.AutoSize = true;
            cantidadLabel.Location = new Point(22, 180);
            cantidadLabel.Name = "cantidadLabel";
            cantidadLabel.Size = new Size(55, 15);
            cantidadLabel.TabIndex = 9;
            cantidadLabel.Text = "Cantidad";
            //
            // cantidadNumericUpDown
            //
            cantidadNumericUpDown.Location = new Point(132, 178);
            cantidadNumericUpDown.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            cantidadNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            cantidadNumericUpDown.Name = "cantidadNumericUpDown";
            cantidadNumericUpDown.Size = new Size(120, 23);
            cantidadNumericUpDown.TabIndex = 10;
            cantidadNumericUpDown.ThousandsSeparator = true;
            cantidadNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // proveedorLabel
            //
            proveedorLabel.AutoSize = true;
            proveedorLabel.Location = new Point(22, 215);
            proveedorLabel.Name = "proveedorLabel";
            proveedorLabel.Size = new Size(61, 15);
            proveedorLabel.TabIndex = 11;
            proveedorLabel.Text = "Proveedor";
            //
            // proveedorTextBox
            //
            proveedorTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            proveedorTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            proveedorTextBox.Location = new Point(132, 212);
            proveedorTextBox.MaxLength = 100;
            proveedorTextBox.Name = "proveedorTextBox";
            proveedorTextBox.Size = new Size(290, 23);
            proveedorTextBox.TabIndex = 12;
            //
            // comprobanteLabel
            //
            comprobanteLabel.AutoSize = true;
            comprobanteLabel.Location = new Point(22, 250);
            comprobanteLabel.Name = "comprobanteLabel";
            comprobanteLabel.Size = new Size(79, 15);
            comprobanteLabel.TabIndex = 13;
            comprobanteLabel.Text = "Comprobante";
            //
            // comprobanteTextBox
            //
            comprobanteTextBox.Location = new Point(132, 247);
            comprobanteTextBox.MaxLength = 50;
            comprobanteTextBox.Name = "comprobanteTextBox";
            comprobanteTextBox.PlaceholderText = "Remito o factura (opcional)";
            comprobanteTextBox.Size = new Size(200, 23);
            comprobanteTextBox.TabIndex = 14;
            //
            // observacionLabel
            //
            observacionLabel.AutoSize = true;
            observacionLabel.Location = new Point(22, 285);
            observacionLabel.Name = "observacionLabel";
            observacionLabel.Size = new Size(72, 15);
            observacionLabel.TabIndex = 15;
            observacionLabel.Text = "Observación";
            //
            // observacionTextBox
            //
            observacionTextBox.Location = new Point(132, 282);
            observacionTextBox.MaxLength = 255;
            observacionTextBox.Multiline = true;
            observacionTextBox.Name = "observacionTextBox";
            observacionTextBox.ScrollBars = ScrollBars.Vertical;
            observacionTextBox.Size = new Size(290, 55);
            observacionTextBox.TabIndex = 16;
            //
            // registradoLabel
            //
            registradoLabel.AutoSize = true;
            registradoLabel.Location = new Point(22, 352);
            registradoLabel.Name = "registradoLabel";
            registradoLabel.Size = new Size(84, 15);
            registradoLabel.TabIndex = 17;
            registradoLabel.Text = "Registrado por";
            //
            // registradoTextBox
            //
            registradoTextBox.Location = new Point(132, 349);
            registradoTextBox.Name = "registradoTextBox";
            registradoTextBox.ReadOnly = true;
            registradoTextBox.Size = new Size(240, 23);
            registradoTextBox.TabIndex = 18;
            registradoTextBox.TabStop = false;
            //
            // aceptarButton
            //
            aceptarButton.Location = new Point(302, 392);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(120, 32);
            aceptarButton.TabIndex = 19;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            //
            // cancelarButton
            //
            cancelarButton.Location = new Point(172, 392);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(120, 32);
            cancelarButton.TabIndex = 20;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            //
            // errorProvider
            //
            errorProvider.ContainerControl = this;
            //
            // IngresoDetalle
            //
            AcceptButton = aceptarButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelarButton;
            ClientSize = new Size(460, 442);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(registradoTextBox);
            Controls.Add(registradoLabel);
            Controls.Add(observacionTextBox);
            Controls.Add(observacionLabel);
            Controls.Add(comprobanteTextBox);
            Controls.Add(comprobanteLabel);
            Controls.Add(proveedorTextBox);
            Controls.Add(proveedorLabel);
            Controls.Add(cantidadNumericUpDown);
            Controls.Add(cantidadLabel);
            Controls.Add(sucursalComboBox);
            Controls.Add(sucursalLabel);
            Controls.Add(stockLabel);
            Controls.Add(productoComboBox);
            Controls.Add(productoLabel);
            Controls.Add(fechaDateTimePicker);
            Controls.Add(fechaLabel);
            Controls.Add(idTextBox);
            Controls.Add(idLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IngresoDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ingreso de mercadería";
            Load += IngresoDetalle_Load;
            ((System.ComponentModel.ISupportInitialize)cantidadNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label idLabel;
        private TextBox idTextBox;
        private Label fechaLabel;
        private DateTimePicker fechaDateTimePicker;
        private Label productoLabel;
        private ComboBox productoComboBox;
        private Label stockLabel;
        private Label sucursalLabel;
        private ComboBox sucursalComboBox;
        private Label cantidadLabel;
        private NumericUpDown cantidadNumericUpDown;
        private Label proveedorLabel;
        private TextBox proveedorTextBox;
        private Label comprobanteLabel;
        private TextBox comprobanteTextBox;
        private Label observacionLabel;
        private TextBox observacionTextBox;
        private Label registradoLabel;
        private TextBox registradoTextBox;
        private Button aceptarButton;
        private Button cancelarButton;
        private ErrorProvider errorProvider;
    }
}
