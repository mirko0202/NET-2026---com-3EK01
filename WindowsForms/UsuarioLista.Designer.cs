namespace WindowsForms
{
    partial class UsuarioLista
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
            tituloLabel = new Label();
            usuariosDataGridView = new DataGridView();
            agregarButton = new Button();
            actualizarButton = new Button();
            eliminarButton = new Button();
            cerrarButton = new Button();
            resultadosLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)usuariosDataGridView).BeginInit();
            SuspendLayout();
            //
            // tituloLabel
            //
            tituloLabel.AutoSize = true;
            tituloLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            tituloLabel.Location = new Point(14, 16);
            tituloLabel.Name = "tituloLabel";
            tituloLabel.Size = new Size(200, 21);
            tituloLabel.TabIndex = 0;
            tituloLabel.Text = "Usuarios del sistema";
            //
            // usuariosDataGridView
            //
            usuariosDataGridView.AllowUserToAddRows = false;
            usuariosDataGridView.AllowUserToDeleteRows = false;
            usuariosDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            usuariosDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            usuariosDataGridView.Location = new Point(14, 50);
            usuariosDataGridView.MultiSelect = false;
            usuariosDataGridView.Name = "usuariosDataGridView";
            usuariosDataGridView.ReadOnly = true;
            usuariosDataGridView.RowHeadersVisible = false;
            usuariosDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            usuariosDataGridView.Size = new Size(772, 362);
            usuariosDataGridView.TabIndex = 1;
            usuariosDataGridView.CellDoubleClick += usuariosDataGridView_CellDoubleClick;
            //
            // agregarButton
            //
            agregarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            agregarButton.Location = new Point(386, 430);
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
            actualizarButton.Location = new Point(512, 430);
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
            eliminarButton.Location = new Point(638, 430);
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
            cerrarButton.Location = new Point(14, 430);
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
            resultadosLabel.Location = new Point(148, 439);
            resultadosLabel.Name = "resultadosLabel";
            resultadosLabel.Size = new Size(0, 15);
            resultadosLabel.TabIndex = 6;
            //
            // UsuarioLista
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 480);
            Controls.Add(resultadosLabel);
            Controls.Add(cerrarButton);
            Controls.Add(eliminarButton);
            Controls.Add(actualizarButton);
            Controls.Add(agregarButton);
            Controls.Add(usuariosDataGridView);
            Controls.Add(tituloLabel);
            MinimumSize = new Size(700, 400);
            Name = "UsuarioLista";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Usuarios";
            Load += UsuarioLista_Load;
            ((System.ComponentModel.ISupportInitialize)usuariosDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label tituloLabel;
        private DataGridView usuariosDataGridView;
        private Button agregarButton;
        private Button actualizarButton;
        private Button eliminarButton;
        private Button cerrarButton;
        private Label resultadosLabel;
    }
}
