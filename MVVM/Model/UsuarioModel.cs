using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conectar.MVVM.Model
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        private string _username;
        private string _email;
        private string _password;
        private bool _EsAdministrador;
        private string _bio;
        private string _FotoPerfil;
        private DateTime _FechaRegistro;


        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public bool EsAdministrador
        {
                        get { return _EsAdministrador; } 
            set { _EsAdministrador = value; }
        }
        public string Bio
        {
            get { return _bio; }
            set { _bio = value; }
        }
        public string FotoPerfil
        {
            get { return _FotoPerfil; }
            set { _FotoPerfil = value; }
        }
        public DateTime FechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
    }
}
