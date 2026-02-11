using Conectar.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ICommand EditarPerfilCommand { get; }

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

            EditarPerfilCommand = new RelayCommand(EditarPerfil);
        }

        private void EditarPerfil(object parameter)
        {

        }
    }
}
