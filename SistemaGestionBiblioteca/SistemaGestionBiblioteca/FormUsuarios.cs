using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SistemaGestionBiblioteca
{
    public partial class FormUsuarios : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private int idUsuarioSeleccionado = 0;

        public FormUsuarios()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
            LimpiarCampos();
        }

        private void CargarUsuarios()
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = @"SELECT IdUsuario, NombreCompleto as Nombre, DNI, Telefono, 
                                Email, Direccion, FechaRegistro as Registro 
                                FROM Usuarios WHERE Estado = 1 ORDER BY NombreCompleto";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvUsuarios.DataSource = dt;
                dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvUsuarios.Columns["IdUsuario"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    query = @"INSERT INTO Usuarios (NombreCompleto, DNI, Telefono, Email, Direccion) 
                             VALUES (@Nombre, @DNI, @Telefono, @Email, @Direccion)";
                }
                else
                {
                    query = @"UPDATE Usuarios SET NombreCompleto = @Nombre, DNI = @DNI, 
                             Telefono = @Telefono, Email = @Email, Direccion = @Direccion 
                             WHERE IdUsuario = @Id";
                }

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                cmd.Parameters.AddWithValue("@DNI", txtDNI.Text.Trim());
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text.Trim());

                if (idUsuarioSeleccionado != 0)
                    cmd.Parameters.AddWithValue("@Id", idUsuarioSeleccionado);

                cmd.ExecuteNonQuery();

                MessageBox.Show(idUsuarioSeleccionado == 0 ?
                    "Usuario registrado correctamente" : "Usuario actualizado correctamente",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarUsuarios();
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

            DialogResult result = MessageBox.Show("¿Está seguro de eliminar este usuario?",
                "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con = ConexionBD.ObtenerConexion();
                    SqlCommand cmd = new SqlCommand("UPDATE Usuarios SET Estado = 0 WHERE IdUsuario = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", idUsuarioSeleccionado);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Usuario eliminado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarUsuarios();
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsuarios.Rows[e.RowIndex];
                idUsuarioSeleccionado = Convert.ToInt32(row.Cells["IdUsuario"].Value);

                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtDNI.Text = row.Cells["DNI"].Value.ToString();
                txtTelefono.Text = row.Cells["Telefono"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtDireccion.Text = row.Cells["Direccion"].Value.ToString();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarUsuario.Text))
            {
                CargarUsuarios();
                return;
            }

            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = @"SELECT IdUsuario, NombreCompleto as Nombre, DNI, Telefono, 
                                Email, Direccion, FechaRegistro as Registro 
                                FROM Usuarios WHERE Estado = 1 AND 
                                (NombreCompleto LIKE @Criterio OR DNI LIKE @Criterio OR Email LIKE @Criterio) 
                                ORDER BY NombreCompleto";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Criterio", "%" + txtBuscarUsuario.Text.Trim() + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvUsuarios.DataSource = dt;
                dgvUsuarios.Columns["IdUsuario"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en búsqueda: " + ex.Message);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDNI.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Por favor complete todos los campos", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtDNI.Text.Length < 8)
            {
                MessageBox.Show("El DNI debe tener al menos 8 caracteres", "Advertencia",
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
            txtNombre.Clear();
            txtDNI.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
            txtBuscarUsuario.Clear();
            txtNombre.Focus();
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