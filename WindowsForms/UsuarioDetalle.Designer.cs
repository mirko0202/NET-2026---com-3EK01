namespace WindowsForms
{
    partial class UsuarioDetalle
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
            usernameLabel = new Label();
            usernameTextBox = new TextBox();
            nombreCompletoLabel = new Label();
            nombreCompletoTextBox = new TextBox();
            rolLabel = new Label();
            rolComboBox = new ComboBox();
            sucursalLabel = new Label();
            sucursalComboBox = new ComboBox();
            activoCheckBox = new CheckBox();
            passwordGroupBox = new GroupBox();
            passwordLabel = new Label();
            passwordTextBox = new TextBox();
            confirmarLabel = new Label();
            confirmarTextBox = new TextBox();
            passwordAyudaLabel = new Label();
            aceptarButton = new Button();
            cancelarButton = new Button();
            errorProvider = new ErrorProvider(components);
            passwordGroupBox.SuspendLayout();
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
            idTextBox.Location = new Point(142, 20);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(80, 23);
            idTextBox.TabIndex = 1;
            idTextBox.TabStop = false;
            //
            // usernameLabel
            //
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(22, 58);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(47, 15);
            usernameLabel.TabIndex = 2;
            usernameLabel.Text = "Usuario";
            //
            // usernameTextBox
            //
            usernameTextBox.Location = new Point(142, 55);
            usernameTextBox.MaxLength = 50;
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(180, 23);
            usernameTextBox.TabIndex = 3;
            //
            // nombreCompletoLabel
            //
            nombreCompletoLabel.AutoSize = true;
            nombreCompletoLabel.Location = new Point(22, 93);
            nombreCompletoLabel.Name = "nombreCompletoLabel";
            nombreCompletoLabel.Size = new Size(102, 15);
            nombreCompletoLabel.TabIndex = 4;
            nombreCompletoLabel.Text = "Nombre completo";
            //
            // nombreCompletoTextBox
            //
            nombreCompletoTextBox.Location = new Point(142, 90);
            nombreCompletoTextBox.MaxLength = 100;
            nombreCompletoTextBox.Name = "nombreCompletoTextBox";
            nombreCompletoTextBox.Size = new Size(290, 23);
            nombreCompletoTextBox.TabIndex = 5;
            //
            // rolLabel
            //
            rolLabel.AutoSize = true;
            rolLabel.Location = new Point(22, 128);
            rolLabel.Name = "rolLabel";
            rolLabel.Size = new Size(24, 15);
            rolLabel.TabIndex = 6;
            rolLabel.Text = "Rol";
            //
            // rolComboBox
            //
            rolComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            rolComboBox.FormattingEnabled = true;
            rolComboBox.Items.AddRange(new object[] { "Administrador", "Empleado" });
            rolComboBox.Location = new Point(142, 125);
            rolComboBox.Name = "rolComboBox";
            rolComboBox.Size = new Size(180, 23);
            rolComboBox.TabIndex = 7;
            //
            // sucursalLabel
            //
            sucursalLabel.AutoSize = true;
            sucursalLabel.Location = new Point(22, 163);
            sucursalLabel.Name = "sucursalLabel";
            sucursalLabel.Size = new Size(51, 15);
            sucursalLabel.TabIndex = 8;
            sucursalLabel.Text = "Sucursal";
            //
            // sucursalComboBox
            //
            sucursalComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sucursalComboBox.FormattingEnabled = true;
            sucursalComboBox.Location = new Point(142, 160);
            sucursalComboBox.Name = "sucursalComboBox";
            sucursalComboBox.Size = new Size(230, 23);
            sucursalComboBox.TabIndex = 9;
            //
            // activoCheckBox
            //
            activoCheckBox.AutoSize = true;
            activoCheckBox.Location = new Point(142, 196);
            activoCheckBox.Name = "activoCheckBox";
            activoCheckBox.Size = new Size(220, 19);
            activoCheckBox.TabIndex = 10;
            activoCheckBox.Text = "Usuario activo (puede iniciar sesión)";
            activoCheckBox.UseVisualStyleBackColor = true;
            //
            // passwordGroupBox
            //
            passwordGroupBox.Controls.Add(passwordLabel);
            passwordGroupBox.Controls.Add(passwordTextBox);
            passwordGroupBox.Controls.Add(confirmarLabel);
            passwordGroupBox.Controls.Add(confirmarTextBox);
            passwordGroupBox.Controls.Add(passwordAyudaLabel);
            passwordGroupBox.Location = new Point(22, 228);
            passwordGroupBox.Name = "passwordGroupBox";
            passwordGroupBox.Size = new Size(430, 118);
            passwordGroupBox.TabIndex = 11;
            passwordGroupBox.TabStop = false;
            passwordGroupBox.Text = "Contraseña";
            //
            // passwordLabel
            //
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(14, 29);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(67, 15);
            passwordLabel.TabIndex = 0;
            passwordLabel.Text = "Contraseña";
            //
            // passwordTextBox
            //
            passwordTextBox.Location = new Point(120, 26);
            passwordTextBox.MaxLength = 100;
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(180, 23);
            passwordTextBox.TabIndex = 1;
            passwordTextBox.UseSystemPasswordChar = true;
            //
            // confirmarLabel
            //
            confirmarLabel.AutoSize = true;
            confirmarLabel.Location = new Point(14, 62);
            confirmarLabel.Name = "confirmarLabel";
            confirmarLabel.Size = new Size(61, 15);
            confirmarLabel.TabIndex = 2;
            confirmarLabel.Text = "Confirmar";
            //
            // confirmarTextBox
            //
            confirmarTextBox.Location = new Point(120, 59);
            confirmarTextBox.MaxLength = 100;
            confirmarTextBox.Name = "confirmarTextBox";
            confirmarTextBox.Size = new Size(180, 23);
            confirmarTextBox.TabIndex = 3;
            confirmarTextBox.UseSystemPasswordChar = true;
            //
            // passwordAyudaLabel
            //
            passwordAyudaLabel.AutoSize = true;
            passwordAyudaLabel.ForeColor = SystemColors.GrayText;
            passwordAyudaLabel.Location = new Point(14, 92);
            passwordAyudaLabel.Name = "passwordAyudaLabel";
            passwordAyudaLabel.Size = new Size(160, 15);
            passwordAyudaLabel.TabIndex = 4;
            passwordAyudaLabel.Text = "Mínimo 6 caracteres.";
            //
            // aceptarButton
            //
            aceptarButton.Location = new Point(332, 362);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(120, 32);
            aceptarButton.TabIndex = 12;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            //
            // cancelarButton
            //
            cancelarButton.Location = new Point(202, 362);
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
            // UsuarioDetalle
            //
            AcceptButton = aceptarButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelarButton;
            ClientSize = new Size(484, 412);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(passwordGroupBox);
            Controls.Add(activoCheckBox);
            Controls.Add(sucursalComboBox);
            Controls.Add(sucursalLabel);
            Controls.Add(rolComboBox);
            Controls.Add(rolLabel);
            Controls.Add(nombreCompletoTextBox);
            Controls.Add(nombreCompletoLabel);
            Controls.Add(usernameTextBox);
            Controls.Add(usernameLabel);
            Controls.Add(idTextBox);
            Controls.Add(idLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UsuarioDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Usuario";
            Load += UsuarioDetalle_Load;
            passwordGroupBox.ResumeLayout(false);
            passwordGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label idLabel;
        private TextBox idTextBox;
        private Label usernameLabel;
        private TextBox usernameTextBox;
        private Label nombreCompletoLabel;
        private TextBox nombreCompletoTextBox;
        private Label rolLabel;
        private ComboBox rolComboBox;
        private Label sucursalLabel;
        private ComboBox sucursalComboBox;
        private CheckBox activoCheckBox;
        private GroupBox passwordGroupBox;
        private Label passwordLabel;
        private TextBox passwordTextBox;
        private Label confirmarLabel;
        private TextBox confirmarTextBox;
        private Label passwordAyudaLabel;
        private Button aceptarButton;
        private Button cancelarButton;
        private ErrorProvider errorProvider;
    }
}
