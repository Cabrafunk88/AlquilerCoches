using Conectar.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Conectar.MVVM.ViewModel
{
    public class PerfilViewModel : BaseViewModel
    {
        private int _idUsuario;
        private string _username;
        private string _email;
        private string _password;
        private bool _esAdministrador;
        private string _bio;
        private string _fotoPerfil;
        private DateTime _fechaRegistro;
        private bool _estaEditando;
        //Para guardar los valores originales en caso de cancelar la edición
        private string _bioBackup;
        private string _emailBackup;

        public int IdUsuario
        {
            get { return _idUsuario; }
            set
            {
                _idUsuario = value;
                OnPropertyChanged();
            }
        }
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
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
        public bool EsAdministrador
        {
            get { return _esAdministrador; }
            set
            {
                _esAdministrador = value;
                OnPropertyChanged();
            }
        }
        public string Bio
        {
            get { return _bio; }
            set
            {
                _bio = value;
                OnPropertyChanged();
            }
        }
        public string FotoPerfil
        {
            get { return _fotoPerfil; }
            set
            {
                _fotoPerfil = value;
                OnPropertyChanged();
            }
        }
        public DateTime FechaRegistro
        {
            get { return _fechaRegistro; }
            set
            {
                _fechaRegistro = value;
                OnPropertyChanged();
            }
        }
        public bool EstaEditando
        {
            get { return _estaEditando; }
            set
            {
                _estaEditando = value;
                OnPropertyChanged();
            }
        }


        public ICommand EditarPerfilCommand { get; }
        public ICommand AceptarCambiosCommand { get; }
        public ICommand CancelarEdicionCommand { get; }

        public PerfilViewModel(UsuarioModel usuario)
        {
            if (usuario != null)
            {
                IdUsuario = usuario.Id;
                Username = usuario.Username;
                Email = usuario.Email;
                Password = usuario.Password;
                EsAdministrador = usuario.EsAdministrador;
                Bio = usuario.Bio;
                FotoPerfil = usuario.FotoPerfil;
                FechaRegistro = usuario.FechaRegistro;
            }

            //Pone estaeditando en true
            EditarPerfilCommand = new RelayCommand(o =>
            {
                _bioBackup = Bio;
                _emailBackup = Email;
                EstaEditando = true;
            });
            //Al cancelar vuelve a solo leer
            CancelarEdicionCommand = new RelayCommand(o => {
                Bio = _bioBackup;
                Email = _emailBackup;
                EstaEditando = false;
                });
            //Al aceptar guardamos cambios en la base de datos
            AceptarCambiosCommand = new RelayCommand(async o => await GuardarCambiosEnBaseDeDatos());
        }

        private async Task GuardarCambiosEnBaseDeDatos()
        {
            // 1. Evitamos que se intente guardar si es modo Bypass (ID = 0)
            if (IdUsuario <= 0)
            {
                MessageBox.Show("Modo Bypass: Los cambios no se guardarán en la base de datos.");
                EstaEditando = false;
                return;
            }

            try
            {
                Conectar.MVVM.Data.AccesoDatos acceso = new Conectar.MVVM.Data.AccesoDatos();

                // 2. Preparamos los parámetros para el procedimiento o consulta
                // Suponiendo que tienes un procedimiento llamado sp_ActualizarUsuario
                List<string> nombresParametros = new List<string> { "p_id", "p_bio", "p_email" };
                List<object> valoresParametros = new List<object> { IdUsuario, Bio, Email };

                // 3. Ejecutamos (Usamos EjecutarProcedimientoNoQueryAsync si solo actualiza)
                int filasAfectadas = await acceso.EjecutarProcedimientoNonQueryAsync("sp_ActualizarUsuario", nombresParametros, valoresParametros);

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("¡Perfil actualizado correctamente!");
                    EstaEditando = false; // Volvemos al modo lectura
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el perfil. Inténtalo de nuevo.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error crítico al guardar: {ex.Message}");
            }
        }
    }
}
