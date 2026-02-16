using Conectar.MVVM.Data;
using Conectar.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Conectar.MVVM.ViewModel
{
    public class ResultadosBusquedaViewModel : BaseViewModel
    {
        public ObservableCollection<Pelicula> Resultados { get; set; } = new ObservableCollection<Pelicula>();

        public async Task BuscarEnDB(string termino)
        {
            AccesoDatos acceso = new AccesoDatos();
            DataTable dt = await acceso.EjecutarProcedimientoAsync(
                "sp_BuscarPeliculas",
                new List<string> { "p_termino" },
                new List<object> { $"%{termino}%" }
                );

            Application.Current.Dispatcher.Invoke(() =>
            {
                Resultados.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    Resultados.Add(new Pelicula
                    {
                        PeliculaID = (int)row["PeliculaID"],
                        Titulo = row["Titulo"].ToString(),
                        PortadaURL = row["PortadaURL"].ToString(),
                        Sinopsis = row["Sinopsis"].ToString()
                    });
                }
            });
        }
    }
}
