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
    /// Lógica de interacción para PerfilPage.xaml
    /// </summary>
    public partial class PerfilPage : Page
    {
        public PerfilPage(Conectar.MVVM.Model.UsuarioModel usuarioLogueado)
        {
            InitializeComponent();
            //Pasamos el viewmodel desde aqui porque piede algo por parametro
            this.DataContext = new ViewModel.PerfilViewModel(usuarioLogueado);
        }
    }
}
