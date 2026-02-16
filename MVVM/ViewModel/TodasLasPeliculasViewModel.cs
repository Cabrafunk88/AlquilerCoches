using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using Conectar.MVVM.Data;
using Conectar.MVVM.Model;

namespace Conectar.MVVM.ViewModel
{
    public class TodasLasPeliculasViewModel : BaseViewModel
    {
        public ObservableCollection<Pelicula> ListaCompleta { get; set; }

        public TodasLasPeliculasViewModel()
        {
            ListaCompleta = new ObservableCollection<Pelicula>();
            _ = CargarTodasAsync();
        }

        private async Task CargarTodasAsync()
        {
            try
            {
                AccesoDatos db = new AccesoDatos();
                // Asegúrate de tener este procedimiento que devuelva TODO
                DataTable dt = await db.EjecutarProcedimientoAsync("ObtenerTodasLasPeliculas");

                Application.Current.Dispatcher.Invoke(() =>
                {
                    ListaCompleta.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        ListaCompleta.Add(new Pelicula
                        {
                            PeliculaID = Convert.ToInt32(row["PeliculaID"]),
                            Titulo = row["Titulo"].ToString(),
                            PortadaURL = $"/Assets{row["PortadaURL"]}"
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar catálogo: " + ex.Message);
            }
        }
    }
}