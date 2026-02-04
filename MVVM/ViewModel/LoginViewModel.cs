using Conectar.MVVM.Data;
using Conectar.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Conectar.MVVM.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private bool _isBusy;
        private string _mensaje;
        private int _intentosFallidos = 0;
        private const int MaxIntentos = 3;
        private bool _bloqueado = false;

        private UsuarioModel _usuarioLogeado;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                System.Windows.Input.CommandManager.InvalidateRequerySuggested();
            }
        }

        public string Mensaje
        {
            get => _mensaje;
            set
            {
                _mensaje = value;
                OnPropertyChanged();
            }
        }

        public int IntentosFallidos
        {
            get { return _intentosFallidos; }
            set
            {
                _intentosFallidos = value;
                OnPropertyChanged();
            }
        }

        public bool Bloqueado
        {
            get => _bloqueado;
            set
            {
                _bloqueado = value;
                OnPropertyChanged();
                //El comando no puede ejecutarse si está bloqueado
                //((RelayCommand)LogInCommand).RaiseCanExecuteChanged();
                System.Windows.Input.CommandManager.InvalidateRequerySuggested();
            }
        }
        public UsuarioModel UsuarioLogeado
        {
            get { return _usuarioLogeado;  }
            set
            {
                _usuarioLogeado = value;
                OnPropertyChanged();
            }
        }

        public ICommand LogInCommand { get; }

        public LoginViewModel()
        {
            LogInCommand = new RelayCommand(EjecutarLogIn, PuedoEjecutarElLogIn);
            UsuarioLogeado = new UsuarioModel();
        }

        private void EjecutarLogIn(object parameter)
        {
            LogInAsync();
        }

        private async void LogInAsync()
        {
            if (IsBusy || Bloqueado) return; //si el boton esta bloqueado, no hacer nada

            IsBusy = true;
            UsuarioLogeado = null;
            Mensaje = "Iniciando sesion";

            try
            {
                AccesoDatos accesoDatos = new AccesoDatos();
                DataTable dt = await accesoDatos.EjecutarProcedimientoAsync(
                    "sp_Login",
                    new List<string> { "p_username", "p_password"},
                    new List<object> { Username, Password }
                    );
                if (dt.Rows.Count == 1) 
                {
                    _intentosFallidos = 0; //Reinicio
                    DataRow fila = dt.Rows[0];
                    UsuarioLogeado = new UsuarioModel
                    {
                        Id = (int)fila["UsuarioID"],
                        Username = fila["NombreUsuario"].ToString(),
                    };
                    Mensaje = $"Sesion iniciada con exito, { UsuarioLogeado.Username}";

                    // --- LÓGICA DE NAVEGACIÓN ---
                    var mainWindow = Application.Current.MainWindow as MainWindow;
                    if (mainWindow != null)
                    {
                        mainWindow.MainFrame.Navigate(new Conectar.MVVM.View.Principal());
                    }
                }
                else
                {
                    _intentosFallidos++;
                    int intentosRestantes = MaxIntentos - _intentosFallidos;

                    if(_intentosFallidos >= MaxIntentos)
                    {
                        _ = TemporizadorBloqueo(15); //bloqueo 15 segundos
                    }
                    else
                    {
                        Mensaje = $"Datos incorrectos. Te quedan {intentosRestantes} intentos.";
                    }

                }
            }catch (Exception ex)
            {
                Mensaje = $"Error al iniciar sesion: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task TemporizadorBloqueo(int segundos) //Temprizador para cuando se bloquea
        {
           Bloqueado = true;
            for (int i = segundos; i > 0; i--)
            {
                Mensaje = $"Demasiados intentos. Acceso bloqueado por {i} segundos.";
                await Task.Delay(1000); // Espera 1 segundo
            } 
            _intentosFallidos = 0;
            Bloqueado = false;
            Mensaje = "Ya puedes intentar iniciar sesion de nuevo.";
        }

        private bool PuedoEjecutarElLogIn(object parameter)
        {
            return !Bloqueado;
        }
    }
}
