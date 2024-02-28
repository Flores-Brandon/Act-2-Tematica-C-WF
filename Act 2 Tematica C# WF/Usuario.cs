using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act_2_Tematica_C__WF
{
    public class Usuario
    {
        public string ID { get; }
        public string Nombre { get; set; }

        public Usuario(string id, string nombre)
        {
            ID = id;
            Nombre = nombre;
        }
    }
}

