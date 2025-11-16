using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SistemaGestionBiblioteca
{
    public partial class FormMenuPrincipal : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public FormMenuPrincipal()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormMenuPrincipal_Load(object sender, EventArgs e)
        {
            ConfigurarPermisos();
            CargarDatos();
            CargarEstadisticas();
            MostrarInfoUsuario();
        }

        private void ConfigurarPermisos()
        {
            // Mostrar/Ocultar botones según el rol
            if (SesionUsuario.EsAdministrador())
            {
                // Administrador tiene acceso a todo
                btnLibros.Visible = true;
                btnUsuarios.Visible = true;
                btnPrestamos.Visible = true;
                btnDevoluciones.Visible = true;
                btnGestionSolicitudes.Visible = true;
                btnReportes.Visible = true;
                btnGestionRoles.Visible = true;
            }
            else if (SesionUsuario.EsBibliotecario())
            {
                // Bibliotecario: operaciones diarias
                btnLibros.Visible = true;
                btnUsuarios.Visible = true;
                btnPrestamos.Visible = true;
                btnDevoluciones.Visible = true;
                btnGestionSolicitudes.Visible = true;
                btnReportes.Visible = true;
                btnGestionRoles.Visible = false; // NO puede gestionar roles
            }
            else
            {
                // Otros roles no deberían estar aquí
                MessageBox.Show("Acceso no autorizado", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void MostrarInfoUsuario()
        {
            lblUsuarioActual.Text = $"Usuario: {SesionUsuario.NombreCompleto}";
            lblRolActual.Text = $"Rol: {SesionUsuario.Rol}";
        }

        private void CargarDatos()
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();

                string query = @"SELECT IdLibro, Titulo, Autor, Editorial, 
                                Categoria, CantidadDisponible as Disponibles 
                                FROM Libros WHERE Estado = 1";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvLibros.DataSource = dt;
                dgvLibros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvLibros.Columns["IdLibro"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        private void CargarEstadisticas()
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();

                // Total de libros
                SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Libros WHERE Estado = 1", con);
                lblTotalLibros.Text = cmd1.ExecuteScalar().ToString();

                // Total usuarios
                SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE Estado = 1", con);
                lblTotalUsuarios.Text = cmd2.ExecuteScalar().ToString();

                // Préstamos activos
                SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM Prestamos WHERE Estado = 'Prestado'", con);
                lblPrestamosActivos.Text = cmd3.ExecuteScalar().ToString();

                // Multas pendientes
                SqlCommand cmd4 = new SqlCommand("SELECT ISNULL(SUM(MontoMulta), 0) FROM Prestamos WHERE Estado = 'Retrasado' AND FechaDevolucionReal IS NULL", con);
                lblMultasPendientes.Text = "S/ " + cmd4.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estadísticas: " + ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                SqlCommand cmd = new SqlCommand("sp_BuscarLibros", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Criterio", txtBuscar.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvLibros.DataSource = dt;
                dgvLibros.Columns["IdLibro"].Visible = false;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron resultados", "Búsqueda",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en búsqueda: " + ex.Message);
            }
        }

        private void btnLibros_Click(object sender, EventArgs e)
        {
            FormLibros formLibros = new FormLibros();
            formLibros.Show();
            this.Hide();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            FormUsuarios formUsuarios = new FormUsuarios();
            formUsuarios.Show();
            this.Hide();
        }

        private void btnPrestamos_Click(object sender, EventArgs e)
        {
            FormPrestamos formPrestamos = new FormPrestamos();
            formPrestamos.Show();
            this.Hide();
        }

        private void btnDevoluciones_Click(object sender, EventArgs e)
        {
            FormDevoluciones formDev = new FormDevoluciones();
            formDev.Show();
            this.Hide();
        }

        private void btnGestionSolicitudes_Click(object sender, EventArgs e)
        {
            FormGestionSolicitudes formSolicitudes = new FormGestionSolicitudes();
            formSolicitudes.Show();
            this.Hide();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            FormReportes formRep = new FormReportes();
            formRep.Show();
            this.Hide();
        }

        private void btnGestionRoles_Click(object sender, EventArgs e)
        {
            if (!SesionUsuario.EsAdministrador())
            {
                MessageBox.Show("Solo el Administrador puede acceder a esta sección",
                    "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FormGestionRoles formRoles = new FormGestionRoles();
            formRoles.Show();
            this.Hide();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro de cerrar sesión?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SesionUsuario.CerrarSesion();
                FormLogin login = new FormLogin();
                login.Show();
                this.Close();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panelTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void FormMenuPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}