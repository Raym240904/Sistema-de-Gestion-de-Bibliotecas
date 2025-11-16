using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SistemaGestionBiblioteca
{
    public partial class FormPrestamos : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public FormPrestamos()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormPrestamos_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
            CargarLibros();
            CargarPrestamos();
            dtpFechaPrestamo.Value = DateTime.Now;
            dtpFechaDevolucion.Value = DateTime.Now.AddDays(15);
        }

        private void CargarUsuarios()
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = "SELECT IdUsuario, NombreCompleto + ' - ' + DNI as Nombre FROM Usuarios WHERE Estado = 1";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboUsuario.DataSource = dt;
                cboUsuario.DisplayMember = "Nombre";
                cboUsuario.ValueMember = "IdUsuario";
                cboUsuario.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        private void CargarLibros()
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = "SELECT IdLibro, Titulo + ' - ' + Autor as Libro FROM Libros WHERE Estado = 1 AND CantidadDisponible > 0";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboLibro.DataSource = dt;
                cboLibro.DisplayMember = "Libro";
                cboLibro.ValueMember = "IdLibro";
                cboLibro.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar libros: " + ex.Message);
            }
        }

        private void CargarPrestamos()
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = @"SELECT P.IdPrestamo, U.NombreCompleto as Usuario, L.Titulo as Libro, 
                                P.FechaPrestamo, P.FechaDevolucionEstimada, P.Estado 
                                FROM Prestamos P 
                                INNER JOIN Usuarios U ON P.IdUsuario = U.IdUsuario 
                                INNER JOIN Libros L ON P.IdLibro = L.IdLibro 
                                WHERE P.Estado = 'Prestado' 
                                ORDER BY P.FechaPrestamo DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPrestamos.DataSource = dt;
                dgvPrestamos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPrestamos.Columns["IdPrestamo"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar préstamos: " + ex.Message);
            }
        }

        private void btnRegistrarPrestamo_Click(object sender, EventArgs e)
        {
            if (cboUsuario.SelectedIndex == -1 || cboLibro.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione usuario y libro", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpFechaDevolucion.Value <= dtpFechaPrestamo.Value)
            {
                MessageBox.Show("La fecha de devolución debe ser posterior a la fecha de préstamo",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                SqlCommand cmd = new SqlCommand("sp_RegistrarPrestamo", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", cboUsuario.SelectedValue);
                cmd.Parameters.AddWithValue("@IdLibro", cboLibro.SelectedValue);

                int diasPrestamo = (dtpFechaDevolucion.Value - dtpFechaPrestamo.Value).Days;
                cmd.Parameters.AddWithValue("@DiasPrestamo", diasPrestamo);

                // NUEVO: Registrar quién realizó el préstamo
                cmd.Parameters.AddWithValue("@IdBibliotecario", SesionUsuario.IdUsuarioSistema);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int resultado = Convert.ToInt32(reader[0]);
                    reader.Close();

                    if (resultado == 1)
                    {
                        MessageBox.Show($"Préstamo registrado correctamente por {SesionUsuario.NombreCompleto}",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LimpiarCampos();
                        CargarLibros();
                        CargarPrestamos();
                    }
                    else
                    {
                        MessageBox.Show("No hay ejemplares disponibles de este libro", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar préstamo: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            cboUsuario.SelectedIndex = -1;
            cboLibro.SelectedIndex = -1;
            dtpFechaPrestamo.Value = DateTime.Now;
            dtpFechaDevolucion.Value = DateTime.Now.AddDays(15);
            txtObservaciones.Clear();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            FormMenuPrincipal menu = new FormMenuPrincipal();
            menu.Show();
            this.Close();
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
    }
}