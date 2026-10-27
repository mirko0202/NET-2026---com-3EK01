namespace WindowsForms
{
    partial class SucursalDetalle
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
            direccionLabel = new Label();
            direccionTextBox = new TextBox();
            telefonoLabel = new Label();
            telefonoTextBox = new TextBox();
            activaCheckBox = new CheckBox();
            aceptarButton = new Button();
            cancelarButton = new Button();
            errorProvider = new ErrorProvider(components);
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
            idTextBox.Location = new Point(112, 20);
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
            nombreTextBox.Location = new Point(112, 55);
            nombreTextBox.MaxLength = 100;
            nombreTextBox.Name = "nombreTextBox";
            nombreTextBox.Size = new Size(310, 23);
            nombreTextBox.TabIndex = 3;
            //
            // direccionLabel
            //
            direccionLabel.AutoSize = true;
            direccionLabel.Location = new Point(22, 93);
            direccionLabel.Name = "direccionLabel";
            direccionLabel.Size = new Size(57, 15);
            direccionLabel.TabIndex = 4;
            direccionLabel.Text = "Dirección";
            //
            // direccionTextBox
            //
            direccionTextBox.Location = new Point(112, 90);
            direccionTextBox.MaxLength = 150;
            direccionTextBox.Name = "direccionTextBox";
            direccionTextBox.Size = new Size(310, 23);
            direccionTextBox.TabIndex = 5;
            //
            // telefonoLabel
            //
            telefonoLabel.AutoSize = true;
            telefonoLabel.Location = new Point(22, 128);
            telefonoLabel.Name = "telefonoLabel";
            telefonoLabel.Size = new Size(53, 15);
            telefonoLabel.TabIndex = 6;
            telefonoLabel.Text = "Teléfono";
            //
            // telefonoTextBox
            //
            telefonoTextBox.Location = new Point(112, 125);
            telefonoTextBox.MaxLength = 30;
            telefonoTextBox.Name = "telefonoTextBox";
            telefonoTextBox.Size = new Size(180, 23);
            telefonoTextBox.TabIndex = 7;
            //
            // activaCheckBox
            //
            activaCheckBox.AutoSize = true;
            activaCheckBox.Location = new Point(112, 162);
            activaCheckBox.Name = "activaCheckBox";
            activaCheckBox.Size = new Size(285, 19);
            activaCheckBox.TabIndex = 8;
            activaCheckBox.Text = "Sucursal activa (habilitada para ventas e ingresos)";
            activaCheckBox.UseVisualStyleBackColor = true;
            //
            // aceptarButton
            //
            aceptarButton.Location = new Point(302, 205);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(120, 32);
            aceptarButton.TabIndex = 9;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            //
            // cancelarButton
            //
            cancelarButton.Location = new Point(172, 205);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(120, 32);
            cancelarButton.TabIndex = 10;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            //
            // errorProvider
            //
            errorProvider.ContainerControl = this;
            //
            // SucursalDetalle
            //
            AcceptButton = aceptarButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelarButton;
            ClientSize = new Size(460, 255);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(activaCheckBox);
            Controls.Add(telefonoTextBox);
            Controls.Add(telefonoLabel);
            Controls.Add(direccionTextBox);
            Controls.Add(direccionLabel);
            Controls.Add(nombreTextBox);
            Controls.Add(nombreLabel);
            Controls.Add(idTextBox);
            Controls.Add(idLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SucursalDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sucursal";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label idLabel;
        private TextBox idTextBox;
        private Label nombreLabel;
        private TextBox nombreTextBox;
        private Label direccionLabel;
        private TextBox direccionTextBox;
        private Label telefonoLabel;
        private TextBox telefonoTextBox;
        private CheckBox activaCheckBox;
        private Button aceptarButton;
        private Button cancelarButton;
        private ErrorProvider errorProvider;
    }
}
