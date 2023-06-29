using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;

namespace Hotel.View_layer
{
    [ValueConversion(typeof(DateTime), typeof(DateTime))]
    public class DateAddConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime fecha && parameter is int dias)
            {
                DateTime fechaEntrega = fecha.AddDays(dias);
                return fechaEntrega.ToString("dd/MM/yyyy HH:mm:ss");
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
