namespace WindowsForms
{
    partial class IngredienteDetalle
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
            nombreLabel = new Label();
            nombreTextBox = new TextBox();
            descripcionLabel = new Label();
            descripcionTextBox = new TextBox();
            unidadLabel = new Label();
            unidadComboBox = new ComboBox();
            stockLabel = new Label();
            stockNumericUpDown = new NumericUpDown();
            stockMinLabel = new Label();
            stockMinNumericUpDown = new NumericUpDown();
            aceptarButton = new Button();
            cancelarButton = new Button();
            errorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)stockNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stockMinNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            //
            // idLabel
            //
            idLabel.AutoSize = true;
            idLabel.Location = new Point(22, 23);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(18, 15);
            idLabel.TabIndex = 0;
            idLabel.Text = "Id";
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
            // nombreLabel
            //
            nombreLabel.AutoSize = true;
            nombreLabel.Location = new Point(22, 58);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new Size(51, 15);
            nombreLabel.TabIndex = 2;
            nombreLabel.Text = "Nombre";
            //
            // nombreTextBox
            //
            nombreTextBox.Location = new Point(132, 55);
            nombreTextBox.MaxLength = 100;
            nombreTextBox.Name = "nombreTextBox";
            nombreTextBox.Size = new Size(290, 23);
            nombreTextBox.TabIndex = 3;
            //
            // descripcionLabel
            //
            descripcionLabel.AutoSize = true;
            descripcionLabel.Location = new Point(22, 93);
            descripcionLabel.Name = "descripcionLabel";
            descripcionLabel.Size = new Size(70, 15);
            descripcionLabel.TabIndex = 4;
            descripcionLabel.Text = "Descripción";
            //
            // descripcionTextBox
            //
            descripcionTextBox.Location = new Point(132, 90);
            descripcionTextBox.MaxLength = 255;
            descripcionTextBox.Multiline = true;
            descripcionTextBox.Name = "descripcionTextBox";
            descripcionTextBox.ScrollBars = ScrollBars.Vertical;
            descripcionTextBox.Size = new Size(290, 60);
            descripcionTextBox.TabIndex = 5;
            //
            // unidadLabel
            //
            unidadLabel.AutoSize = true;
            unidadLabel.Location = new Point(22, 165);
            unidadLabel.Name = "unidadLabel";
            unidadLabel.Size = new Size(45, 15);
            unidadLabel.TabIndex = 6;
            unidadLabel.Text = "Unidad";
            //
            // unidadComboBox
            //
            unidadComboBox.FormattingEnabled = true;
            unidadComboBox.Items.AddRange(new object[] { "unidad", "kg", "litro", "docena", "porción" });
            unidadComboBox.Location = new Point(132, 162);
            unidadComboBox.MaxLength = 20;
            unidadComboBox.Name = "unidadComboBox";
            unidadComboBox.Size = new Size(160, 23);
            unidadComboBox.TabIndex = 7;
            //
            // stockLabel
            //
            stockLabel.AutoSize = true;
            stockLabel.Location = new Point(22, 202);
            stockLabel.Name = "stockLabel";
            stockLabel.Size = new Size(37, 15);
            stockLabel.TabIndex = 8;
            stockLabel.Text = "Stock";
            //
            // stockNumericUpDown
            //
            stockNumericUpDown.Location = new Point(132, 200);
            stockNumericUpDown.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            stockNumericUpDown.Name = "stockNumericUpDown";
            stockNumericUpDown.Size = new Size(120, 23);
            stockNumericUpDown.TabIndex = 9;
            //
            // stockMinLabel
            //
            stockMinLabel.AutoSize = true;
            stockMinLabel.Location = new Point(22, 239);
            stockMinLabel.Name = "stockMinLabel";
            stockMinLabel.Size = new Size(75, 15);
            stockMinLabel.TabIndex = 10;
            stockMinLabel.Text = "Stock mínimo";
            //
            // stockMinNumericUpDown
            //
            stockMinNumericUpDown.Location = new Point(132, 237);
            stockMinNumericUpDown.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            stockMinNumericUpDown.Name = "stockMinNumericUpDown";
            stockMinNumericUpDown.Size = new Size(120, 23);
            stockMinNumericUpDown.TabIndex = 11;
            //
            // aceptarButton
            //
            aceptarButton.Location = new Point(302, 285);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(120, 32);
            aceptarButton.TabIndex = 12;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            //
            // cancelarButton
            //
            cancelarButton.Location = new Point(172, 285);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(120, 32);
            cancelarButton.TabIndex = 13;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            //
            // errorProvider
            //
            errorProvider.ContainerControl = this;
            //
            // IngredienteDetalle
            //
            AcceptButton = aceptarButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelarButton;
            ClientSize = new Size(460, 335);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(stockMinNumericUpDown);
            Controls.Add(stockMinLabel);
            Controls.Add(stockNumericUpDown);
            Controls.Add(stockLabel);
            Controls.Add(unidadComboBox);
            Controls.Add(unidadLabel);
            Controls.Add(descripcionTextBox);
            Controls.Add(descripcionLabel);
            Controls.Add(nombreTextBox);
            Controls.Add(nombreLabel);
            Controls.Add(idTextBox);
            Controls.Add(idLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IngredienteDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ingrediente";
            ((System.ComponentModel.ISupportInitialize)stockNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)stockMinNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label idLabel;
        private TextBox idTextBox;
        private Label nombreLabel;
        private TextBox nombreTextBox;
        private Label descripcionLabel;
        private TextBox descripcionTextBox;
        private Label unidadLabel;
        private ComboBox unidadComboBox;
        private Label stockLabel;
        private NumericUpDown stockNumericUpDown;
        private Label stockMinLabel;
        private NumericUpDown stockMinNumericUpDown;
        private Button aceptarButton;
        private Button cancelarButton;
        private ErrorProvider errorProvider;
    }
}
