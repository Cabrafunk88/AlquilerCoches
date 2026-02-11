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
    /// Lógica de interacción para LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        private void Boton_Bypass(object sender, RoutedEventArgs e)
        {
            var usuarioBypass = new Conectar.MVVM.Model.UsuarioModel
            {
                Username = "Modo Bypass",
                Email = "admin@bypass.com",
                Bio = "Has entrado usando el modo de depuración (Bypass).",
                Id = 0,
                FotoPerfil = "pack://application:,,,/Imagenes/ModoBypass.png", // O la ruta de tu imagen por defecto
                FechaRegistro = DateTime.Now
            };

            Principal principal = new Principal(usuarioBypass);

            this.NavigationService.Navigate(principal);
        }

        private void Boton_Registro(object sender, RoutedEventArgs e)
        {
            RegistroPage RegistroPage = new RegistroPage();

            this.NavigationService.Navigate(RegistroPage);
        }

        private void Boton_Ajustes(object sender, RoutedEventArgs e)
        {
            AjustesPage ajustesPage = new AjustesPage();

            this.NavigationService.Navigate(ajustesPage);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
