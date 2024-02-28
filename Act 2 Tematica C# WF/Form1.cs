using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Act_2_Tematica_C__WF
{
    public partial class MainForm : Form
    {
        private Biblioteca biblioteca = new Biblioteca();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Cargar libros y usuarios desde los archivos
            biblioteca.CargarLibrosDesdeArchivo("libros.txt");
            biblioteca.CargarUsuariosDesdeArchivo("usuarios.txt");
        }

        private void btnPrestarLibro_Click(object sender, EventArgs e)
        {
            string isbn = txtISBN.Text;
            string userId = txtUserID.Text;

            if (!string.IsNullOrEmpty(isbn) && !string.IsNullOrEmpty(userId))
            {
                // Verificar si el libro existe
                if (!biblioteca.LibroExiste(isbn))
                {
                    // Si el libro no existe, usar los valores proporcionados por el usuario
                    string tituloLibro = txtTituloLibro.Text;
                    string autorLibro = txtAutorLibro.Text;
                    string categoriaLibro = txtCategoriaLibro.Text;

                    biblioteca.AgregarLibro(new Libro(isbn, tituloLibro, autorLibro, categoriaLibro));
                }

                // Verificar si el usuario existe
                if (!biblioteca.UsuarioExiste(userId))
                {
                    // Si el usuario no existe, usar el nombre proporcionado por el usuario
                    string nombreUsuario = txtNombreUsuario.Text;

                    biblioteca.AgregarUsuario(new Usuario(userId, nombreUsuario));
                }

                // Intentar prestar el libro
                if (biblioteca.PrestarLibro(isbn, userId))
                {
                    MessageBox.Show("Libro prestado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo prestar el libro. Verifique el ISBN y el ID del usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor ingrese el ISBN del libro y el ID de usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMostrarPrestamos_Click(object sender, EventArgs e)
        {
            lstPrestamos.Items.Clear();
            foreach (var prestamo in biblioteca.ObtenerPrestamos())
            {
                string isbn = prestamo.Key;
                string userId = prestamo.Value;
                string libroTitulo = "";
                string usuarioNombre = "";

                // Verificar si el libro y el usuario existen en la biblioteca
                if (biblioteca.LibroExiste(isbn))
                {
                    libroTitulo = biblioteca.ObtenerLibroPorISBN(isbn).Titulo;
                }
                else
                {
                    libroTitulo = "Libro no encontrado";
                }

                if (biblioteca.UsuarioExiste(userId))
                {
                    usuarioNombre = biblioteca.ObtenerUsuarioPorID(userId).Nombre;
                }
                else
                {
                    usuarioNombre = "Usuario no encontrado";
                }

                lstPrestamos.Items.Add($"Usuario: {usuarioNombre}, Libro: {libroTitulo}");
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Guardar libros y usuarios en archivos antes de cerrar la aplicación
            biblioteca.GuardarLibrosEnArchivo("libros.txt");
            biblioteca.GuardarUsuariosEnArchivo("usuarios.txt");
        }

        private void txtISBN_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
