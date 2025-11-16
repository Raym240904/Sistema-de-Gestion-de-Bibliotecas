using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SistemaGestionBiblioteca
{
    public partial class FormGestionRoles : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private int idUsuarioSeleccionado = 0;

        public FormGestionRoles()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormGestionRoles_Load(object sender, EventArgs e)
        {
            if (!SesionUsuario.EsAdministrador())
            {
                MessageBox.Show("Solo el Administrador puede acceder a esta sección",
                    "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            CargarRoles();
            CargarUsuariosSistema();
            LimpiarCampos();
        }

        private void CargarRoles()
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = "SELECT IdRol, NombreRol FROM Roles";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboRol.DataSource = dt;
                cboRol.DisplayMember = "NombreRol";
                cboRol.ValueMember = "IdRol";
                cboRol.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar roles: " + ex.Message);
            }
        }

        private void CargarUsuariosSistema()
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = @"SELECT US.IdUsuarioSistema, US.Usuario, US.NombreCompleto, 
                                R.NombreRol as Rol, US.Email, US.FechaCreacion,
                                CASE WHEN US.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END as Estado
                                FROM UsuariosSistema US
                                INNER JOIN Roles R ON US.IdRol = R.IdRol
                                ORDER BY US.NombreCompleto";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvUsuariosSistema.DataSource = dt;
                dgvUsuariosSistema.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvUsuariosSistema.Columns["IdUsuarioSistema"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query;

                if (idUsuarioSeleccionado == 0)
                {
                    // Verificar si el usuario ya existe
                    SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM UsuariosSistema WHERE Usuario = @Usuario", con);
                    cmdCheck.Parameters.AddWithValue("@Usuario", txtUsuario.Text.Trim());
                    int existe = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (existe > 0)
                    {
                        MessageBox.Show("El nombre de usuario ya existe", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    query = @"INSERT INTO UsuariosSistema (Usuario, Contrasena, IdRol, NombreCompleto, Email) 
                             VALUES (@Usuario, @Contrasena, @IdRol, @NombreCompleto, @Email)";
                }
                else
                {
                    query = @"UPDATE UsuariosSistema 
                             SET Usuario = @Usuario, Contrasena = @Contrasena, IdRol = @IdRol, 
                                 NombreCompleto = @NombreCompleto, Email = @Email 
                             WHERE IdUsuarioSistema = @Id";
                }

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text.Trim());
                cmd.Parameters.AddWithValue("@Contrasena", txtContrasena.Text);
                cmd.Parameters.AddWithValue("@IdRol", cboRol.SelectedValue);
                cmd.Parameters.AddWithValue("@NombreCompleto", txtNombreCompleto.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());

                if (idUsuarioSeleccionado != 0)
                    cmd.Parameters.AddWithValue("@Id", idUsuarioSeleccionado);

                cmd.ExecuteNonQuery();

                MessageBox.Show(idUsuarioSeleccionado == 0 ?
                    "Usuario registrado correctamente" : "Usuario actualizado correctamente",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarUsuariosSistema();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un usuario para eliminar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (idUsuarioSeleccionado == SesionUsuario.IdUsuarioSistema)
            {
                MessageBox.Show("No puede eliminar su propio usuario", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de desactivar este usuario?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con = ConexionBD.ObtenerConexion();
                    SqlCommand cmd = new SqlCommand("UPDATE UsuariosSistema SET Estado = 0 WHERE IdUsuarioSistema = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", idUsuarioSeleccionado);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Usuario desactivado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarUsuariosSistema();
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvUsuariosSistema_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsuariosSistema.Rows[e.RowIndex];
                idUsuarioSeleccionado = Convert.ToInt32(row.Cells["IdUsuarioSistema"].Value);

                txtUsuario.Text = row.Cells["Usuario"].Value.ToString();
                txtNombreCompleto.Text = row.Cells["NombreCompleto"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtContrasena.Text = "********"; // Por seguridad no mostramos la contraseña real

                string rol = row.Cells["Rol"].Value.ToString();
                for (int i = 0; i < cboRol.Items.Count; i++)
                {
                    if (((DataRowView)cboRol.Items[i])["NombreRol"].ToString() == rol)
                    {
                        cboRol.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                CargarUsuariosSistema();
                return;
            }

            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = @"SELECT US.IdUsuarioSistema, US.Usuario, US.NombreCompleto, 
                                R.NombreRol as Rol, US.Email, US.FechaCreacion,
                                CASE WHEN US.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END as Estado
                                FROM UsuariosSistema US
                                INNER JOIN Roles R ON US.IdRol = R.IdRol
                                WHERE US.Usuario LIKE @Criterio OR US.NombreCompleto LIKE @Criterio OR US.Email LIKE @Criterio
                                ORDER BY US.NombreCompleto";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Criterio", "%" + txtBuscar.Text.Trim() + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvUsuariosSistema.DataSource = dt;
                dgvUsuariosSistema.Columns["IdUsuarioSistema"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en búsqueda: " + ex.Message);
            }
        }

        private void chkMostrarContrasena_CheckedChanged(object sender, EventArgs e)
        {
            txtContrasena.UseSystemPasswordChar = !chkMostrarContrasena.Checked;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtContrasena.Text) ||
                string.IsNullOrWhiteSpace(txtNombreCompleto.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                cboRol.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor complete todos los campos", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtUsuario.Text.Length < 4)
            {
                MessageBox.Show("El usuario debe tener al menos 4 caracteres", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtContrasena.Text.Length < 6 && txtContrasena.Text != "********")
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Ingrese un email válido", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            idUsuarioSeleccionado = 0;
            txtUsuario.Clear();
            txtContrasena.Clear();
            txtNombreCompleto.Clear();
            txtEmail.Clear();
            txtBuscar.Clear();
            cboRol.SelectedIndex = -1;
            chkMostrarContrasena.Checked = false;
            txtUsuario.Focus();
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