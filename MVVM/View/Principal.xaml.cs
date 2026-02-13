using Conectar.MVVM.Data;   // Tu clase AccesoDatos
using Conectar.MVVM.Model;  // Tu clase Pelicula
using Conectar.MVVM.ViewModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data; // Necesario para DataTable y DataRow
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Conectar.MVVM.View
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : Page
    {
        public Principal(Conectar.MVVM.Model.UsuarioModel usuario)
        {
            InitializeComponent();


            // Configuración del ViewModel
            var viewModel = new LoginViewModel();
            viewModel.UsuarioLogeado = usuario;
            viewModel.Username = usuario.Username;

            var peliVM = new PeliculasViewModel();
            this.DataContext = peliVM;

            
        }

        
        private void Pelicula_Click(object sender, RoutedEventArgs e)
        {
            var boton = sender as Button;
            if (boton?.Tag != null)
            {
                int peliculaId = (int)boton.Tag;
                // Aquí podrías navegar a una página de detalle:
                // this.NavigationService.Navigate(new DetallePeliculaPage(peliculaId));
            }
        }

        #region EVENTOS DE NAVEGACIÓN ORIGINALES

        private void BotonUsuario(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.ContextMenu.Width = btn.ActualWidth;
                btn.ContextMenu.PlacementTarget = btn;
                btn.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                btn.ContextMenu.IsOpen = true;
            }
        }

        private void IrAAjustes(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AjustesPage());
        }

        private void IrAPerfil(object sender, RoutedEventArgs e)
        {
            var vm = (Conectar.MVVM.ViewModel.LoginViewModel)this.DataContext;
            if (vm.UsuarioLogeado != null)
            {
                this.NavigationService.Navigate(new PerfilPage(vm.UsuarioLogeado));
            }
        }

        private void IrAReviews(object sender, RoutedEventArgs e)
        {
            var vm = (Conectar.MVVM.ViewModel.LoginViewModel)this.DataContext;
            if (vm.UsuarioLogeado != null)
            {
                this.NavigationService.Navigate(new ReviewsPage(vm.UsuarioLogeado.Id));
            }
        }

        private void CerrarSesion(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LogInPage());
        }

        #endregion
    }
}