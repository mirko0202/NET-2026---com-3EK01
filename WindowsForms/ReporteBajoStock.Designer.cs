namespace WindowsForms
{
    partial class ReporteBajoStock
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
            resumenLabel = new Label();
            itemsDataGridView = new DataGridView();
            actualizarButton = new Button();
            exportarButton = new Button();
            cerrarButton = new Button();
            notaLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)itemsDataGridView).BeginInit();
            SuspendLayout();
            //
            // tituloLabel
            //
            tituloLabel.AutoSize = true;
            tituloLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            tituloLabel.Location = new Point(14, 14);
            tituloLabel.Name = "tituloLabel";
            tituloLabel.Size = new Size(420, 21);
            tituloLabel.TabIndex = 0;
            tituloLabel.Text = "Productos e ingredientes con stock igual o inferior al mínimo";
            //
            // resumenLabel
            //
            resumenLabel.AutoSize = true;
            resumenLabel.Font = new Font("Segoe UI", 10F);
            resumenLabel.Location = new Point(15, 42);
            resumenLabel.Name = "resumenLabel";
            resumenLabel.Size = new Size(0, 19);
            resumenLabel.TabIndex = 1;
            //
            // itemsDataGridView
            //
            itemsDataGridView.AllowUserToAddRows = false;
            itemsDataGridView.AllowUserToDeleteRows = false;
            itemsDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            itemsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            itemsDataGridView.Location = new Point(14, 72);
            itemsDataGridView.MultiSelect = false;
            itemsDataGridView.Name = "itemsDataGridView";
            itemsDataGridView.ReadOnly = true;
            itemsDataGridView.RowHeadersVisible = false;
            itemsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            itemsDataGridView.Size = new Size(772, 330);
            itemsDataGridView.TabIndex = 2;
            //
            // actualizarButton
            //
            actualizarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            actualizarButton.Location = new Point(530, 440);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(120, 32);
            actualizarButton.TabIndex = 3;
            actualizarButton.Text = "Actualizar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            //
            // exportarButton
            //
            exportarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            exportarButton.Enabled = false;
            exportarButton.Location = new Point(656, 440);
            exportarButton.Name = "exportarButton";
            exportarButton.Size = new Size(130, 32);
            exportarButton.TabIndex = 4;
            exportarButton.Text = "Exportar PDF";
            exportarButton.UseVisualStyleBackColor = true;
            exportarButton.Click += exportarButton_Click;
            //
            // cerrarButton
            //
            cerrarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cerrarButton.Location = new Point(14, 440);
            cerrarButton.Name = "cerrarButton";
            cerrarButton.Size = new Size(120, 32);
            cerrarButton.TabIndex = 5;
            cerrarButton.Text = "Cerrar";
            cerrarButton.UseVisualStyleBackColor = true;
            cerrarButton.Click += cerrarButton_Click;
            //
            // notaLabel
            //
            notaLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            notaLabel.AutoSize = true;
            notaLabel.ForeColor = SystemColors.GrayText;
            notaLabel.Location = new Point(14, 410);
            notaLabel.Name = "notaLabel";
            notaLabel.Size = new Size(330, 15);
            notaLabel.TabIndex = 6;
            notaLabel.Text = "Faltante: unidades necesarias para volver al stock mínimo.";
            //
            // ReporteBajoStock
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 486);
            Controls.Add(notaLabel);
            Controls.Add(cerrarButton);
            Controls.Add(exportarButton);
            Controls.Add(actualizarButton);
            Controls.Add(itemsDataGridView);
            Controls.Add(resumenLabel);
            Controls.Add(tituloLabel);
            MinimumSize = new Size(720, 420);
            Name = "ReporteBajoStock";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Reporte de stock bajo el mínimo";
            Load += ReporteBajoStock_Load;
            ((System.ComponentModel.ISupportInitialize)itemsDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label tituloLabel;
        private Label resumenLabel;
        private DataGridView itemsDataGridView;
        private Button actualizarButton;
        private Button exportarButton;
        private Button cerrarButton;
        private Label notaLabel;
    }
}
