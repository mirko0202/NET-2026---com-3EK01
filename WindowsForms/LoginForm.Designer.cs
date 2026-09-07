namespace WindowsForms
{
    partial class LoginForm
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
            tituloLabel = new Label();
            subtituloLabel = new Label();
            usuarioLabel = new Label();
            usuarioTextBox = new TextBox();
            passwordLabel = new Label();
            passwordTextBox = new TextBox();
            ingresarButton = new Button();
            cancelarButton = new Button();
            credencialesLabel = new Label();
            errorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            //
            // tituloLabel
            //
            tituloLabel.AutoSize = true;
            tituloLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            tituloLabel.Location = new Point(30, 25);
            tituloLabel.Name = "tituloLabel";
            tituloLabel.Size = new Size(228, 30);
            tituloLabel.TabIndex = 0;
            tituloLabel.Text = "Gestión de Stock";
            //
            // subtituloLabel
            //
            subtituloLabel.AutoSize = true;
            subtituloLabel.ForeColor = SystemColors.GrayText;
            subtituloLabel.Location = new Point(32, 60);
            subtituloLabel.Name = "subtituloLabel";
            subtituloLabel.Size = new Size(180, 15);
            subtituloLabel.TabIndex = 1;
            subtituloLabel.Text = "Ingrese sus credenciales para continuar";
            //
            // usuarioLabel
            //
            usuarioLabel.AutoSize = true;
            usuarioLabel.Location = new Point(32, 100);
            usuarioLabel.Name = "usuarioLabel";
            usuarioLabel.Size = new Size(49, 15);
            usuarioLabel.TabIndex = 2;
            usuarioLabel.Text = "Usuario";
            //
            // usuarioTextBox
            //
            usuarioTextBox.Location = new Point(32, 118);
            usuarioTextBox.MaxLength = 50;
            usuarioTextBox.Name = "usuarioTextBox";
            usuarioTextBox.Size = new Size(300, 23);
            usuarioTextBox.TabIndex = 3;
            //
            // passwordLabel
            //
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(32, 152);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(61, 15);
            passwordLabel.TabIndex = 4;
            passwordLabel.Text = "Contraseña";
            //
            // passwordTextBox
            //
            passwordTextBox.Location = new Point(32, 170);
            passwordTextBox.MaxLength = 100;
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(300, 23);
            passwordTextBox.TabIndex = 5;
            //
            // ingresarButton
            //
            ingresarButton.Location = new Point(202, 215);
            ingresarButton.Name = "ingresarButton";
            ingresarButton.Size = new Size(130, 32);
            ingresarButton.TabIndex = 6;
            ingresarButton.Text = "Iniciar sesión";
            ingresarButton.UseVisualStyleBackColor = true;
            ingresarButton.Click += ingresarButton_Click;
            //
            // cancelarButton
            //
            cancelarButton.Location = new Point(62, 215);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(130, 32);
            cancelarButton.TabIndex = 7;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            //
            // credencialesLabel
            //
            credencialesLabel.AutoSize = true;
            credencialesLabel.ForeColor = SystemColors.GrayText;
            credencialesLabel.Location = new Point(32, 260);
            credencialesLabel.Name = "credencialesLabel";
            credencialesLabel.Size = new Size(300, 15);
            credencialesLabel.TabIndex = 8;
            credencialesLabel.Text = "Datos de prueba:  admin / admin123   ·   empleado / empleado123";
            //
            // errorProvider
            //
            errorProvider.ContainerControl = this;
            //
            // LoginForm
            //
            AcceptButton = ingresarButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelarButton;
            ClientSize = new Size(384, 291);
            Controls.Add(credencialesLabel);
            Controls.Add(cancelarButton);
            Controls.Add(ingresarButton);
            Controls.Add(passwordTextBox);
            Controls.Add(passwordLabel);
            Controls.Add(usuarioTextBox);
            Controls.Add(usuarioLabel);
            Controls.Add(subtituloLabel);
            Controls.Add(tituloLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Iniciar sesión";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label tituloLabel;
        private Label subtituloLabel;
        private Label usuarioLabel;
        private TextBox usuarioTextBox;
        private Label passwordLabel;
        private TextBox passwordTextBox;
        private Button ingresarButton;
        private Button cancelarButton;
        private Label credencialesLabel;
        private ErrorProvider errorProvider;
    }
}
