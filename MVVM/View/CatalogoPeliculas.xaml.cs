using System;
using System.Windows;
using System.Windows.Controls;

namespace Conectar.MVVM.View // <--- ESTO ES LO QUE FALTA
{
    public partial class CatalogoPeliculas : Page
    {
        public CatalogoPeliculas()
        {
            InitializeComponent();
        }

        private void Pelicula_Click(object sender, RoutedEventArgs e)
        {
            var boton = sender as Button;
            if (boton?.Tag != null)
            {
                try
                {
                    int id = Convert.ToInt32(boton.Tag);
                    // Aquí irá la navegación al detalle
                    MessageBox.Show("Has seleccionado la película con ID: " + id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al convertir ID: " + ex.Message);
                }
            }
        }
    }
}