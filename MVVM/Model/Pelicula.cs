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
        public string PortadaURL { get; set; } // Lo que viene de la BD: "/Portadas/Peli.jpg"

        // Esta propiedad es la que usaremos en el Binding del XAML
        public string ImagenFuente
        {
            get
            {
                // Si PortadaURL es "/Portadas/TheGodfather.jpg"
                // La ruta final será "pack://application:,,,/Assets/Portadas/TheGodfather.jpg"

                // Limpiamos la ruta por si acaso trae barras extra
                string rutaLimpia = PortadaURL.TrimStart('/');
                return $"pack://application:,,,/Assets/{rutaLimpia}";
            }
        }
    }
}
