using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SistemaGestionBiblioteca
{
    public partial class FormGestionSolicitudes : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private int idSolicitudSeleccionada = 0;

        public FormGestionSolicitudes()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormGestionSolicitudes_Load(object sender, EventArgs e)
        {
            CargarSolicitudesPendientes();
        }

        private void CargarSolicitudesPendientes()
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = @"SELECT S.IdSolicitud, U.NombreCompleto as Usuario, U.DNI, 
                                L.Titulo as Libro, L.CantidadDisponible as Disponibles,
                                S.FechaSolicitud, S.DiasRequeridos, S.Estado, S.Observaciones
                                FROM SolicitudesPrestamo S
                                INNER JOIN Usuarios U ON S.IdUsuario = U.IdUsuario
                                INNER JOIN Libros L ON S.IdLibro = L.IdLibro
                                WHERE S.Estado = 'Pendiente'
                                ORDER BY S.FechaSolicitud";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvSolicitudes.DataSource = dt;
                dgvSolicitudes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvSolicitudes.Columns["IdSolicitud"].Visible = false;

                foreach (DataGridViewRow row in dgvSolicitudes.Rows)
                {
                    if (Convert.ToInt32(row.Cells["Disponibles"].Value) == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar solicitudes: " + ex.Message);
            }
        }

        private void dgvSolicitudes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSolicitudes.Rows[e.RowIndex];
                idSolicitudSeleccionada = Convert.ToInt32(row.Cells["IdSolicitud"].Value);

                lblUsuarioInfo.Text = row.Cells["Usuario"].Value.ToString();
                lblLibroInfo.Text = row.Cells["Libro"].Value.ToString();
                lblDiasRequeridos.Text = row.Cells["DiasRequeridos"].Value.ToString() + " días";
                lblDisponibles.Text = row.Cells["Disponibles"].Value.ToString();
                txtObservacionesRespuesta.Text = row.Cells["Observaciones"].Value?.ToString() ?? "";
            }
        }

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            if (idSolicitudSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una solicitud", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de aprobar esta solicitud y crear el préstamo?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con = ConexionBD.ObtenerConexion();
                    SqlCommand cmd = new SqlCommand("sp_AprobarSolicitudPrestamo", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdSolicitud", idSolicitudSeleccionada);
                    cmd.Parameters.AddWithValue("@IdBibliotecario", SesionUsuario.IdUsuarioSistema);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Solicitud aprobada y préstamo creado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarSeleccion();
                    CargarSolicitudesPendientes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al aprobar: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            if (idSolicitudSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una solicitud", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de rechazar esta solicitud?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con = ConexionBD.ObtenerConexion();
                    string query = @"UPDATE SolicitudesPrestamo 
                                    SET Estado = 'Rechazada', 
                                        IdBibliotecarioRevisa = @IdBibliotecario,
                                        FechaRevision = GETDATE(),
                                        Observaciones = @Observaciones
                                    WHERE IdSolicitud = @IdSolicitud";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@IdSolicitud", idSolicitudSeleccionada);
                    cmd.Parameters.AddWithValue("@IdBibliotecario", SesionUsuario.IdUsuarioSistema);
                    cmd.Parameters.AddWithValue("@Observaciones", txtObservacionesRespuesta.Text.Trim());

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Solicitud rechazada", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarSeleccion();
                    CargarSolicitudesPendientes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al rechazar: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LimpiarSeleccion()
        {
            idSolicitudSeleccionada = 0;
            lblUsuarioInfo.Text = "-";
            lblLibroInfo.Text = "-";
            lblDiasRequeridos.Text = "-";
            lblDisponibles.Text = "-";
            txtObservacionesRespuesta.Clear();
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