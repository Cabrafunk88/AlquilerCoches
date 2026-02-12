using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conectar.MVVM.Model
{
    public class Pelicula
    {
        public int PeliculaID { get; set; }
        public string Titulo { get; set; }
        public int Anio { get; set; }
        public string Director { get; set; }
        public string Genero { get; set; }
        public string Sinopsis { get; set; }
        public string PortadaURL { get; set; }
    }
}
