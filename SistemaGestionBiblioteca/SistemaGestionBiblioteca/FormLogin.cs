using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SistemaGestionBiblioteca
{
    public partial class FormLogin : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public FormLogin()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtContrasena.UseSystemPasswordChar = true;

            if (!ConexionBD.ProbarConexion())
            {
                MessageBox.Show("No se puede conectar a la base de datos. Verifique la configuración.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show("Por favor complete todos los campos", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = @"SELECT US.IdUsuarioSistema, US.NombreCompleto, R.IdRol, R.NombreRol, U.IdUsuario
                                FROM UsuariosSistema US
                                INNER JOIN Roles R ON US.IdRol = R.IdRol
                                LEFT JOIN Usuarios U ON US.IdUsuarioSistema = U.IdUsuarioSistema
                                WHERE US.Usuario = @Usuario AND US.Contrasena = @Contrasena AND US.Estado = 1";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text.Trim());
                cmd.Parameters.AddWithValue("@Contrasena", txtContrasena.Text);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    // Guardar datos de sesión
                    SesionUsuario.IdUsuarioSistema = Convert.ToInt32(reader["IdUsuarioSistema"]);
                    SesionUsuario.NombreUsuario = txtUsuario.Text.Trim();
                    SesionUsuario.NombreCompleto = reader["NombreCompleto"].ToString();
                    SesionUsuario.IdRol = Convert.ToInt32(reader["IdRol"]);
                    SesionUsuario.Rol = reader["NombreRol"].ToString();

                    if (reader["IdUsuario"] != DBNull.Value)
                    {
                        SesionUsuario.IdUsuarioLector = Convert.ToInt32(reader["IdUsuario"]);
                    }

                    reader.Close();

                    MessageBox.Show($"¡Bienvenido {SesionUsuario.NombreCompleto}!\nRol: {SesionUsuario.Rol}",
                        "Acceso Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir formulario según el rol
                    if (SesionUsuario.EsUsuarioLector())
                    {
                        FormPortalLector portalLector = new FormPortalLector();
                        portalLector.Show();
                    }
                    else
                    {
                        FormMenuPrincipal menu = new FormMenuPrincipal();
                        menu.Show();
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error de Acceso",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContrasena.Clear();
                    txtUsuario.Focus();
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void chkMostrarContrasena_CheckedChanged(object sender, EventArgs e)
        {
            txtContrasena.UseSystemPasswordChar = !chkMostrarContrasena.Checked;
        }
    }
}