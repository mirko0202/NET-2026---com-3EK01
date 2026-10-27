namespace WindowsForms
{
    partial class ReporteVentas
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
            filtrosGroupBox = new GroupBox();
            desdeLabel = new Label();
            desdeDateTimePicker = new DateTimePicker();
            hastaLabel = new Label();
            hastaDateTimePicker = new DateTimePicker();
            sucursalLabel = new Label();
            sucursalComboBox = new ComboBox();
            generarButton = new Button();
            exportarButton = new Button();
            indicadoresTableLayoutPanel = new TableLayoutPanel();
            ventasTituloLabel = new Label();
            unidadesTituloLabel = new Label();
            totalTituloLabel = new Label();
            ticketTituloLabel = new Label();
            ventasValorLabel = new Label();
            unidadesValorLabel = new Label();
            totalValorLabel = new Label();
            ticketValorLabel = new Label();
            graficoPictureBox = new PictureBox();
            detalleTableLayoutPanel = new TableLayoutPanel();
            porSucursalLabel = new Label();
            productosLabel = new Label();
            porSucursalDataGridView = new DataGridView();
            productosDataGridView = new DataGridView();
            cerrarButton = new Button();
            generadoLabel = new Label();
            filtrosGroupBox.SuspendLayout();
            indicadoresTableLayoutPanel.SuspendLayout();
            detalleTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)graficoPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)porSucursalDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)productosDataGridView).BeginInit();
            SuspendLayout();
            //
            // filtrosGroupBox
            //
            filtrosGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filtrosGroupBox.Controls.Add(desdeLabel);
            filtrosGroupBox.Controls.Add(desdeDateTimePicker);
            filtrosGroupBox.Controls.Add(hastaLabel);
            filtrosGroupBox.Controls.Add(hastaDateTimePicker);
            filtrosGroupBox.Controls.Add(sucursalLabel);
            filtrosGroupBox.Controls.Add(sucursalComboBox);
            filtrosGroupBox.Controls.Add(generarButton);
            filtrosGroupBox.Controls.Add(exportarButton);
            filtrosGroupBox.Location = new Point(14, 12);
            filtrosGroupBox.Name = "filtrosGroupBox";
            filtrosGroupBox.Size = new Size(1072, 70);
            filtrosGroupBox.TabIndex = 0;
            filtrosGroupBox.TabStop = false;
            filtrosGroupBox.Text = "Período y sucursal";
            //
            // desdeLabel
            //
            desdeLabel.AutoSize = true;
            desdeLabel.Location = new Point(16, 31);
            desdeLabel.Name = "desdeLabel";
            desdeLabel.Size = new Size(42, 15);
            desdeLabel.TabIndex = 0;
            desdeLabel.Text = "Desde:";
            //
            // desdeDateTimePicker
            //
            desdeDateTimePicker.CustomFormat = "dd/MM/yyyy";
            desdeDateTimePicker.Format = DateTimePickerFormat.Custom;
            desdeDateTimePicker.Location = new Point(64, 28);
            desdeDateTimePicker.Name = "desdeDateTimePicker";
            desdeDateTimePicker.Size = new Size(120, 23);
            desdeDateTimePicker.TabIndex = 1;
            //
            // hastaLabel
            //
            hastaLabel.AutoSize = true;
            hastaLabel.Location = new Point(204, 31);
            hastaLabel.Name = "hastaLabel";
            hastaLabel.Size = new Size(40, 15);
            hastaLabel.TabIndex = 2;
            hastaLabel.Text = "Hasta:";
            //
            // hastaDateTimePicker
            //
            hastaDateTimePicker.CustomFormat = "dd/MM/yyyy";
            hastaDateTimePicker.Format = DateTimePickerFormat.Custom;
            hastaDateTimePicker.Location = new Point(250, 28);
            hastaDateTimePicker.Name = "hastaDateTimePicker";
            hastaDateTimePicker.Size = new Size(120, 23);
            hastaDateTimePicker.TabIndex = 3;
            //
            // sucursalLabel
            //
            sucursalLabel.AutoSize = true;
            sucursalLabel.Location = new Point(392, 31);
            sucursalLabel.Name = "sucursalLabel";
            sucursalLabel.Size = new Size(54, 15);
            sucursalLabel.TabIndex = 4;
            sucursalLabel.Text = "Sucursal:";
            //
            // sucursalComboBox
            //
            sucursalComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sucursalComboBox.FormattingEnabled = true;
            sucursalComboBox.Location = new Point(452, 28);
            sucursalComboBox.Name = "sucursalComboBox";
            sucursalComboBox.Size = new Size(200, 23);
            sucursalComboBox.TabIndex = 5;
            //
            // generarButton
            //
            generarButton.Location = new Point(680, 26);
            generarButton.Name = "generarButton";
            generarButton.Size = new Size(120, 28);
            generarButton.TabIndex = 6;
            generarButton.Text = "Generar";
            generarButton.UseVisualStyleBackColor = true;
            generarButton.Click += generarButton_Click;
            //
            // exportarButton
            //
            exportarButton.Enabled = false;
            exportarButton.Location = new Point(810, 26);
            exportarButton.Name = "exportarButton";
            exportarButton.Size = new Size(130, 28);
            exportarButton.TabIndex = 7;
            exportarButton.Text = "Exportar PDF";
            exportarButton.UseVisualStyleBackColor = true;
            exportarButton.Click += exportarButton_Click;
            //
            // indicadoresTableLayoutPanel
            //
            indicadoresTableLayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            indicadoresTableLayoutPanel.BackColor = Color.White;
            indicadoresTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            indicadoresTableLayoutPanel.ColumnCount = 4;
            indicadoresTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            indicadoresTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            indicadoresTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            indicadoresTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            indicadoresTableLayoutPanel.Controls.Add(ventasTituloLabel, 0, 0);
            indicadoresTableLayoutPanel.Controls.Add(unidadesTituloLabel, 1, 0);
            indicadoresTableLayoutPanel.Controls.Add(totalTituloLabel, 2, 0);
            indicadoresTableLayoutPanel.Controls.Add(ticketTituloLabel, 3, 0);
            indicadoresTableLayoutPanel.Controls.Add(ventasValorLabel, 0, 1);
            indicadoresTableLayoutPanel.Controls.Add(unidadesValorLabel, 1, 1);
            indicadoresTableLayoutPanel.Controls.Add(totalValorLabel, 2, 1);
            indicadoresTableLayoutPanel.Controls.Add(ticketValorLabel, 3, 1);
            indicadoresTableLayoutPanel.Location = new Point(14, 92);
            indicadoresTableLayoutPanel.Name = "indicadoresTableLayoutPanel";
            indicadoresTableLayoutPanel.RowCount = 2;
            indicadoresTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            indicadoresTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            indicadoresTableLayoutPanel.Size = new Size(1072, 63);
            indicadoresTableLayoutPanel.TabIndex = 1;
            //
            // ventasTituloLabel
            //
            ventasTituloLabel.Dock = DockStyle.Fill;
            ventasTituloLabel.ForeColor = SystemColors.GrayText;
            ventasTituloLabel.Name = "ventasTituloLabel";
            ventasTituloLabel.Padding = new Padding(6, 4, 0, 0);
            ventasTituloLabel.TabIndex = 0;
            ventasTituloLabel.Text = "Ventas";
            //
            // unidadesTituloLabel
            //
            unidadesTituloLabel.Dock = DockStyle.Fill;
            unidadesTituloLabel.ForeColor = SystemColors.GrayText;
            unidadesTituloLabel.Name = "unidadesTituloLabel";
            unidadesTituloLabel.Padding = new Padding(6, 4, 0, 0);
            unidadesTituloLabel.TabIndex = 1;
            unidadesTituloLabel.Text = "Unidades vendidas";
            //
            // totalTituloLabel
            //
            totalTituloLabel.Dock = DockStyle.Fill;
            totalTituloLabel.ForeColor = SystemColors.GrayText;
            totalTituloLabel.Name = "totalTituloLabel";
            totalTituloLabel.Padding = new Padding(6, 4, 0, 0);
            totalTituloLabel.TabIndex = 2;
            totalTituloLabel.Text = "Total facturado";
            //
            // ticketTituloLabel
            //
            ticketTituloLabel.Dock = DockStyle.Fill;
            ticketTituloLabel.ForeColor = SystemColors.GrayText;
            ticketTituloLabel.Name = "ticketTituloLabel";
            ticketTituloLabel.Padding = new Padding(6, 4, 0, 0);
            ticketTituloLabel.TabIndex = 3;
            ticketTituloLabel.Text = "Ticket promedio";
            //
            // ventasValorLabel
            //
            ventasValorLabel.Dock = DockStyle.Fill;
            ventasValorLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            ventasValorLabel.Name = "ventasValorLabel";
            ventasValorLabel.Padding = new Padding(4, 0, 0, 0);
            ventasValorLabel.TabIndex = 4;
            ventasValorLabel.Text = "-";
            //
            // unidadesValorLabel
            //
            unidadesValorLabel.Dock = DockStyle.Fill;
            unidadesValorLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            unidadesValorLabel.Name = "unidadesValorLabel";
            unidadesValorLabel.Padding = new Padding(4, 0, 0, 0);
            unidadesValorLabel.TabIndex = 5;
            unidadesValorLabel.Text = "-";
            //
            // totalValorLabel
            //
            totalValorLabel.Dock = DockStyle.Fill;
            totalValorLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            totalValorLabel.Name = "totalValorLabel";
            totalValorLabel.Padding = new Padding(4, 0, 0, 0);
            totalValorLabel.TabIndex = 6;
            totalValorLabel.Text = "-";
            //
            // ticketValorLabel
            //
            ticketValorLabel.Dock = DockStyle.Fill;
            ticketValorLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            ticketValorLabel.Name = "ticketValorLabel";
            ticketValorLabel.Padding = new Padding(4, 0, 0, 0);
            ticketValorLabel.TabIndex = 7;
            ticketValorLabel.Text = "-";
            //
            // graficoPictureBox
            //
            graficoPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            graficoPictureBox.BackColor = Color.White;
            graficoPictureBox.BorderStyle = BorderStyle.FixedSingle;
            graficoPictureBox.Location = new Point(14, 165);
            graficoPictureBox.Name = "graficoPictureBox";
            graficoPictureBox.Size = new Size(1072, 300);
            graficoPictureBox.TabIndex = 2;
            graficoPictureBox.TabStop = false;
            graficoPictureBox.SizeChanged += graficoPictureBox_SizeChanged;
            //
            // detalleTableLayoutPanel
            //
            detalleTableLayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            detalleTableLayoutPanel.ColumnCount = 2;
            detalleTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            detalleTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            detalleTableLayoutPanel.Controls.Add(porSucursalLabel, 0, 0);
            detalleTableLayoutPanel.Controls.Add(productosLabel, 1, 0);
            detalleTableLayoutPanel.Controls.Add(porSucursalDataGridView, 0, 1);
            detalleTableLayoutPanel.Controls.Add(productosDataGridView, 1, 1);
            detalleTableLayoutPanel.Location = new Point(14, 475);
            detalleTableLayoutPanel.Name = "detalleTableLayoutPanel";
            detalleTableLayoutPanel.RowCount = 2;
            detalleTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            detalleTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            detalleTableLayoutPanel.Size = new Size(1072, 205);
            detalleTableLayoutPanel.TabIndex = 3;
            //
            // porSucursalLabel
            //
            porSucursalLabel.AutoSize = true;
            porSucursalLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            porSucursalLabel.Name = "porSucursalLabel";
            porSucursalLabel.TabIndex = 0;
            porSucursalLabel.Text = "Ventas por sucursal";
            //
            // productosLabel
            //
            productosLabel.AutoSize = true;
            productosLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            productosLabel.Name = "productosLabel";
            productosLabel.TabIndex = 1;
            productosLabel.Text = "Productos más vendidos";
            //
            // porSucursalDataGridView
            //
            porSucursalDataGridView.AllowUserToAddRows = false;
            porSucursalDataGridView.AllowUserToDeleteRows = false;
            porSucursalDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            porSucursalDataGridView.BackgroundColor = SystemColors.Window;
            porSucursalDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            porSucursalDataGridView.Dock = DockStyle.Fill;
            porSucursalDataGridView.Name = "porSucursalDataGridView";
            porSucursalDataGridView.ReadOnly = true;
            porSucursalDataGridView.RowHeadersVisible = false;
            porSucursalDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            porSucursalDataGridView.TabIndex = 2;
            //
            // productosDataGridView
            //
            productosDataGridView.AllowUserToAddRows = false;
            productosDataGridView.AllowUserToDeleteRows = false;
            productosDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            productosDataGridView.BackgroundColor = SystemColors.Window;
            productosDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            productosDataGridView.Dock = DockStyle.Fill;
            productosDataGridView.Name = "productosDataGridView";
            productosDataGridView.ReadOnly = true;
            productosDataGridView.RowHeadersVisible = false;
            productosDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            productosDataGridView.TabIndex = 3;
            //
            // cerrarButton
            //
            cerrarButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cerrarButton.Location = new Point(14, 692);
            cerrarButton.Name = "cerrarButton";
            cerrarButton.Size = new Size(120, 32);
            cerrarButton.TabIndex = 4;
            cerrarButton.Text = "Cerrar";
            cerrarButton.UseVisualStyleBackColor = true;
            cerrarButton.Click += cerrarButton_Click;
            //
            // generadoLabel
            //
            generadoLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            generadoLabel.AutoSize = true;
            generadoLabel.ForeColor = SystemColors.GrayText;
            generadoLabel.Location = new Point(148, 701);
            generadoLabel.Name = "generadoLabel";
            generadoLabel.Size = new Size(0, 15);
            generadoLabel.TabIndex = 5;
            //
            // ReporteVentas
            //
            AcceptButton = generarButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 736);
            Controls.Add(generadoLabel);
            Controls.Add(cerrarButton);
            Controls.Add(detalleTableLayoutPanel);
            Controls.Add(graficoPictureBox);
            Controls.Add(indicadoresTableLayoutPanel);
            Controls.Add(filtrosGroupBox);
            MinimumSize = new Size(1000, 700);
            Name = "ReporteVentas";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Reporte de ventas por período y sucursal";
            Load += ReporteVentas_Load;
            filtrosGroupBox.ResumeLayout(false);
            filtrosGroupBox.PerformLayout();
            indicadoresTableLayoutPanel.ResumeLayout(false);
            detalleTableLayoutPanel.ResumeLayout(false);
            detalleTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)graficoPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)porSucursalDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)productosDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox filtrosGroupBox;
        private Label desdeLabel;
        private DateTimePicker desdeDateTimePicker;
        private Label hastaLabel;
        private DateTimePicker hastaDateTimePicker;
        private Label sucursalLabel;
        private ComboBox sucursalComboBox;
        private Button generarButton;
        private Button exportarButton;
        private TableLayoutPanel indicadoresTableLayoutPanel;
        private Label ventasTituloLabel;
        private Label unidadesTituloLabel;
        private Label totalTituloLabel;
        private Label ticketTituloLabel;
        private Label ventasValorLabel;
        private Label unidadesValorLabel;
        private Label totalValorLabel;
        private Label ticketValorLabel;
        private PictureBox graficoPictureBox;
        private TableLayoutPanel detalleTableLayoutPanel;
        private Label porSucursalLabel;
        private Label productosLabel;
        private DataGridView porSucursalDataGridView;
        private DataGridView productosDataGridView;
        private Button cerrarButton;
        private Label generadoLabel;
    }
}
