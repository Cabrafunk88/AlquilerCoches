using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Conectar.Helpers
{
    public class IntToBooleanConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return false;

            int puntuacionActual = (int)value;
            int estrellaId = int.Parse(parameter.ToString());

            return estrellaId <= puntuacionActual;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Si el usuario pincha la estrella, devuelve ese número al ViewModel
            return value != null && value.Equals(true) ? int.Parse(parameter.ToString()) : Binding.DoNothing;
        }
    }
}
