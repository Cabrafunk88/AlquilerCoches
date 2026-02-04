using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Conectar.MVVM.ViewModel
{
    public class RegistroViewModel : BaseViewModel
    {
        private string _nombreUsuario;
        private string _correoElectronico;
        private string _contrasena;
        private string _descripcion;
        private string _mensaje;

        public string NombreUsuario
        {
            get { return _nombreUsuario; }
            set
            {
                _nombreUsuario = value;
                OnPropertyChanged();
            }
        }
        public string CorreoElectronico
        {
            get { return _correoElectronico; }
            set
            {
                _correoElectronico = value;
                OnPropertyChanged();
            }
        }
        public string Contrasena
        {
            get { return _contrasena; }
            set
            {
                _contrasena = value;
                OnPropertyChanged();
            }
        }
        public string Descripcion
        {
            get { return _descripcion; }
            set
            {
                _descripcion = value;
                OnPropertyChanged();
            }
        }
        public string Mensaje
        {
            get { return _mensaje; }
            set
            {
                _mensaje = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegistrarCommand { get; }

        public RegistroViewModel()
        {
            RegistrarCommand = new RelayCommand(async _ => await RegistrarUsuarioAsync());
        }

        private async Task RegistrarUsuarioAsync()
        {
            if (string.IsNullOrEmpty(NombreUsuario) || string.IsNullOrEmpty(CorreoElectronico) || string.IsNullOrEmpty(Contrasena) || string.IsNullOrEmpty(Descripcion))
            {
                Mensaje = "Por favor, complete todos los campos.";
                return;
            }
            try
            {
                var acceso = new Data.AccesoDatos();

                //Insertar nuevo usuario
                int filas = await acceso.EjecutarProcedimientoNonQueryAsync(
                    "sp_RegistrarUsuario",
                    new List<string> { "p_nombreUsuario", "p_correoElectronico", "p_contraseña", "p_descripcion" },
                    new List<object> { NombreUsuario, CorreoElectronico, Contrasena, Descripcion }
                    );

                if (filas > 0)
                {
                    Mensaje = "Registro exitoso.";
                    await Task.Delay(2000);
                    // Volver a la página de inicio de sesión
                    var mainWindow = Application.Current.MainWindow as MainWindow;
                    if (mainWindow != null)
                    {
                        mainWindow.MainFrame.Navigate(new Conectar.MVVM.View.LogInPage());
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al registrar usuario: {ex.Message}";
            }
        }
    }
}
