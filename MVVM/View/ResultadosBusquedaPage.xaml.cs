using Conectar.MVVM.Model;
using Conectar.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Conectar.MVVM.View
{
    /// <summary>
    /// Lógica de interacción para ResultadosBusquedaPage.xaml
    /// </summary>
    public partial class ResultadosBusquedaPage : Page
    {
        private UsuarioModel _usuario;
        public ResultadosBusquedaPage(string termino, UsuarioModel usuario)
        {
            InitializeComponent();
            _usuario = usuario;

            var vm = new ResultadosBusquedaViewModel();
            this.DataContext = vm;

            // Lanzamos la búsqueda en la base de datos
            _ = vm.BuscarEnDB(termino);
        }

        private void Pelicula_Click(object sender, RoutedEventArgs e)
        {
            var boton = sender as Button;
            if (boton?.Tag != null)
            {
                //Recupera pelicula asociada al botón
                Pelicula peliculaSeleccionada = (Pelicula)boton.Tag;
                // Navegamos pasando la Película seleccionada y el Usuario a la página de detalle
                this.NavigationService.Navigate(new PeliculaDetallePage(peliculaSeleccionada, _usuario));
            }
        }
    private void BotonVolver(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
