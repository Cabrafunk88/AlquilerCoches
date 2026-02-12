using Conectar.MVVM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conectar.MVVM.ViewModel
{
    public class ReviewsViewModel : BaseViewModel
    {
        private DataTable _misResenas;
        public DataTable MisResenas
        {
            get => _misResenas;
            set
            {
                _misResenas = value; OnPropertyChanged();
            }
        }

        public ReviewsViewModel(int idUsuario)
        {
            CargarResenas(idUsuario);
        }

        private async void CargarResenas(int idUsuario) 
        {
            AccesoDatos acceso = new AccesoDatos();

            //Id usuario para cargar solo sus reseñas
            MisResenas = await acceso.EjecutarProcedimientoAsync(
            "sp_ObtenerResenasUsuario",
            new List<string> { "p_usuario_id" },
            new List<object> { idUsuario }
        );
        }
    }
}
