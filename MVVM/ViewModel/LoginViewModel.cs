using Conectar.MVVM.Data;
using Conectar.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Conectar.MVVM.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private bool _isBusy;
        private string _mensaje;

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
            IsBusy = true;
            UsuarioLogeado = null;
            Mensaje = "Iniciando sesion";

            try
            {
                AccesoDatos accesoDatos = new AccesoDatos();
                DataTable dt = await accesoDatos.EjecutarProcedimientoAsync(
                    "sp_Login",
                    new List<string> { "p_usuario", "p_password"},
                    new List<object> { Username, Password }
                    );
                if (dt.Rows.Count == 1) 
                { 
                    DataRow fila = dt.Rows[0];
                    UsuarioLogeado = new UsuarioModel
                    {
                        Id = (int)fila["id"],
                        Username = fila["usuario"].ToString(),
                    };
                    Mensaje = $"Sesion iniciada con exito, { UsuarioLogeado.Username}";
                }
                else
                {
                    Mensaje = "Error";
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

        private bool PuedoEjecutarElLogIn(object parameter)
        {
            return !IsBusy;
        }
    }
}
