namespace WindowsForms
{
    partial class DetalleVentaItem
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
            productoLabel = new Label();
            productoComboBox = new ComboBox();
            stockLabel = new Label();
            stockValorLabel = new Label();
            cantidadLabel = new Label();
            cantidadNumericUpDown = new NumericUpDown();
            precioLabel = new Label();
            precioNumericUpDown = new NumericUpDown();
            subtotalLabel = new Label();
            subtotalValorLabel = new Label();
            aceptarButton = new Button();
            cancelarButton = new Button();
            errorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)cantidadNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)precioNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            //
            // productoLabel
            //
            productoLabel.AutoSize = true;
            productoLabel.Location = new Point(22, 25);
            productoLabel.Name = "productoLabel";
            productoLabel.Size = new Size(56, 15);
            productoLabel.TabIndex = 0;
            productoLabel.Text = "Producto";
            //
            // productoComboBox
            //
            productoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            productoComboBox.FormattingEnabled = true;
            productoComboBox.Location = new Point(142, 22);
            productoComboBox.Name = "productoComboBox";
            productoComboBox.Size = new Size(270, 23);
            productoComboBox.TabIndex = 1;
            productoComboBox.SelectedIndexChanged += productoComboBox_SelectedIndexChanged;
            //
            // stockLabel
            //
            stockLabel.AutoSize = true;
            stockLabel.Location = new Point(22, 61);
            stockLabel.Name = "stockLabel";
            stockLabel.Size = new Size(93, 15);
            stockLabel.TabIndex = 2;
            stockLabel.Text = "Stock disponible";
            //
            // stockValorLabel
            //
            stockValorLabel.AutoSize = true;
            stockValorLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            stockValorLabel.Location = new Point(142, 61);
            stockValorLabel.Name = "stockValorLabel";
            stockValorLabel.Size = new Size(12, 15);
            stockValorLabel.TabIndex = 3;
            stockValorLabel.Text = "-";
            //
            // cantidadLabel
            //
            cantidadLabel.AutoSize = true;
            cantidadLabel.Location = new Point(22, 97);
            cantidadLabel.Name = "cantidadLabel";
            cantidadLabel.Size = new Size(55, 15);
            cantidadLabel.TabIndex = 4;
            cantidadLabel.Text = "Cantidad";
            //
            // cantidadNumericUpDown
            //
            cantidadNumericUpDown.Location = new Point(142, 95);
            cantidadNumericUpDown.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            cantidadNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            cantidadNumericUpDown.Name = "cantidadNumericUpDown";
            cantidadNumericUpDown.Size = new Size(120, 23);
            cantidadNumericUpDown.TabIndex = 5;
            cantidadNumericUpDown.TextAlign = HorizontalAlignment.Right;
            cantidadNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            cantidadNumericUpDown.ValueChanged += valores_ValueChanged;
            //
            // precioLabel
            //
            precioLabel.AutoSize = true;
            precioLabel.Location = new Point(22, 133);
            precioLabel.Name = "precioLabel";
            precioLabel.Size = new Size(87, 15);
            precioLabel.TabIndex = 6;
            precioLabel.Text = "Precio unitario";
            //
            // precioNumericUpDown
            //
            precioNumericUpDown.DecimalPlaces = 2;
            precioNumericUpDown.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            precioNumericUpDown.Location = new Point(142, 131);
            precioNumericUpDown.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            precioNumericUpDown.Name = "precioNumericUpDown";
            precioNumericUpDown.Size = new Size(120, 23);
            precioNumericUpDown.TabIndex = 7;
            precioNumericUpDown.TextAlign = HorizontalAlignment.Right;
            precioNumericUpDown.ThousandsSeparator = true;
            precioNumericUpDown.ValueChanged += valores_ValueChanged;
            //
            // subtotalLabel
            //
            subtotalLabel.AutoSize = true;
            subtotalLabel.Location = new Point(22, 169);
            subtotalLabel.Name = "subtotalLabel";
            subtotalLabel.Size = new Size(51, 15);
            subtotalLabel.TabIndex = 8;
            subtotalLabel.Text = "Subtotal";
            //
            // subtotalValorLabel
            //
            subtotalValorLabel.AutoSize = true;
            subtotalValorLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            subtotalValorLabel.Location = new Point(142, 169);
            subtotalValorLabel.Name = "subtotalValorLabel";
            subtotalValorLabel.Size = new Size(12, 15);
            subtotalValorLabel.TabIndex = 9;
            subtotalValorLabel.Text = "-";
            //
            // aceptarButton
            //
            aceptarButton.Location = new Point(292, 206);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(120, 32);
            aceptarButton.TabIndex = 10;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            //
            // cancelarButton
            //
            cancelarButton.DialogResult = DialogResult.Cancel;
            cancelarButton.Location = new Point(166, 206);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(120, 32);
            cancelarButton.TabIndex = 11;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            //
            // errorProvider
            //
            errorProvider.ContainerControl = this;
            //
            // DetalleVentaItem
            //
            AcceptButton = aceptarButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelarButton;
            ClientSize = new Size(444, 254);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(subtotalValorLabel);
            Controls.Add(subtotalLabel);
            Controls.Add(precioNumericUpDown);
            Controls.Add(precioLabel);
            Controls.Add(cantidadNumericUpDown);
            Controls.Add(cantidadLabel);
            Controls.Add(stockValorLabel);
            Controls.Add(stockLabel);
            Controls.Add(productoComboBox);
            Controls.Add(productoLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DetalleVentaItem";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Producto de la venta";
            Load += DetalleVentaItem_Load;
            ((System.ComponentModel.ISupportInitialize)cantidadNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)precioNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label productoLabel;
        private ComboBox productoComboBox;
        private Label stockLabel;
        private Label stockValorLabel;
        private Label cantidadLabel;
        private NumericUpDown cantidadNumericUpDown;
        private Label precioLabel;
        private NumericUpDown precioNumericUpDown;
        private Label subtotalLabel;
        private Label subtotalValorLabel;
        private Button aceptarButton;
        private Button cancelarButton;
        private ErrorProvider errorProvider;
    }
}
