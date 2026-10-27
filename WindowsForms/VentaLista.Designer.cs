namespace WindowsForms
{
    partial class VentaLista
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
            ventasDataGridView = new DataGridView();
            detalleTituloLabel = new Label();
            detallesDataGridView = new DataGridView();
            cerrarButton = new Button();
            resultadosLabel = new Label();
            agregarButton = new Button();
            actualizarButton = new Button();
            eliminarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)ventasDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)detallesDataGridView).BeginInit();
            SuspendLayout();
            //
            // tituloLabel
            //
            tituloLabel.AutoSize = true;
            tituloLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            tituloLabel.Location = new Point(14, 14);
            tituloLabel.Name = "tituloLabel";
            tituloLabel.Size = new Size(60, 21);
            tituloLabel.TabIndex = 0;
            tituloLabel.Text = "Ventas";
            //
            // ventasDataGridView
            //
            ventasDataGridView.AllowUserToAddRows = false;
            ventasDataGridView.AllowUserToDeleteRows = false;
            ventasDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ventasDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ventasDataGridView.Location = new Point(14, 44);
            ventasDataGridView.MultiSelect = false;
            ventasDataGridView.Name = "ventasDataGridView";
            ventasDataGridView.ReadOnly = true;
            ventasDataGridView.RowHeadersVisible = false;
            ventasDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ventasDataGridView.Size = new Size(952, 270);
            ventasDataGridView.TabIndex = 1;
            ventasDataGridView.SelectionChanged += ventasDataGridView_SelectionChanged;
            //
            // detalleTituloLabel
            //
            detalleTituloLabel.AutoSize = true;
            detalleTituloLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            detalleTituloLabel.Location = new Point(14, 328);
            detalleTituloLabel.Name = "detalleTituloLabel";
            detalleTituloLabel.Size = new Size(240, 17);
            detalleTituloLabel.TabIndex = 2;
            detalleTituloLabel.Text = "Productos de la venta seleccionada";
            //
            // detallesDataGridView
            //
            detallesDataGridView.AllowUserToAddRows = false;
            detallesDataGridView.AllowUserToDeleteRows = false;
            detallesDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            detallesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            detallesDataGridView.Location = new Point(14, 352);
            detallesDataGridView.MultiSelect = false;
            detallesDataGridView.Name = "detallesDataGridView";
            detallesDataGridView.ReadOnly = true;
            detallesDataGridView.RowHeadersVisible = false;
            detallesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            detallesDataGridView.Size = new Size(952, 222);
            detallesDataGridView.TabIndex = 3;
            detallesDataGridView.TabStop = false;
            //
            // cerrarButton
            //
            cerrarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cerrarButton.Location = new Point(14, 592);
            cerrarButton.Name = "cerrarButton";
            cerrarButton.Size = new Size(120, 32);
            cerrarButton.TabIndex = 7;
            cerrarButton.Text = "Cerrar";
            cerrarButton.UseVisualStyleBackColor = true;
            cerrarButton.Click += cerrarButton_Click;
            //
            // resultadosLabel
            //
            resultadosLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            resultadosLabel.AutoSize = true;
            resultadosLabel.ForeColor = SystemColors.GrayText;
            resultadosLabel.Location = new Point(148, 601);
            resultadosLabel.Name = "resultadosLabel";
            resultadosLabel.Size = new Size(0, 15);
            resultadosLabel.TabIndex = 8;
            //
            // agregarButton
            //
            agregarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            agregarButton.Location = new Point(594, 592);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(120, 32);
            agregarButton.TabIndex = 4;
            agregarButton.Text = "Nueva venta";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            //
            // actualizarButton
            //
            actualizarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            actualizarButton.Enabled = false;
            actualizarButton.Location = new Point(720, 592);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(120, 32);
            actualizarButton.TabIndex = 5;
            actualizarButton.Text = "Modificar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            //
            // eliminarButton
            //
            eliminarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            eliminarButton.Enabled = false;
            eliminarButton.Location = new Point(846, 592);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(120, 32);
            eliminarButton.TabIndex = 6;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            //
            // VentaLista
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 640);
            Controls.Add(eliminarButton);
            Controls.Add(actualizarButton);
            Controls.Add(agregarButton);
            Controls.Add(resultadosLabel);
            Controls.Add(cerrarButton);
            Controls.Add(detallesDataGridView);
            Controls.Add(detalleTituloLabel);
            Controls.Add(ventasDataGridView);
            Controls.Add(tituloLabel);
            MinimumSize = new Size(820, 560);
            Name = "VentaLista";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ventas";
            Load += VentaLista_Load;
            ((System.ComponentModel.ISupportInitialize)ventasDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)detallesDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label tituloLabel;
        private DataGridView ventasDataGridView;
        private Label detalleTituloLabel;
        private DataGridView detallesDataGridView;
        private Button cerrarButton;
        private Label resultadosLabel;
        private Button agregarButton;
        private Button actualizarButton;
        private Button eliminarButton;
    }
}
