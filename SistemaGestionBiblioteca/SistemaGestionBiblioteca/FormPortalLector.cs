using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SistemaGestionBiblioteca
{
    public partial class FormPortalLector : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public FormPortalLector()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormPortalLector_Load(object sender, EventArgs e)
        {
            // VERIFICACIÓN CRÍTICA: Asegurarse de que el usuario tiene un IdUsuarioLector válido
            if (SesionUsuario.IdUsuarioLector <= 0)
            {
                MessageBox.Show("Error: No se ha iniciado sesión correctamente como lector.\n" +
                    "IdUsuarioLector: " + SesionUsuario.IdUsuarioLector,
                    "Error de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Cerrar sesión y volver al login
                SesionUsuario.CerrarSesion();
                FormLogin login = new FormLogin();
                login.Show();
                this.Close();
                return;
            }

            MostrarInfoUsuario();
            CargarCatalogo();
            CargarMisPrestamos();
            CargarMisSolicitudes();
        }

        private void MostrarInfoUsuario()
        {
            lblBienvenida.Text = $"¡Bienvenido, {SesionUsuario.NombreCompleto}!";

            // Información adicional para debug (puedes comentar esto después)
            this.Text = $"Portal Lector - Usuario ID: {SesionUsuario.IdUsuarioLector}";
        }

        private void CargarCatalogo()
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = @"SELECT IdLibro, Titulo, Autor, Editorial, Categoria, 
                                AnioPublicacion as Año, CantidadDisponible as Disponibles,
                                CASE WHEN CantidadDisponible > 0 THEN 'Disponible' ELSE 'No Disponible' END as Estado
                                FROM Libros WHERE Estado = 1 ORDER BY Titulo";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvCatalogo.DataSource = dt;
                dgvCatalogo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvCatalogo.Columns["IdLibro"] != null)
                    dgvCatalogo.Columns["IdLibro"].Visible = false;

                // Colorear filas según disponibilidad
                foreach (DataGridViewRow row in dgvCatalogo.Rows)
                {
                    if (row.Cells["Disponibles"].Value != null &&
                        Convert.ToInt32(row.Cells["Disponibles"].Value) == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar catálogo: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarMisPrestamos()
        {
            try
            {
                // VERIFICACIÓN antes de hacer la consulta
                if (SesionUsuario.IdUsuarioLector <= 0)
                {
                    MessageBox.Show("Error: IdUsuarioLector no válido: " + SesionUsuario.IdUsuarioLector,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = @"SELECT P.IdPrestamo, L.Titulo as Libro, P.FechaPrestamo, 
                                P.FechaDevolucionEstimada, P.Estado,
                                DATEDIFF(DAY, GETDATE(), P.FechaDevolucionEstimada) as [Días Restantes]
                                FROM Prestamos P
                                INNER JOIN Libros L ON P.IdLibro = L.IdLibro
                                WHERE P.IdUsuario = @IdUsuario AND P.Estado = 'Prestado'
                                ORDER BY P.FechaDevolucionEstimada";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdUsuario", SesionUsuario.IdUsuarioLector);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvMisPrestamos.DataSource = dt;
                dgvMisPrestamos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvMisPrestamos.Columns["IdPrestamo"] != null)
                    dgvMisPrestamos.Columns["IdPrestamo"].Visible = false;

                // Colorear según días restantes
                foreach (DataGridViewRow row in dgvMisPrestamos.Rows)
                {
                    if (row.Cells["Días Restantes"].Value != null)
                    {
                        int diasRestantes = Convert.ToInt32(row.Cells["Días Restantes"].Value);
                        if (diasRestantes < 0)
                            row.DefaultCellStyle.BackColor = Color.LightCoral; // Retrasado
                        else if (diasRestantes <= 3)
                            row.DefaultCellStyle.BackColor = Color.LightYellow; // Por vencer
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar préstamos: " + ex.Message +
                    "\nIdUsuarioLector: " + SesionUsuario.IdUsuarioLector,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarMisSolicitudes()
        {
            try
            {
                // VERIFICACIÓN antes de hacer la consulta
                if (SesionUsuario.IdUsuarioLector <= 0)
                {
                    MessageBox.Show("Error: IdUsuarioLector no válido: " + SesionUsuario.IdUsuarioLector,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = @"SELECT S.IdSolicitud, L.Titulo as Libro, S.FechaSolicitud, 
                                S.DiasRequeridos, S.Estado, S.Observaciones
                                FROM SolicitudesPrestamo S
                                INNER JOIN Libros L ON S.IdLibro = L.IdLibro
                                WHERE S.IdUsuario = @IdUsuario
                                ORDER BY S.FechaSolicitud DESC";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdUsuario", SesionUsuario.IdUsuarioLector);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvMisSolicitudes.DataSource = dt;
                dgvMisSolicitudes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvMisSolicitudes.Columns["IdSolicitud"] != null)
                    dgvMisSolicitudes.Columns["IdSolicitud"].Visible = false;

                // Colorear según estado
                foreach (DataGridViewRow row in dgvMisSolicitudes.Rows)
                {
                    if (row.Cells["Estado"].Value != null)
                    {
                        string estado = row.Cells["Estado"].Value.ToString();
                        if (estado == "Aprobada")
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                        else if (estado == "Rechazada")
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                        else if (estado == "Pendiente")
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar solicitudes: " + ex.Message +
                    "\nIdUsuarioLector: " + SesionUsuario.IdUsuarioLector,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarCatalogo_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBuscarCatalogo.Text))
                {
                    CargarCatalogo();
                    return;
                }

                SqlConnection con = ConexionBD.ObtenerConexion();

                // Verificar si el stored procedure existe, si no usar query normal
                string query = @"SELECT IdLibro, Titulo, Autor, Editorial, Categoria, 
                                AnioPublicacion as Año, CantidadDisponible as Disponibles,
                                CASE WHEN CantidadDisponible > 0 THEN 'Disponible' ELSE 'No Disponible' END as Estado
                                FROM Libros 
                                WHERE Estado = 1 AND 
                                (Titulo LIKE @Criterio OR Autor LIKE @Criterio OR 
                                 Editorial LIKE @Criterio OR Categoria LIKE @Criterio)
                                ORDER BY Titulo";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Criterio", "%" + txtBuscarCatalogo.Text.Trim() + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvCatalogo.DataSource = dt;

                if (dgvCatalogo.Columns["IdLibro"] != null)
                    dgvCatalogo.Columns["IdLibro"].Visible = false;

                foreach (DataGridViewRow row in dgvCatalogo.Rows)
                {
                    if (row.Cells["Disponibles"].Value != null &&
                        Convert.ToInt32(row.Cells["Disponibles"].Value) == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron libros con ese criterio de búsqueda.",
                        "Sin Resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en búsqueda: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSolicitarPrestamo_Click(object sender, EventArgs e)
        {
            if (dgvCatalogo.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un libro del catálogo", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idLibro = Convert.ToInt32(dgvCatalogo.SelectedRows[0].Cells["IdLibro"].Value);
            string tituloLibro = dgvCatalogo.SelectedRows[0].Cells["Titulo"].Value.ToString();
            int disponibles = Convert.ToInt32(dgvCatalogo.SelectedRows[0].Cells["Disponibles"].Value);

            if (disponibles == 0)
            {
                MessageBox.Show("Este libro no está disponible actualmente", "No Disponible",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                FormSolicitarPrestamo formSolicitud = new FormSolicitarPrestamo(idLibro, tituloLibro);
                if (formSolicitud.ShowDialog() == DialogResult.OK)
                {
                    CargarCatalogo();
                    CargarMisSolicitudes();
                    MessageBox.Show("Solicitud enviada correctamente. Un bibliotecario la revisará pronto.",
                        "Solicitud Enviada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir formulario de solicitud: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarCatalogo();
            CargarMisPrestamos();
            CargarMisSolicitudes();
            MessageBox.Show("Información actualizada", "Actualizar",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void FormPortalLector_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}