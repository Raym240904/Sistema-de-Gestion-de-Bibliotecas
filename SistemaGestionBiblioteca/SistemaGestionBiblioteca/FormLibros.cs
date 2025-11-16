using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SistemaGestionBiblioteca
{
    public partial class FormLibros : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private int idLibroSeleccionado = 0;

        public FormLibros()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormLibros_Load(object sender, EventArgs e)
        {
            CargarLibros();
            LimpiarCampos();
        }

        private void CargarLibros()
        {
            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                string query = @"SELECT IdLibro, Titulo, Autor, Editorial, ISBN, 
                                AnioPublicacion as Año, Categoria, CantidadTotal as Total, 
                                CantidadDisponible as Disponibles, Ubicacion 
                                FROM Libros WHERE Estado = 1 ORDER BY Titulo";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvLibros.DataSource = dt;
                dgvLibros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvLibros.Columns["IdLibro"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar libros: " + ex.Message, "Error",
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

                if (idLibroSeleccionado == 0)
                {
                    query = @"INSERT INTO Libros (Titulo, Autor, Editorial, ISBN, AnioPublicacion, 
                             Categoria, CantidadTotal, CantidadDisponible, Ubicacion) 
                             VALUES (@Titulo, @Autor, @Editorial, @ISBN, @Anio, @Categoria, 
                             @Total, @Disponible, @Ubicacion)";
                }
                else
                {
                    query = @"UPDATE Libros SET Titulo = @Titulo, Autor = @Autor, 
                             Editorial = @Editorial, ISBN = @ISBN, AnioPublicacion = @Anio, 
                             Categoria = @Categoria, CantidadTotal = @Total, 
                             CantidadDisponible = @Disponible, Ubicacion = @Ubicacion 
                             WHERE IdLibro = @Id";
                }

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Titulo", txtTitulo.Text.Trim());
                cmd.Parameters.AddWithValue("@Autor", txtAutor.Text.Trim());
                cmd.Parameters.AddWithValue("@Editorial", txtEditorial.Text.Trim());
                cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text.Trim());
                cmd.Parameters.AddWithValue("@Anio", int.Parse(txtAnio.Text));
                cmd.Parameters.AddWithValue("@Categoria", cboCategoria.Text);
                cmd.Parameters.AddWithValue("@Total", int.Parse(txtCantidadTotal.Text));
                cmd.Parameters.AddWithValue("@Disponible", int.Parse(txtCantidadDisponible.Text));
                cmd.Parameters.AddWithValue("@Ubicacion", txtUbicacion.Text.Trim());

                if (idLibroSeleccionado != 0)
                    cmd.Parameters.AddWithValue("@Id", idLibroSeleccionado);

                cmd.ExecuteNonQuery();

                MessageBox.Show(idLibroSeleccionado == 0 ?
                    "Libro registrado correctamente" : "Libro actualizado correctamente",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarLibros();
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
            if (idLibroSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un libro para eliminar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de eliminar este libro?",
                "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlConnection con = ConexionBD.ObtenerConexion();
                    SqlCommand cmd = new SqlCommand("UPDATE Libros SET Estado = 0 WHERE IdLibro = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", idLibroSeleccionado);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Libro eliminado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarLibros();
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvLibros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLibros.Rows[e.RowIndex];
                idLibroSeleccionado = Convert.ToInt32(row.Cells["IdLibro"].Value);

                txtTitulo.Text = row.Cells["Titulo"].Value.ToString();
                txtAutor.Text = row.Cells["Autor"].Value.ToString();
                txtEditorial.Text = row.Cells["Editorial"].Value.ToString();
                txtISBN.Text = row.Cells["ISBN"].Value.ToString();
                txtAnio.Text = row.Cells["Año"].Value.ToString();
                cboCategoria.Text = row.Cells["Categoria"].Value.ToString();
                txtCantidadTotal.Text = row.Cells["Total"].Value.ToString();
                txtCantidadDisponible.Text = row.Cells["Disponibles"].Value.ToString();
                txtUbicacion.Text = row.Cells["Ubicacion"].Value.ToString();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarLibro.Text))
            {
                CargarLibros();
                return;
            }

            try
            {
                SqlConnection con = ConexionBD.ObtenerConexion();
                SqlCommand cmd = new SqlCommand("sp_BuscarLibros", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Criterio", txtBuscarLibro.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvLibros.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en búsqueda: " + ex.Message);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text) ||
                string.IsNullOrWhiteSpace(txtAutor.Text) ||
                string.IsNullOrWhiteSpace(txtEditorial.Text) ||
                string.IsNullOrWhiteSpace(txtISBN.Text) ||
                string.IsNullOrWhiteSpace(txtAnio.Text) ||
                string.IsNullOrWhiteSpace(cboCategoria.Text) ||
                string.IsNullOrWhiteSpace(txtCantidadTotal.Text) ||
                string.IsNullOrWhiteSpace(txtCantidadDisponible.Text) ||
                string.IsNullOrWhiteSpace(txtUbicacion.Text))
            {
                MessageBox.Show("Por favor complete todos los campos", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtAnio.Text, out int anio) || anio < 1000 || anio > DateTime.Now.Year)
            {
                MessageBox.Show("Ingrese un año válido", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtCantidadTotal.Text, out int total) || total < 1)
            {
                MessageBox.Show("La cantidad total debe ser mayor a 0", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtCantidadDisponible.Text, out int disponible) || disponible < 0 || disponible > total)
            {
                MessageBox.Show("La cantidad disponible debe estar entre 0 y la cantidad total",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            idLibroSeleccionado = 0;
            txtTitulo.Clear();
            txtAutor.Clear();
            txtEditorial.Clear();
            txtISBN.Clear();
            txtAnio.Clear();
            cboCategoria.SelectedIndex = -1;
            txtCantidadTotal.Clear();
            txtCantidadDisponible.Clear();
            txtUbicacion.Clear();
            txtBuscarLibro.Clear();
            txtTitulo.Focus();
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