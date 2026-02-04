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
            Principal principal = new Principal();

            this.NavigationService.Navigate(principal);
        }

        private void Boton_Registro(object sender, RoutedEventArgs e)
        {
            RegistroPage RegistroPage = new RegistroPage();

            this.NavigationService.Navigate(RegistroPage);
        }
    }
}
