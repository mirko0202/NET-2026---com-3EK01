namespace WindowsForms
{
    partial class Home
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
            menuStrip = new MenuStrip();
            gestionToolStripMenuItem = new ToolStripMenuItem();
            productosToolStripMenuItem = new ToolStripMenuItem();
            ingredientesToolStripMenuItem = new ToolStripMenuItem();
            ventasToolStripMenuItem = new ToolStripMenuItem();
            sesionToolStripMenuItem = new ToolStripMenuItem();
            cerrarSesionToolStripMenuItem = new ToolStripMenuItem();
            separadorSesionToolStripMenuItem = new ToolStripSeparator();
            salirToolStripMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            usuarioStatusLabel = new ToolStripStatusLabel();
            bienvenidaLabel = new Label();
            detalleLabel = new Label();
            permisosLabel = new Label();
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            //
            // menuStrip
            //
            menuStrip.Items.AddRange(new ToolStripItem[] { gestionToolStripMenuItem, sesionToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(900, 24);
            menuStrip.TabIndex = 0;
            //
            // gestionToolStripMenuItem
            //
            gestionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { productosToolStripMenuItem, ingredientesToolStripMenuItem, ventasToolStripMenuItem });
            gestionToolStripMenuItem.Name = "gestionToolStripMenuItem";
            gestionToolStripMenuItem.Size = new Size(59, 20);
            gestionToolStripMenuItem.Text = "&Gestión";
            //
            // productosToolStripMenuItem
            //
            productosToolStripMenuItem.Name = "productosToolStripMenuItem";
            productosToolStripMenuItem.Size = new Size(180, 22);
            productosToolStripMenuItem.Text = "&Productos";
            productosToolStripMenuItem.Click += productosToolStripMenuItem_Click;
            //
            // ingredientesToolStripMenuItem
            //
            ingredientesToolStripMenuItem.Name = "ingredientesToolStripMenuItem";
            ingredientesToolStripMenuItem.Size = new Size(180, 22);
            ingredientesToolStripMenuItem.Text = "&Ingredientes";
            ingredientesToolStripMenuItem.Click += ingredientesToolStripMenuItem_Click;
            //
            // ventasToolStripMenuItem
            //
            ventasToolStripMenuItem.Name = "ventasToolStripMenuItem";
            ventasToolStripMenuItem.Size = new Size(180, 22);
            ventasToolStripMenuItem.Text = "&Ventas";
            ventasToolStripMenuItem.Click += ventasToolStripMenuItem_Click;
            //
            // sesionToolStripMenuItem
            //
            sesionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cerrarSesionToolStripMenuItem, separadorSesionToolStripMenuItem, salirToolStripMenuItem });
            sesionToolStripMenuItem.Name = "sesionToolStripMenuItem";
            sesionToolStripMenuItem.Size = new Size(54, 20);
            sesionToolStripMenuItem.Text = "&Sesión";
            //
            // cerrarSesionToolStripMenuItem
            //
            cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            cerrarSesionToolStripMenuItem.Size = new Size(180, 22);
            cerrarSesionToolStripMenuItem.Text = "&Cerrar sesión";
            cerrarSesionToolStripMenuItem.Click += cerrarSesionToolStripMenuItem_Click;
            //
            // separadorSesionToolStripMenuItem
            //
            separadorSesionToolStripMenuItem.Name = "separadorSesionToolStripMenuItem";
            separadorSesionToolStripMenuItem.Size = new Size(177, 6);
            //
            // salirToolStripMenuItem
            //
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.Size = new Size(180, 22);
            salirToolStripMenuItem.Text = "S&alir";
            salirToolStripMenuItem.Click += salirToolStripMenuItem_Click;
            //
            // statusStrip
            //
            statusStrip.Items.AddRange(new ToolStripItem[] { usuarioStatusLabel });
            statusStrip.Location = new Point(0, 478);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(900, 22);
            statusStrip.TabIndex = 1;
            //
            // usuarioStatusLabel
            //
            usuarioStatusLabel.Name = "usuarioStatusLabel";
            usuarioStatusLabel.Size = new Size(0, 17);
            //
            // bienvenidaLabel
            //
            bienvenidaLabel.AutoSize = true;
            bienvenidaLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            bienvenidaLabel.Location = new Point(40, 70);
            bienvenidaLabel.Name = "bienvenidaLabel";
            bienvenidaLabel.Size = new Size(300, 37);
            bienvenidaLabel.TabIndex = 2;
            bienvenidaLabel.Text = "Gestión de Stock";
            //
            // detalleLabel
            //
            detalleLabel.AutoSize = true;
            detalleLabel.Font = new Font("Segoe UI", 11F);
            detalleLabel.ForeColor = SystemColors.GrayText;
            detalleLabel.Location = new Point(44, 115);
            detalleLabel.Name = "detalleLabel";
            detalleLabel.Size = new Size(300, 20);
            detalleLabel.TabIndex = 3;
            detalleLabel.Text = "Sistema de gestión de stock para tienda gastronómica";
            //
            // permisosLabel
            //
            permisosLabel.AutoSize = true;
            permisosLabel.Location = new Point(44, 165);
            permisosLabel.Name = "permisosLabel";
            permisosLabel.Size = new Size(300, 15);
            permisosLabel.TabIndex = 4;
            permisosLabel.Text = "";
            //
            // Home
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 500);
            Controls.Add(permisosLabel);
            Controls.Add(detalleLabel);
            Controls.Add(bienvenidaLabel);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "Home";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Stock - Escritorio";
            WindowState = FormWindowState.Maximized;
            Load += Home_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem gestionToolStripMenuItem;
        private ToolStripMenuItem productosToolStripMenuItem;
        private ToolStripMenuItem ingredientesToolStripMenuItem;
        private ToolStripMenuItem ventasToolStripMenuItem;
        private ToolStripMenuItem sesionToolStripMenuItem;
        private ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private ToolStripSeparator separadorSesionToolStripMenuItem;
        private ToolStripMenuItem salirToolStripMenuItem;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel usuarioStatusLabel;
        private Label bienvenidaLabel;
        private Label detalleLabel;
        private Label permisosLabel;
    }
}
