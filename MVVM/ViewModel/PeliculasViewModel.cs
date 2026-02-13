using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using Conectar.MVVM.Data;
using Conectar.MVVM.Model;

namespace Conectar.MVVM.ViewModel
{

    public class PeliculasViewModel : BaseViewModel
    {

        // La lista que usará el ItemsControl
        public ObservableCollection<Pelicula> Peliculas { get; set; }

        public PeliculasViewModel()
        {
            Peliculas = new ObservableCollection<Pelicula>();
            // Ejecutamos la carga automáticamente al crear el ViewModel
            _ = CargarPeliculasAleatorias();
        }

        private async Task CargarPeliculasAleatorias()
        {
            try
            {
                AccesoDatos db = new AccesoDatos();
                DataTable dt = await db.EjecutarProcedimientoAsync("ObtenerPeliculasAleatorias");

                // Limpiamos y llenamos la colección en el hilo de la UI
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Peliculas.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        string rutaBD = row["PortadaURL"].ToString();
                        Peliculas.Add(new Pelicula
                        {
                            PeliculaID = Convert.ToInt32(row["PeliculaID"]),
                            Titulo = row["Titulo"].ToString(),
                            PortadaURL = $"/Assets{rutaBD}"
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las películas: " + ex.Message);
            }
        }
    }
}