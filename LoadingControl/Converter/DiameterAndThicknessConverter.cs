using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace LoadingControl.Converter
{
    public class DiameterAndThicknessConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values.Length<2 || !double.TryParse(values[0].ToString(),out double diameter) || !double.TryParse(values[1].ToString(),out double thickness))
            {
                return 0;
            }
            double circumference = Math.PI * diameter;
            double linelength = circumference * 0.75;
            double gaplength = circumference - linelength;
            return new DoubleCollection(new[] {gaplength/thickness,linelength/thickness});
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
