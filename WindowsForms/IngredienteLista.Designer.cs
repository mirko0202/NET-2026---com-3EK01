namespace WindowsForms
{
    partial class IngredienteLista
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
            ingredientesDataGridView = new DataGridView();
            agregarButton = new Button();
            actualizarButton = new Button();
            eliminarButton = new Button();
            cerrarButton = new Button();
            resultadosLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)ingredientesDataGridView).BeginInit();
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
            tituloLabel.Text = "Ingredientes";
            //
            // ingredientesDataGridView
            //
            ingredientesDataGridView.AllowUserToAddRows = false;
            ingredientesDataGridView.AllowUserToDeleteRows = false;
            ingredientesDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ingredientesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ingredientesDataGridView.Location = new Point(14, 50);
            ingredientesDataGridView.MultiSelect = false;
            ingredientesDataGridView.Name = "ingredientesDataGridView";
            ingredientesDataGridView.ReadOnly = true;
            ingredientesDataGridView.RowHeadersVisible = false;
            ingredientesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ingredientesDataGridView.Size = new Size(872, 422);
            ingredientesDataGridView.TabIndex = 1;
            //
            // agregarButton
            //
            agregarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            agregarButton.Location = new Point(486, 490);
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
            actualizarButton.Location = new Point(612, 490);
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
            eliminarButton.Location = new Point(738, 490);
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
            // IngredienteLista
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 540);
            Controls.Add(resultadosLabel);
            Controls.Add(cerrarButton);
            Controls.Add(eliminarButton);
            Controls.Add(actualizarButton);
            Controls.Add(agregarButton);
            Controls.Add(ingredientesDataGridView);
            Controls.Add(tituloLabel);
            MinimumSize = new Size(760, 480);
            Name = "IngredienteLista";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ingredientes";
            Load += IngredienteLista_Load;
            ((System.ComponentModel.ISupportInitialize)ingredientesDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label tituloLabel;
        private DataGridView ingredientesDataGridView;
        private Button agregarButton;
        private Button actualizarButton;
        private Button eliminarButton;
        private Button cerrarButton;
        private Label resultadosLabel;
    }
}
