using Conectar.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Conectar.MVVM.View
{
    /// <summary>
    /// Lógica de interacción para AjustesPage.xaml
    /// </summary>
    public partial class AjustesPage : Page
    {
        public AjustesPage()
        {
            InitializeComponent();

            // 1. Sincronizamos el Slider con el volumen actual
            SldVolumen.Value = AudioManager.GetVolume();

            // 2. Comprobamos si ya estaba silenciado para ajustar el botón al entrar
            ActualizarEstadoInterfaz(AudioManager.IsMuted());
        }

        private void SldVolumen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SldVolumen != null)
            {
                AudioManager.SetVolume(e.NewValue);
            }
        }

        // --- ESTE ES EL MÉTODO QUE TE FALTABA ---
        private void BtnMute_Click(object sender, RoutedEventArgs e)
        {
            // Ejecuta la lógica de silencio en el helper
            bool silenciado = AudioManager.ToggleMute();

            // Refleja los cambios en la pantalla
            ActualizarEstadoInterfaz(silenciado);
        }

        // Método para no repetir código y mantener la UI limpia
        private void ActualizarEstadoInterfaz(bool silenciado)
        {
            if (silenciado)
            {
                BtnMute.Content = "Activar Sonido";
                SldVolumen.IsEnabled = false; // Bloqueamos el slider
                SldVolumen.Opacity = 0.5;      // Lo ponemos semitransparente
            }
            else
            {
                BtnMute.Content = "Silenciar Música";
                SldVolumen.IsEnabled = true;  // Desbloqueamos
                SldVolumen.Opacity = 1.0;
                // Sincronizamos el slider por si el volumen cambió al reactivar
                SldVolumen.Value = AudioManager.GetVolume();
            }
        }

        private void BotonVolver(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}