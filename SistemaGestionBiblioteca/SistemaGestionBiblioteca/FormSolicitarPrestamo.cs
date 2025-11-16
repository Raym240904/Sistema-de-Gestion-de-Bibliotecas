using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaGestionBiblioteca
{
    public partial class FormSolicitarPrestamo : Form
    {
        private int idLibro;
        private string tituloLibro;

        public FormSolicitarPrestamo(int idLibro, string tituloLibro)
        {
            InitializeComponent();
            this.idLibro = idLibro;
            this.tituloLibro = tituloLibro;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void FormSolicitarPrestamo_Load(object sender, EventArgs e)
        {
            lblLibroSeleccionado.Text = $"Libro: {tituloLibro}";
            numDiasPrestamo.Value = 15; // Por defecto 15 días
        }

        private void btnEnviarSolicitud_Click(object sender, EventArgs e)
        {
            if (numDiasPrestamo.Value < 1 || numDiasPrestamo.Value > 30)
            {
                MessageBox.Show("Los días de préstamo deben estar entre 1 y 30", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // VALIDACIÓN CRÍTICA: Verificar que el usuario lector tenga IdUsuario
            if (!SesionUsuario.IdUsuarioLector.HasValue || SesionUsuario.IdUsuarioLector.Value == 0)
            {
                MessageBox.Show("Error: No se pudo identificar su cuenta de usuario.\n\nPor favor, cierre sesión y vuelva a iniciar sesión.",
                    "Error de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();

                // Verificar nuevamente en la base de datos
                string queryVerificar = "SELECT IdUsuario FROM Usuarios WHERE IdUsuarioSistema = @IdUsuarioSistema";
                SqlCommand cmdVerificar = new SqlCommand(queryVerificar, con);
                cmdVerificar.Parameters.AddWithValue("@IdUsuarioSistema", SesionUsuario.IdUsuarioSistema);

                object resultado = cmdVerificar.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value)
                {
                    MessageBox.Show("Su cuenta no está vinculada correctamente.\n\nContacte al administrador del sistema.",
                        "Error de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int idUsuarioReal = Convert.ToInt32(resultado);

                // Ahora sí, crear la solicitud
                SqlCommand cmd = new SqlCommand("sp_CrearSolicitudPrestamo", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", idUsuarioReal);
                cmd.Parameters.AddWithValue("@IdLibro", idLibro);
                cmd.Parameters.AddWithValue("@DiasRequeridos", (int)numDiasPrestamo.Value);
                cmd.Parameters.AddWithValue("@Observaciones", txtObservaciones.Text.Trim());

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int idSolicitud = Convert.ToInt32(reader["IdSolicitud"]);
                    string mensaje = reader["Mensaje"].ToString();

                    if (idSolicitud > 0)
                    {
                        MessageBox.Show("Solicitud enviada correctamente.\n\nUn bibliotecario la revisará pronto.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al crear la solicitud: " + mensaje,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar solicitud: " + ex.Message + "\n\nDetalles técnicos:\n" + ex.StackTrace,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}