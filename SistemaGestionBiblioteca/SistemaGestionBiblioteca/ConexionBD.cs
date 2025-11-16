using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaGestionBiblioteca
{
    public class ConexionBD
    {
        private static string connectionString = "Data Source=DESKTOP-G29AMJ4;Initial Catalog=BibliotecaDB;Integrated Security=True";
        private static SqlConnection conexion;

        public static SqlConnection ObtenerConexion()
        {
            try
            {
                if (conexion == null)
                {
                    conexion = new SqlConnection(connectionString);
                }

                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }

                return conexion;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message,
                    "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static void CerrarConexion()
        {
            try
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar conexión: " + ex.Message);
            }
        }

        public static bool ProbarConexion()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }

    // Clase para manejar la sesión del usuario
    public static class SesionUsuario
    {
        public static int IdUsuarioSistema { get; set; }
        public static string NombreUsuario { get; set; }
        public static string NombreCompleto { get; set; }
        public static string Rol { get; set; }
        public static int IdRol { get; set; }
        public static int? IdUsuarioLector { get; set; } // Para usuarios lectores

        public static bool EsAdministrador()
        {
            return IdRol == 1;
        }

        public static bool EsBibliotecario()
        {
            return IdRol == 2;
        }

        public static bool EsUsuarioLector()
        {
            return IdRol == 3;
        }

        public static void CerrarSesion()
        {
            IdUsuarioSistema = 0;
            NombreUsuario = null;
            NombreCompleto = null;
            Rol = null;
            IdRol = 0;
            IdUsuarioLector = null;
        }
    }
}