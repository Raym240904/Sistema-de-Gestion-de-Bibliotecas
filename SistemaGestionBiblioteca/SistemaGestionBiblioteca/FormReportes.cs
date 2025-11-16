using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SistemaGestionBiblioteca
{
    public partial class FormReportes : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public FormReportes()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormReportes_Load(object sender, EventArgs e)
        {
            // Inicializar fechas
            dtpFechaInicio.Value = DateTime.Now.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Now;
        }

        private void btnLibrosMasPopulares_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                SqlCommand cmd = new SqlCommand("sp_LibrosMasPopulares", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvReportes.DataSource = dt;
                dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                lblTituloReporte.Text = "REPORTE: LIBROS MÁS POPULARES";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte: " + ex.Message);
            }
        }

        private void btnReporteMultas_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                SqlCommand cmd = new SqlCommand("sp_ReporteMultas", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvReportes.DataSource = dt;
                dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                lblTituloReporte.Text = "REPORTE: CONTROL DE MULTAS Y RETRASOS";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte: " + ex.Message);
            }
        }

        private void btnReportePorFechas_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = @"SELECT 
                                    P.IdPrestamo,
                                    U.NombreCompleto as Usuario,
                                    L.Titulo as Libro,
                                    P.FechaPrestamo,
                                    P.FechaDevolucionEstimada,
                                    P.FechaDevolucionReal,
                                    P.Estado,
                                    P.MultaDias as [Días Retraso],
                                    P.MontoMulta as Multa
                                FROM Prestamos P
                                INNER JOIN Usuarios U ON P.IdUsuario = U.IdUsuario
                                INNER JOIN Libros L ON P.IdLibro = L.IdLibro
                                WHERE P.FechaPrestamo BETWEEN @FechaInicio AND @FechaFin
                                ORDER BY P.FechaPrestamo DESC";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FechaInicio", dtpFechaInicio.Value.Date);
                cmd.Parameters.AddWithValue("@FechaFin", dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvReportes.DataSource = dt;
                dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvReportes.Columns["IdPrestamo"].Visible = false;

                lblTituloReporte.Text = $"REPORTE: PRÉSTAMOS DEL {dtpFechaInicio.Value.ToShortDateString()} AL {dtpFechaFin.Value.ToShortDateString()}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte: " + ex.Message);
            }
        }

        private void btnReporteUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = @"SELECT 
                                    U.NombreCompleto as Usuario,
                                    U.DNI,
                                    U.Email,
                                    U.Telefono,
                                    COUNT(P.IdPrestamo) as [Total Préstamos],
                                    SUM(CASE WHEN P.Estado = 'Prestado' THEN 1 ELSE 0 END) as [Préstamos Activos],
                                    SUM(CASE WHEN P.MultaDias > 0 THEN 1 ELSE 0 END) as [Con Retraso],
                                    SUM(P.MontoMulta) as [Total Multas]
                                FROM Usuarios U
                                LEFT JOIN Prestamos P ON U.IdUsuario = P.IdUsuario
                                WHERE U.Estado = 1
                                GROUP BY U.NombreCompleto, U.DNI, U.Email, U.Telefono
                                ORDER BY [Total Préstamos] DESC";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvReportes.DataSource = dt;
                dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                lblTituloReporte.Text = "REPORTE: GESTIÓN DE USUARIOS Y RESERVAS";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte: " + ex.Message);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvReportes.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivo CSV|*.csv";
            saveFileDialog.Title = "Exportar Reporte";
            saveFileDialog.FileName = "Reporte_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.UTF8);

                    // Escribir encabezados
                    for (int i = 0; i < dgvReportes.Columns.Count; i++)
                    {
                        if (dgvReportes.Columns[i].Visible)
                        {
                            sw.Write(dgvReportes.Columns[i].HeaderText);
                            if (i < dgvReportes.Columns.Count - 1)
                                sw.Write(";");
                        }
                    }
                    sw.WriteLine();

                    // Escribir datos
                    foreach (DataGridViewRow row in dgvReportes.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            for (int i = 0; i < dgvReportes.Columns.Count; i++)
                            {
                                if (dgvReportes.Columns[i].Visible)
                                {
                                    sw.Write(row.Cells[i].Value?.ToString() ?? "");
                                    if (i < dgvReportes.Columns.Count - 1)
                                        sw.Write(";");
                                }
                            }
                            sw.WriteLine();
                        }
                    }

                    sw.Close();
                    MessageBox.Show("Reporte exportado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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