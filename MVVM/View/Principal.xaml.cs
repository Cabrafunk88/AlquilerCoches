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
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : Page
    {
        public Principal(string nombreUsuario)
        {
            InitializeComponent();

            // Obtenemos el ViewModel de esta página y le asignamos el nombre recibido
            var viewModel = (Conectar.MVVM.ViewModel.LoginViewModel)this.DataContext;
            viewModel.Username = nombreUsuario;
        }
        public Principal() : this("Invitado") { }

        private void BotonUsuario(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.ContextMenu.Width = btn.ActualWidth; // Hacemos que el menú tenga el mismo ancho que el botón

                btn.ContextMenu.PlacementTarget = btn;

                // Colocamos el menú justo debajo del botón
                btn.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;

                btn.ContextMenu.IsOpen = true;
            }
        }

        private void IrAAjustes(object sender, RoutedEventArgs e)
        {
            // Navegamos a la página de ajustes
            this.NavigationService.Navigate(new AjustesPage());
        }

        private void IrAPerfil(object sender, RoutedEventArgs e)
        {
            // Navegamos a la página de perfil
            //Coges el nombre de usuario del ViewModel para pasarlo a la página de perfil
            this.NavigationService.Navigate(new PerfilPage(new Conectar.MVVM.Model.UsuarioModel { Username = ((Conectar.MVVM.ViewModel.LoginViewModel)this.DataContext).Username }));
        }

        private void IrAReviews(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ReviewsPage());
        }
        private void CerrarSesion(object sender, RoutedEventArgs e)
        {
            // Navegamos de vuelta a la página de login
            this.NavigationService.Navigate(new LogInPage());
        }
    }
}






