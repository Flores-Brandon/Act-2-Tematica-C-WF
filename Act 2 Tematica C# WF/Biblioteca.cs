using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act_2_Tematica_C__WF
{
    public class Biblioteca
    {
        private Dictionary<string, Libro> libros = new Dictionary<string, Libro>(); // Key: ISBN
        private Dictionary<string, Usuario> usuarios = new Dictionary<string, Usuario>(); // Key: ID
        private Dictionary<string, string> prestamos = new Dictionary<string, string>(); // Key: ISBN, Value: ID de usuario

        public void AgregarLibro(Libro libro)
        {
            libros[libro.ISBN] = libro;
        }

        public void AgregarUsuario(Usuario usuario)
        {
            usuarios[usuario.ID] = usuario;
        }

        public bool PrestarLibro(string isbn, string userId)
        {
            if (!libros.ContainsKey(isbn) || !usuarios.ContainsKey(userId))
            {
                return false;
            }

            if (!prestamos.ContainsKey(isbn))
            {
                prestamos[isbn] = userId;
                return true;
            }
            return false;
        }

        public List<KeyValuePair<string, string>> ObtenerPrestamos()
        {
            return new List<KeyValuePair<string, string>>(prestamos);
        }

        public Libro ObtenerLibroPorISBN(string isbn)
        {
            if (libros.ContainsKey(isbn))
                return libros[isbn];
            else
                return null;
        }

        public Usuario ObtenerUsuarioPorID(string id)
        {
            if (usuarios.ContainsKey(id))
                return usuarios[id];
            else
                return null;
        }

        public bool LibroExiste(string isbn)
        {
            return libros.ContainsKey(isbn);
        }

        public bool UsuarioExiste(string id)
        {
            return usuarios.ContainsKey(id);
        }

        public void CargarLibrosDesdeArchivo(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 4)
                        {
                            Libro libro = new Libro(parts[0], parts[1], parts[2], parts[3]);
                            libros[libro.ISBN] = libro;
                        }
                    }
                }
            }
        }

        public void CargarUsuariosDesdeArchivo(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            Usuario usuario = new Usuario(parts[0], parts[1]);
                            usuarios[usuario.ID] = usuario;
                        }
                    }
                }
            }
        }

        public void GuardarLibrosEnArchivo(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var libro in libros.Values)
                {
                    writer.WriteLine($"{libro.ISBN},{libro.Titulo},{libro.Autor},{libro.Categoria}");
                }
            }
        }

        public void GuardarUsuariosEnArchivo(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var usuario in usuarios.Values)
                {
                    writer.WriteLine($"{usuario.ID},{usuario.Nombre}");
                }
            }
        }
    }
}

