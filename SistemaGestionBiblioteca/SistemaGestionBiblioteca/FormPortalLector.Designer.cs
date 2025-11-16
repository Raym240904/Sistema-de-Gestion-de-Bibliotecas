namespace SistemaGestionBiblioteca
{
    partial class FormPortalLector
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
            this.panelTitulo = new System.Windows.Forms.Panel();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCatalogo = new System.Windows.Forms.TabPage();
            this.dgvCatalogo = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSolicitarPrestamo = new System.Windows.Forms.Button();
            this.btnBuscarCatalogo = new System.Windows.Forms.Button();
            this.txtBuscarCatalogo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabMisPrestamos = new System.Windows.Forms.TabPage();
            this.dgvMisPrestamos = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tabMisSolicitudes = new System.Windows.Forms.TabPage();
            this.dgvMisSolicitudes = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.panelTitulo.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabCatalogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatalogo)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabMisPrestamos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMisPrestamos)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabMisSolicitudes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMisSolicitudes)).BeginInit();
            this.panel3.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitulo
            // 
            this.panelTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelTitulo.Controls.Add(this.lblBienvenida);
            this.panelTitulo.Controls.Add(this.btnMinimizar);
            this.panelTitulo.Controls.Add(this.btnCerrar);
            this.panelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitulo.Location = new System.Drawing.Point(0, 0);
            this.panelTitulo.Name = "panelTitulo";
            this.panelTitulo.Size = new System.Drawing.Size(1200, 50);
            this.panelTitulo.TabIndex = 0;
            this.panelTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitulo_MouseDown);
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienvenida.ForeColor = System.Drawing.Color.White;
            this.lblBienvenida.Location = new System.Drawing.Point(12, 12);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(246, 25);
            this.lblBienvenida.TabIndex = 2;
            this.lblBienvenida.Text = "PORTAL DEL LECTOR";
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.btnMinimizar.ForeColor = System.Drawing.Color.White;
            this.btnMinimizar.Location = new System.Drawing.Point(1110, 0);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(45, 50);
            this.btnMinimizar.TabIndex = 1;
            this.btnMinimizar.Text = "_";
            this.btnMinimizar.UseVisualStyleBackColor = true;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(1155, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(45, 50);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCatalogo);
            this.tabControl1.Controls.Add(this.tabMisPrestamos);
            this.tabControl1.Controls.Add(this.tabMisSolicitudes);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tabControl1.Location = new System.Drawing.Point(0, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1200, 570);
            this.tabControl1.TabIndex = 1;
            // 
            // tabCatalogo
            // 
            this.tabCatalogo.Controls.Add(this.dgvCatalogo);
            this.tabCatalogo.Controls.Add(this.panel1);
            this.tabCatalogo.Location = new System.Drawing.Point(4, 26);
            this.tabCatalogo.Name = "tabCatalogo";
            this.tabCatalogo.Padding = new System.Windows.Forms.Padding(10);
            this.tabCatalogo.Size = new System.Drawing.Size(1192, 540);
            this.tabCatalogo.TabIndex = 0;
            this.tabCatalogo.Text = "📚 Catálogo de Libros";
            this.tabCatalogo.UseVisualStyleBackColor = true;
            // 
            // dgvCatalogo
            // 
            this.dgvCatalogo.AllowUserToAddRows = false;
            this.dgvCatalogo.AllowUserToDeleteRows = false;
            this.dgvCatalogo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCatalogo.BackgroundColor = System.Drawing.Color.White;
            this.dgvCatalogo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCatalogo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCatalogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvCatalogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCatalogo.Location = new System.Drawing.Point(10, 90);
            this.dgvCatalogo.MultiSelect = false;
            this.dgvCatalogo.Name = "dgvCatalogo";
            this.dgvCatalogo.ReadOnly = true;
            this.dgvCatalogo.RowHeadersVisible = false;
            this.dgvCatalogo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCatalogo.Size = new System.Drawing.Size(1172, 440);
            this.dgvCatalogo.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.btnSolicitarPrestamo);
            this.panel1.Controls.Add(this.btnBuscarCatalogo);
            this.panel1.Controls.Add(this.txtBuscarCatalogo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1172, 80);
            this.panel1.TabIndex = 0;
            // 
            // btnSolicitarPrestamo
            // 
            this.btnSolicitarPrestamo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSolicitarPrestamo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSolicitarPrestamo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSolicitarPrestamo.FlatAppearance.BorderSize = 0;
            this.btnSolicitarPrestamo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSolicitarPrestamo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSolicitarPrestamo.ForeColor = System.Drawing.Color.White;
            this.btnSolicitarPrestamo.Location = new System.Drawing.Point(955, 20);
            this.btnSolicitarPrestamo.Name = "btnSolicitarPrestamo";
            this.btnSolicitarPrestamo.Size = new System.Drawing.Size(200, 40);
            this.btnSolicitarPrestamo.TabIndex = 3;
            this.btnSolicitarPrestamo.Text = "📋 Solicitar Préstamo";
            this.btnSolicitarPrestamo.UseVisualStyleBackColor = false;
            this.btnSolicitarPrestamo.Click += new System.EventHandler(this.btnSolicitarPrestamo_Click);
            // 
            // btnBuscarCatalogo
            // 
            this.btnBuscarCatalogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnBuscarCatalogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarCatalogo.FlatAppearance.BorderSize = 0;
            this.btnBuscarCatalogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarCatalogo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBuscarCatalogo.ForeColor = System.Drawing.Color.White;
            this.btnBuscarCatalogo.Location = new System.Drawing.Point(630, 20);
            this.btnBuscarCatalogo.Name = "btnBuscarCatalogo";
            this.btnBuscarCatalogo.Size = new System.Drawing.Size(120, 40);
            this.btnBuscarCatalogo.TabIndex = 2;
            this.btnBuscarCatalogo.Text = "🔍 Buscar";
            this.btnBuscarCatalogo.UseVisualStyleBackColor = false;
            this.btnBuscarCatalogo.Click += new System.EventHandler(this.btnBuscarCatalogo_Click);
            // 
            // txtBuscarCatalogo
            // 
            this.txtBuscarCatalogo.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtBuscarCatalogo.Location = new System.Drawing.Point(200, 25);
            this.txtBuscarCatalogo.Name = "txtBuscarCatalogo";
            this.txtBuscarCatalogo.Size = new System.Drawing.Size(400, 29);
            this.txtBuscarCatalogo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Buscar Libro (Cualquier campo):";
            // 
            // tabMisPrestamos
            // 
            this.tabMisPrestamos.Controls.Add(this.dgvMisPrestamos);
            this.tabMisPrestamos.Controls.Add(this.panel2);
            this.tabMisPrestamos.Location = new System.Drawing.Point(4, 26);
            this.tabMisPrestamos.Name = "tabMisPrestamos";
            this.tabMisPrestamos.Padding = new System.Windows.Forms.Padding(10);
            this.tabMisPrestamos.Size = new System.Drawing.Size(1192, 540);
            this.tabMisPrestamos.TabIndex = 1;
            this.tabMisPrestamos.Text = "📖 Mis Préstamos Activos";
            this.tabMisPrestamos.UseVisualStyleBackColor = true;
            // 
            // dgvMisPrestamos
            // 
            this.dgvMisPrestamos.AllowUserToAddRows = false;
            this.dgvMisPrestamos.AllowUserToDeleteRows = false;
            this.dgvMisPrestamos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMisPrestamos.BackgroundColor = System.Drawing.Color.White;
            this.dgvMisPrestamos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMisPrestamos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMisPrestamos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMisPrestamos.Location = new System.Drawing.Point(10, 70);
            this.dgvMisPrestamos.MultiSelect = false;
            this.dgvMisPrestamos.Name = "dgvMisPrestamos";
            this.dgvMisPrestamos.ReadOnly = true;
            this.dgvMisPrestamos.RowHeadersVisible = false;
            this.dgvMisPrestamos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMisPrestamos.Size = new System.Drawing.Size(1172, 460);
            this.dgvMisPrestamos.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1172, 60);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(20, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(582, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "📌 Aquí puedes ver todos tus préstamos activos y las fechas de devolución";
            // 
            // tabMisSolicitudes
            // 
            this.tabMisSolicitudes.Controls.Add(this.dgvMisSolicitudes);
            this.tabMisSolicitudes.Controls.Add(this.panel3);
            this.tabMisSolicitudes.Location = new System.Drawing.Point(4, 26);
            this.tabMisSolicitudes.Name = "tabMisSolicitudes";
            this.tabMisSolicitudes.Padding = new System.Windows.Forms.Padding(10);
            this.tabMisSolicitudes.Size = new System.Drawing.Size(1192, 540);
            this.tabMisSolicitudes.TabIndex = 2;
            this.tabMisSolicitudes.Text = "📝 Mis Solicitudes";
            this.tabMisSolicitudes.UseVisualStyleBackColor = true;
            // 
            // dgvMisSolicitudes
            // 
            this.dgvMisSolicitudes.AllowUserToAddRows = false;
            this.dgvMisSolicitudes.AllowUserToDeleteRows = false;
            this.dgvMisSolicitudes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMisSolicitudes.BackgroundColor = System.Drawing.Color.White;
            this.dgvMisSolicitudes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMisSolicitudes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMisSolicitudes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMisSolicitudes.Location = new System.Drawing.Point(10, 70);
            this.dgvMisSolicitudes.MultiSelect = false;
            this.dgvMisSolicitudes.Name = "dgvMisSolicitudes";
            this.dgvMisSolicitudes.ReadOnly = true;
            this.dgvMisSolicitudes.RowHeadersVisible = false;
            this.dgvMisSolicitudes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMisSolicitudes.Size = new System.Drawing.Size(1172, 460);
            this.dgvMisSolicitudes.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1172, 60);
            this.panel3.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(20, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(706, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "📌 Aquí puedes ver el estado de todas tus solicitudes: Pendientes, Aprobadas o Rechazadas";
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelBotones.Controls.Add(this.btnActualizar);
            this.panelBotones.Controls.Add(this.btnCerrarSesion);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 620);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(1200, 80);
            this.panelBotones.TabIndex = 2;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatAppearance.BorderSize = 0;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnActualizar.ForeColor = System.Drawing.Color.White;
            this.btnActualizar.Location = new System.Drawing.Point(870, 15);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(150, 50);
            this.btnActualizar.TabIndex = 1;
            this.btnActualizar.Text = "🔄 Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrarSesion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCerrarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarSesion.FlatAppearance.BorderSize = 0;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Location = new System.Drawing.Point(1030, 15);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(150, 50);
            this.btnCerrarSesion.TabIndex = 0;
            this.btnCerrarSesion.Text = "🚪 Cerrar Sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // FormPortalLector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.panelTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPortalLector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Portal del Lector";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormPortalLector_FormClosed);
            this.Load += new System.EventHandler(this.FormPortalLector_Load);
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabCatalogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatalogo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabMisPrestamos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMisPrestamos)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabMisSolicitudes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMisSolicitudes)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCatalogo;
        private System.Windows.Forms.DataGridView dgvCatalogo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSolicitarPrestamo;
        private System.Windows.Forms.Button btnBuscarCatalogo;
        private System.Windows.Forms.TextBox txtBuscarCatalogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabMisPrestamos;
        private System.Windows.Forms.DataGridView dgvMisPrestamos;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabMisSolicitudes;
        private System.Windows.Forms.DataGridView dgvMisSolicitudes;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnCerrarSesion;
    }
}