using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SistemaGestionBiblioteca
{
    public partial class FormDevoluciones : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private int idPrestamoSeleccionado = 0;

        public FormDevoluciones()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormDevoluciones_Load(object sender, EventArgs e)
        {
            CargarPrestamosActivos();
        }

        private void CargarPrestamosActivos()
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = @"SELECT P.IdPrestamo, U.NombreCompleto as Usuario, U.DNI, 
                                L.Titulo as Libro, P.FechaPrestamo, P.FechaDevolucionEstimada, 
                                DATEDIFF(DAY, P.FechaDevolucionEstimada, GETDATE()) as DiasRetraso,
                                CASE WHEN GETDATE() > P.FechaDevolucionEstimada 
                                THEN 'RETRASADO' ELSE 'A TIEMPO' END as Situacion
                                FROM Prestamos P 
                                INNER JOIN Usuarios U ON P.IdUsuario = U.IdUsuario 
                                INNER JOIN Libros L ON P.IdLibro = L.IdLibro 
                                WHERE P.Estado = 'Prestado' 
                                ORDER BY P.FechaDevolucionEstimada";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPrestamosActivos.DataSource = dt;
                dgvPrestamosActivos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPrestamosActivos.Columns["IdPrestamo"].Visible = false;

                // Colorear filas retrasadas
                foreach (DataGridViewRow row in dgvPrestamosActivos.Rows)
                {
                    if (row.Cells["Situacion"].Value.ToString() == "RETRASADO")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar préstamos: " + ex.Message);
            }
        }

        private void dgvPrestamosActivos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPrestamosActivos.Rows[e.RowIndex];
                idPrestamoSeleccionado = Convert.ToInt32(row.Cells["IdPrestamo"].Value);

                lblUsuarioInfo.Text = row.Cells["Usuario"].Value.ToString();
                lblLibroInfo.Text = row.Cells["Libro"].Value.ToString();
                lblFechaPrestamoInfo.Text = Convert.ToDateTime(row.Cells["FechaPrestamo"].Value).ToShortDateString();
                lblFechaDevolucionInfo.Text = Convert.ToDateTime(row.Cells["FechaDevolucionEstimada"].Value).ToShortDateString();

                int diasRetraso = Convert.ToInt32(row.Cells["DiasRetraso"].Value);
                if (diasRetraso > 0)
                {
                    lblDiasRetraso.Text = diasRetraso.ToString() + " días";
                    lblDiasRetraso.ForeColor = Color.Red;
                    decimal multa = diasRetraso * 2.00m;
                    lblMultaCalculada.Text = "S/ " + multa.ToString("0.00");
                    lblMultaCalculada.ForeColor = Color.Red;
                }
                else
                {
                    lblDiasRetraso.Text = "0 días (A tiempo)";
                    lblDiasRetraso.ForeColor = Color.Green;
                    lblMultaCalculada.Text = "S/ 0.00";
                    lblMultaCalculada.ForeColor = Color.Green;
                }
            }
        }

        private void btnRegistrarDevolucion_Click(object sender, EventArgs e)
        {
            if (idPrestamoSeleccionado == 0)
            {
                MessageBox.Show("Por favor seleccione un préstamo para devolver", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de registrar esta devolución?",
                "Confirmar Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con = ConexionBD.ObtenerConexion();
                    SqlCommand cmd = new SqlCommand("sp_DevolverLibro", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdPrestamo", idPrestamoSeleccionado);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int diasRetraso = Convert.ToInt32(reader["DiasRetraso"]);
                        decimal multa = Convert.ToDecimal(reader["Multa"]);
                        reader.Close();

                        string mensaje = "Devolución registrada correctamente.\n\n";
                        if (diasRetraso > 0)
                        {
                            mensaje += $"Días de retraso: {diasRetraso}\n";
                            mensaje += $"Multa a pagar: S/ {multa:0.00}";
                            MessageBox.Show(mensaje, "Devolución con Multa",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            mensaje += "Devolución a tiempo. Sin multa.";
                            MessageBox.Show(mensaje, "Devolución Exitosa",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        LimpiarSeleccion();
                        CargarPrestamosActivos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar devolución: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                CargarPrestamosActivos();
                return;
            }

            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = @"SELECT P.IdPrestamo, U.NombreCompleto as Usuario, U.DNI, 
                                L.Titulo as Libro, P.FechaPrestamo, P.FechaDevolucionEstimada, 
                                DATEDIFF(DAY, P.FechaDevolucionEstimada, GETDATE()) as DiasRetraso,
                                CASE WHEN GETDATE() > P.FechaDevolucionEstimada 
                                THEN 'RETRASADO' ELSE 'A TIEMPO' END as Situacion
                                FROM Prestamos P 
                                INNER JOIN Usuarios U ON P.IdUsuario = U.IdUsuario 
                                INNER JOIN Libros L ON P.IdLibro = L.IdLibro 
                                WHERE P.Estado = 'Prestado' AND 
                                (U.NombreCompleto LIKE @Criterio OR U.DNI LIKE @Criterio OR L.Titulo LIKE @Criterio)
                                ORDER BY P.FechaDevolucionEstimada";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Criterio", "%" + txtBuscar.Text.Trim() + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPrestamosActivos.DataSource = dt;
                dgvPrestamosActivos.Columns["IdPrestamo"].Visible = false;

                foreach (DataGridViewRow row in dgvPrestamosActivos.Rows)
                {
                    if (row.Cells["Situacion"].Value.ToString() == "RETRASADO")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en búsqueda: " + ex.Message);
            }
        }

        private void chkSoloRetrasados_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = @"SELECT P.IdPrestamo, U.NombreCompleto as Usuario, U.DNI, 
                                L.Titulo as Libro, P.FechaPrestamo, P.FechaDevolucionEstimada, 
                                DATEDIFF(DAY, P.FechaDevolucionEstimada, GETDATE()) as DiasRetraso,
                                CASE WHEN GETDATE() > P.FechaDevolucionEstimada 
                                THEN 'RETRASADO' ELSE 'A TIEMPO' END as Situacion
                                FROM Prestamos P 
                                INNER JOIN Usuarios U ON P.IdUsuario = U.IdUsuario 
                                INNER JOIN Libros L ON P.IdLibro = L.IdLibro 
                                WHERE P.Estado = 'Prestado'";

                if (chkSoloRetrasados.Checked)
                {
                    query += " AND GETDATE() > P.FechaDevolucionEstimada";
                }

                query += " ORDER BY P.FechaDevolucionEstimada";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPrestamosActivos.DataSource = dt;
                dgvPrestamosActivos.Columns["IdPrestamo"].Visible = false;

                foreach (DataGridViewRow row in dgvPrestamosActivos.Rows)
                {
                    if (row.Cells["Situacion"].Value.ToString() == "RETRASADO")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }
        }

        private void LimpiarSeleccion()
        {
            idPrestamoSeleccionado = 0;
            lblUsuarioInfo.Text = "-";
            lblLibroInfo.Text = "-";
            lblFechaPrestamoInfo.Text = "-";
            lblFechaDevolucionInfo.Text = "-";
            lblDiasRetraso.Text = "-";
            lblMultaCalculada.Text = "-";
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