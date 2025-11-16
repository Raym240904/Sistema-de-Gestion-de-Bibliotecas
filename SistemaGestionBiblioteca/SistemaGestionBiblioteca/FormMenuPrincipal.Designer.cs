namespace SistemaGestionBiblioteca
{
    partial class FormMenuPrincipal
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

        private void InitializeComponent()
        {
            this.panelTitulo = new System.Windows.Forms.Panel();
            this.lblRolActual = new System.Windows.Forms.Label();
            this.lblUsuarioActual = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnGestionRoles = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.btnGestionSolicitudes = new System.Windows.Forms.Button();
            this.btnDevoluciones = new System.Windows.Forms.Button();
            this.btnPrestamos = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnLibros = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.lblSistema = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.dgvLibros = new System.Windows.Forms.DataGridView();
            this.panelBusqueda = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.panelEstadisticas = new System.Windows.Forms.Panel();
            this.panelMultas = new System.Windows.Forms.Panel();
            this.lblMultasPendientes = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panelPrestamos = new System.Windows.Forms.Panel();
            this.lblPrestamosActivos = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panelUsuarios = new System.Windows.Forms.Panel();
            this.lblTotalUsuarios = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panelLibros = new System.Windows.Forms.Panel();
            this.lblTotalLibros = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTitulo.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLibros)).BeginInit();
            this.panelBusqueda.SuspendLayout();
            this.panelEstadisticas.SuspendLayout();
            this.panelMultas.SuspendLayout();
            this.panelPrestamos.SuspendLayout();
            this.panelUsuarios.SuspendLayout();
            this.panelLibros.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitulo
            // 
            this.panelTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panelTitulo.Controls.Add(this.lblRolActual);
            this.panelTitulo.Controls.Add(this.lblUsuarioActual);
            this.panelTitulo.Controls.Add(this.lblTitulo);
            this.panelTitulo.Controls.Add(this.btnMinimizar);
            this.panelTitulo.Controls.Add(this.btnCerrar);
            this.panelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitulo.Location = new System.Drawing.Point(0, 0);
            this.panelTitulo.Name = "panelTitulo";
            this.panelTitulo.Size = new System.Drawing.Size(1200, 40);
            this.panelTitulo.TabIndex = 0;
            this.panelTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitulo_MouseDown);
            // 
            // lblRolActual
            // 
            this.lblRolActual.AutoSize = true;
            this.lblRolActual.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRolActual.ForeColor = System.Drawing.Color.White;
            this.lblRolActual.Location = new System.Drawing.Point(750, 12);
            this.lblRolActual.Name = "lblRolActual";
            this.lblRolActual.Size = new System.Drawing.Size(100, 15);
            this.lblRolActual.TabIndex = 4;
            this.lblRolActual.Text = "Rol: ";
            // 
            // lblUsuarioActual
            // 
            this.lblUsuarioActual.AutoSize = true;
            this.lblUsuarioActual.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUsuarioActual.ForeColor = System.Drawing.Color.White;
            this.lblUsuarioActual.Location = new System.Drawing.Point(500, 12);
            this.lblUsuarioActual.Name = "lblUsuarioActual";
            this.lblUsuarioActual.Size = new System.Drawing.Size(150, 15);
            this.lblUsuarioActual.TabIndex = 3;
            this.lblUsuarioActual.Text = "Usuario: ";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(220, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(167, 21);
            this.lblTitulo.TabIndex = 2;
            this.lblTitulo.Text = "MENÚ PRINCIPAL";
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimizar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.btnMinimizar.ForeColor = System.Drawing.Color.White;
            this.btnMinimizar.Location = new System.Drawing.Point(1120, 0);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(40, 40);
            this.btnMinimizar.TabIndex = 1;
            this.btnMinimizar.Text = "_";
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.Transparent;
            this.btnCerrar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(1160, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(40, 40);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.panelMenu.Controls.Add(this.btnGestionRoles);
            this.panelMenu.Controls.Add(this.btnReportes);
            this.panelMenu.Controls.Add(this.btnGestionSolicitudes);
            this.panelMenu.Controls.Add(this.btnDevoluciones);
            this.panelMenu.Controls.Add(this.btnPrestamos);
            this.panelMenu.Controls.Add(this.btnUsuarios);
            this.panelMenu.Controls.Add(this.btnLibros);
            this.panelMenu.Controls.Add(this.btnCerrarSesion);
            this.panelMenu.Controls.Add(this.panelLogo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 40);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(200, 660);
            this.panelMenu.TabIndex = 1;
            // 
            // btnGestionRoles
            // 
            this.btnGestionRoles.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGestionRoles.FlatAppearance.BorderSize = 0;
            this.btnGestionRoles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionRoles.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnGestionRoles.ForeColor = System.Drawing.Color.White;
            this.btnGestionRoles.Location = new System.Drawing.Point(0, 440);
            this.btnGestionRoles.Name = "btnGestionRoles";
            this.btnGestionRoles.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnGestionRoles.Size = new System.Drawing.Size(200, 50);
            this.btnGestionRoles.TabIndex = 8;
            this.btnGestionRoles.Text = "👤 Gestión de Roles";
            this.btnGestionRoles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGestionRoles.UseVisualStyleBackColor = true;
            this.btnGestionRoles.Click += new System.EventHandler(this.btnGestionRoles_Click);
            // 
            // btnReportes
            // 
            this.btnReportes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReportes.FlatAppearance.BorderSize = 0;
            this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnReportes.ForeColor = System.Drawing.Color.White;
            this.btnReportes.Location = new System.Drawing.Point(0, 390);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnReportes.Size = new System.Drawing.Size(200, 50);
            this.btnReportes.TabIndex = 7;
            this.btnReportes.Text = "📊 Reportes";
            this.btnReportes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportes.UseVisualStyleBackColor = true;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // btnGestionSolicitudes
            // 
            this.btnGestionSolicitudes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGestionSolicitudes.FlatAppearance.BorderSize = 0;
            this.btnGestionSolicitudes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionSolicitudes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnGestionSolicitudes.ForeColor = System.Drawing.Color.White;
            this.btnGestionSolicitudes.Location = new System.Drawing.Point(0, 340);
            this.btnGestionSolicitudes.Name = "btnGestionSolicitudes";
            this.btnGestionSolicitudes.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnGestionSolicitudes.Size = new System.Drawing.Size(200, 50);
            this.btnGestionSolicitudes.TabIndex = 6;
            this.btnGestionSolicitudes.Text = "📝 Solicitudes Pendientes";
            this.btnGestionSolicitudes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGestionSolicitudes.UseVisualStyleBackColor = true;
            this.btnGestionSolicitudes.Click += new System.EventHandler(this.btnGestionSolicitudes_Click);
            // 
            // btnDevoluciones
            // 
            this.btnDevoluciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDevoluciones.FlatAppearance.BorderSize = 0;
            this.btnDevoluciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDevoluciones.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDevoluciones.ForeColor = System.Drawing.Color.White;
            this.btnDevoluciones.Location = new System.Drawing.Point(0, 290);
            this.btnDevoluciones.Name = "btnDevoluciones";
            this.btnDevoluciones.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnDevoluciones.Size = new System.Drawing.Size(200, 50);
            this.btnDevoluciones.TabIndex = 5;
            this.btnDevoluciones.Text = "↩️ Devoluciones";
            this.btnDevoluciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDevoluciones.UseVisualStyleBackColor = true;
            this.btnDevoluciones.Click += new System.EventHandler(this.btnDevoluciones_Click);
            // 
            // btnPrestamos
            // 
            this.btnPrestamos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPrestamos.FlatAppearance.BorderSize = 0;
            this.btnPrestamos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrestamos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPrestamos.ForeColor = System.Drawing.Color.White;
            this.btnPrestamos.Location = new System.Drawing.Point(0, 240);
            this.btnPrestamos.Name = "btnPrestamos";
            this.btnPrestamos.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnPrestamos.Size = new System.Drawing.Size(200, 50);
            this.btnPrestamos.TabIndex = 4;
            this.btnPrestamos.Text = "📋 Préstamos";
            this.btnPrestamos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrestamos.UseVisualStyleBackColor = true;
            this.btnPrestamos.Click += new System.EventHandler(this.btnPrestamos_Click);
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUsuarios.FlatAppearance.BorderSize = 0;
            this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuarios.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnUsuarios.ForeColor = System.Drawing.Color.White;
            this.btnUsuarios.Location = new System.Drawing.Point(0, 190);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnUsuarios.Size = new System.Drawing.Size(200, 50);
            this.btnUsuarios.TabIndex = 3;
            this.btnUsuarios.Text = "👥 Usuarios";
            this.btnUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuarios.UseVisualStyleBackColor = true;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btnLibros
            // 
            this.btnLibros.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLibros.FlatAppearance.BorderSize = 0;
            this.btnLibros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLibros.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLibros.ForeColor = System.Drawing.Color.White;
            this.btnLibros.Location = new System.Drawing.Point(0, 140);
            this.btnLibros.Name = "btnLibros";
            this.btnLibros.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnLibros.Size = new System.Drawing.Size(200, 50);
            this.btnLibros.TabIndex = 2;
            this.btnLibros.Text = "📚 Libros";
            this.btnLibros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLibros.UseVisualStyleBackColor = true;
            this.btnLibros.Click += new System.EventHandler(this.btnLibros_Click);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCerrarSesion.FlatAppearance.BorderSize = 0;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Location = new System.Drawing.Point(0, 610);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnCerrarSesion.Size = new System.Drawing.Size(200, 50);
            this.btnCerrarSesion.TabIndex = 1;
            this.btnCerrarSesion.Text = "🚪 Cerrar Sesión";
            this.btnCerrarSesion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.lblSistema);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(200, 140);
            this.panelLogo.TabIndex = 0;
            // 
            // lblSistema
            // 
            this.lblSistema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSistema.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSistema.ForeColor = System.Drawing.Color.White;
            this.lblSistema.Location = new System.Drawing.Point(0, 0);
            this.lblSistema.Name = "lblSistema";
            this.lblSistema.Size = new System.Drawing.Size(200, 140);
            this.lblSistema.TabIndex = 0;
            this.lblSistema.Text = "SISTEMA DE\r\nGESTIÓN DE\r\nBIBLIOTECA";
            this.lblSistema.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelContenido
            // 
            this.panelContenido.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelContenido.Controls.Add(this.dgvLibros);
            this.panelContenido.Controls.Add(this.panelBusqueda);
            this.panelContenido.Controls.Add(this.panelEstadisticas);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(200, 40);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenido.Size = new System.Drawing.Size(1000, 660);
            this.panelContenido.TabIndex = 2;
            // 
            // dgvLibros
            // 
            this.dgvLibros.AllowUserToAddRows = false;
            this.dgvLibros.AllowUserToDeleteRows = false;
            this.dgvLibros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLibros.BackgroundColor = System.Drawing.Color.White;
            this.dgvLibros.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLibros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLibros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLibros.Location = new System.Drawing.Point(20, 220);
            this.dgvLibros.Name = "dgvLibros";
            this.dgvLibros.ReadOnly = true;
            this.dgvLibros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLibros.Size = new System.Drawing.Size(960, 420);
            this.dgvLibros.TabIndex = 2;
            // 
            // panelBusqueda
            // 
            this.panelBusqueda.BackColor = System.Drawing.Color.White;
            this.panelBusqueda.Controls.Add(this.btnBuscar);
            this.panelBusqueda.Controls.Add(this.txtBuscar);
            this.panelBusqueda.Controls.Add(this.lblBuscar);
            this.panelBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBusqueda.Location = new System.Drawing.Point(20, 140);
            this.panelBusqueda.Name = "panelBusqueda";
            this.panelBusqueda.Padding = new System.Windows.Forms.Padding(15);
            this.panelBusqueda.Size = new System.Drawing.Size(960, 80);
            this.panelBusqueda.TabIndex = 1;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(820, 20);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(120, 40);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtBuscar.Location = new System.Drawing.Point(200, 25);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(600, 29);
            this.txtBuscar.TabIndex = 1;
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBuscar.Location = new System.Drawing.Point(18, 28);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(176, 20);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Catálogo con Búsqueda:";
            // 
            // panelEstadisticas
            // 
            this.panelEstadisticas.Controls.Add(this.panelMultas);
            this.panelEstadisticas.Controls.Add(this.panelPrestamos);
            this.panelEstadisticas.Controls.Add(this.panelUsuarios);
            this.panelEstadisticas.Controls.Add(this.panelLibros);
            this.panelEstadisticas.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEstadisticas.Location = new System.Drawing.Point(20, 20);
            this.panelEstadisticas.Name = "panelEstadisticas";
            this.panelEstadisticas.Size = new System.Drawing.Size(960, 120);
            this.panelEstadisticas.TabIndex = 0;
            // 
            // panelMultas
            // 
            this.panelMultas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.panelMultas.Controls.Add(this.lblMultasPendientes);
            this.panelMultas.Controls.Add(this.label8);
            this.panelMultas.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMultas.Location = new System.Drawing.Point(720, 0);
            this.panelMultas.Name = "panelMultas";
            this.panelMultas.Size = new System.Drawing.Size(230, 120);
            this.panelMultas.TabIndex = 3;
            // 
            // lblMultasPendientes
            // 
            this.lblMultasPendientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMultasPendientes.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblMultasPendientes.ForeColor = System.Drawing.Color.White;
            this.lblMultasPendientes.Location = new System.Drawing.Point(0, 40);
            this.lblMultasPendientes.Name = "lblMultasPendientes";
            this.lblMultasPendientes.Size = new System.Drawing.Size(230, 80);
            this.lblMultasPendientes.TabIndex = 1;
            this.lblMultasPendientes.Text = "0";
            this.lblMultasPendientes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(230, 40);
            this.label8.TabIndex = 0;
            this.label8.Text = "Multas Pendientes";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelPrestamos
            // 
            this.panelPrestamos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.panelPrestamos.Controls.Add(this.lblPrestamosActivos);
            this.panelPrestamos.Controls.Add(this.label6);
            this.panelPrestamos.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelPrestamos.Location = new System.Drawing.Point(480, 0);
            this.panelPrestamos.Name = "panelPrestamos";
            this.panelPrestamos.Size = new System.Drawing.Size(240, 120);
            this.panelPrestamos.TabIndex = 2;
            // 
            // lblPrestamosActivos
            // 
            this.lblPrestamosActivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrestamosActivos.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblPrestamosActivos.ForeColor = System.Drawing.Color.White;
            this.lblPrestamosActivos.Location = new System.Drawing.Point(0, 40);
            this.lblPrestamosActivos.Name = "lblPrestamosActivos";
            this.lblPrestamosActivos.Size = new System.Drawing.Size(240, 80);
            this.lblPrestamosActivos.TabIndex = 1;
            this.lblPrestamosActivos.Text = "0";
            this.lblPrestamosActivos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(240, 40);
            this.label6.TabIndex = 0;
            this.label6.Text = "Préstamos Activos";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelUsuarios
            // 
            this.panelUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.panelUsuarios.Controls.Add(this.lblTotalUsuarios);
            this.panelUsuarios.Controls.Add(this.label4);
            this.panelUsuarios.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelUsuarios.Location = new System.Drawing.Point(240, 0);
            this.panelUsuarios.Name = "panelUsuarios";
            this.panelUsuarios.Size = new System.Drawing.Size(240, 120);
            this.panelUsuarios.TabIndex = 1;
            // 
            // lblTotalUsuarios
            // 
            this.lblTotalUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalUsuarios.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalUsuarios.ForeColor = System.Drawing.Color.White;
            this.lblTotalUsuarios.Location = new System.Drawing.Point(0, 40);
            this.lblTotalUsuarios.Name = "lblTotalUsuarios";
            this.lblTotalUsuarios.Size = new System.Drawing.Size(240, 80);
            this.lblTotalUsuarios.TabIndex = 1;
            this.lblTotalUsuarios.Text = "0";
            this.lblTotalUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(240, 40);
            this.label4.TabIndex = 0;
            this.label4.Text = "Total Usuarios";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelLibros
            // 
            this.panelLibros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.panelLibros.Controls.Add(this.lblTotalLibros);
            this.panelLibros.Controls.Add(this.label1);
            this.panelLibros.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLibros.Location = new System.Drawing.Point(0, 0);
            this.panelLibros.Name = "panelLibros";
            this.panelLibros.Size = new System.Drawing.Size(240, 120);
            this.panelLibros.TabIndex = 0;
            // 
            // lblTotalLibros
            // 
            this.lblTotalLibros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalLibros.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalLibros.ForeColor = System.Drawing.Color.White;
            this.lblTotalLibros.Location = new System.Drawing.Point(0, 40);
            this.lblTotalLibros.Name = "lblTotalLibros";
            this.lblTotalLibros.Size = new System.Drawing.Size(240, 80);
            this.lblTotalLibros.TabIndex = 1;
            this.lblTotalLibros.Text = "0";
            this.lblTotalLibros.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Libros";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormMenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FormMenuPrincipal";
            this.Text = "Menú Principal - Sistema de Gestión de Biblioteca";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMenuPrincipal_FormClosed);
            this.Load += new System.EventHandler(this.FormMenuPrincipal_Load);
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.panelMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelContenido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLibros)).EndInit();
            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.panelEstadisticas.ResumeLayout(false);
            this.panelMultas.ResumeLayout(false);
            this.panelPrestamos.ResumeLayout(false);
            this.panelUsuarios.ResumeLayout(false);
            this.panelLibros.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #region Variables de Componentes

        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Label lblRolActual;
        private System.Windows.Forms.Label lblUsuarioActual;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnGestionRoles;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnGestionSolicitudes;
        private System.Windows.Forms.Button btnDevoluciones;
        private System.Windows.Forms.Button btnPrestamos;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnLibros;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label lblSistema;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.DataGridView dgvLibros;
        private System.Windows.Forms.Panel panelBusqueda;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.Panel panelEstadisticas;
        private System.Windows.Forms.Panel panelMultas;
        private System.Windows.Forms.Label lblMultasPendientes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panelPrestamos;
        private System.Windows.Forms.Label lblPrestamosActivos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelUsuarios;
        private System.Windows.Forms.Label lblTotalUsuarios;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelLibros;
        private System.Windows.Forms.Label lblTotalLibros;
        private System.Windows.Forms.Label label1;

        #endregion
    }
}