using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Eventplanner.Converter
{
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null)
            {
                int paramAsInt;
                bool success = int.TryParse((string)parameter, out paramAsInt);

                if (success)
                {
                    return (int)value == paramAsInt;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {

                if ((bool)value)
                {
                    int paramAsInt;
                    bool success = int.TryParse((string)parameter, out paramAsInt);

                    if (success)
                    {
                        return paramAsInt;
                    }
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }
    }
}
