using Conectar.MVVM.Data;
using Conectar.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Conectar.MVVM.ViewModel
{
    public class PeliculaDetalleViewModel : BaseViewModel
    {
        private int _peliculaID; 
        private string _titulo; 
        private string _sinopsis; 
        private string _portadaURL;
        private DataTable _reviewsGenerales;
        private string _nuevaResena;
        private int _puntuacionSeleccionada;

        public int PeliculaID 
        {
            get { return _peliculaID; }
            set
            {
                _peliculaID = value;
                OnPropertyChanged();
            }
        }
        public string Titulo
        {
            get { return _titulo; } 
            set 
            { 
                _titulo = value; 
                OnPropertyChanged(); 
            }
        }
        public string Sinopsis 
        {
            get { return _sinopsis; } set { _sinopsis = value; OnPropertyChanged(); }
        }
        public string PortadaURL
        {
            get { return _portadaURL; } set { _portadaURL = value; OnPropertyChanged(); }
        }
        public DataTable ReviewsGenerales 
        { 
            get { return _reviewsGenerales; } 
            set { _reviewsGenerales = value; OnPropertyChanged(); } 
        }
        public string NuevaResena 
        { 
            get { return _nuevaResena; }
            set { _nuevaResena = value; OnPropertyChanged(); } 
        }
        public int PuntuacionSeleccionada
        {
            get { return _puntuacionSeleccionada; }
            set
            {
                _puntuacionSeleccionada = value;
                OnPropertyChanged();
            }
        }
        public string ImagenFuente => $"pack://application:,,,/Assets/{PortadaURL.TrimStart('/')}";
        public int IdUsuarioLogueado { get; set; }

        public ICommand EnviarResenaCommand { get; }

        public PeliculaDetalleViewModel(Pelicula pelicula, UsuarioModel usuario) 
        {
            PeliculaID = pelicula.PeliculaID;
            Titulo = pelicula.Titulo; 
            Sinopsis = pelicula.Sinopsis; 
            PortadaURL = pelicula.PortadaURL;

            IdUsuarioLogueado = usuario.Id;

            //Cargamos las reviews de la pelicula solo
            _ = CargarReviews();

            EnviarResenaCommand = new RelayCommand(async o => await EjecutarEnvioResena());

        }

        private async Task CargarReviews()
        {
            AccesoDatos acceso = new AccesoDatos();
            ReviewsGenerales = await acceso.EjecutarProcedimientoAsync(
                "sp_ObtenerResenasPelicula",
                new List<string> { "p_pelicula_id" },
                new List<object> { PeliculaID }
                );
        }

        private async Task EjecutarEnvioResena()
        {
            if (IdUsuarioLogueado <= 0)
            {
                MessageBox.Show("Modo Bypass: Debes iniciar sesión con una cuenta real para poder publicar reseñas.");
                return;
            }

            //Validamos que el usuario haya escrito algo antes de enviar y que haya seleccionado una estrella o mas
            if (PuntuacionSeleccionada == 0 || string.IsNullOrWhiteSpace(NuevaResena))
            {
                MessageBox.Show("Por favor, selecciona una puntuacion y escribe un comentario antes de enviar.");
                return;
            }

            try
            {
                AccesoDatos acceso = new AccesoDatos();

                List<string> nombres = new List<string> { "p_usuario", "p_pelicula", "p_puntuacion", "p_contenido" };
                List<object> valores = new List<object> { IdUsuarioLogueado, PeliculaID, PuntuacionSeleccionada, NuevaResena };

                int resultado = await acceso.EjecutarProcedimientoNonQueryAsync("sp_InsertarResena", nombres, valores);

                if (resultado > 0)
                {
                    MessageBox.Show("Reseña enviada con exito");
                    NuevaResena = string.Empty;
                    await CargarReviews();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar la reseña: " + ex.Message);
            }
        }

    }
}
