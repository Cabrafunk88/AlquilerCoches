using Conectar.MVVM.Model;
using Conectar.MVVM.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Conectar.MVVM.View 
{
    public partial class CatalogoPeliculas : Page
    {
        public CatalogoPeliculas(Conectar.MVVM.Model.UsuarioModel usuario)
        {
            InitializeComponent();

            // Configuración del ViewModel
            var viewModel = new LoginViewModel();
            viewModel.UsuarioLogeado = usuario;
            viewModel.Username = usuario.Username;

            var peliVM = new LoginViewModel();
            this.DataContext = viewModel;

            _ = viewModel.CargarTodasAsync(); // Carga las películas al iniciar la página
        }

        private void Pelicula_Click(object sender, RoutedEventArgs e)
        {
            var boton = sender as Button;
            if (boton?.Tag != null)
            {
                //Recupera pelicula asociada al botón
                Pelicula peliculaSeleccionada = (Pelicula)boton.Tag;

                // 2. Recuperamos el usuario logueado
                var vm = (Conectar.MVVM.ViewModel.LoginViewModel)this.DataContext;

                if (vm?.UsuarioLogeado != null)
                {
                    // 3. Navegamos pasando la Película seleccionada y el Usuario a la página de detalle
                    this.NavigationService.Navigate(new PeliculaDetallePage(peliculaSeleccionada, vm.UsuarioLogeado));
                }
                else
                {
                    MessageBox.Show("Error: No se ha detectado ningún usuario logueado.");
                }
            }
        }

        private void BotonVolver(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}