using Eventplanner.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Eventplanner.Converter
{
    public class GenderToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Gender gender && Enum.IsDefined(typeof(Gender), gender))
            {
                switch (gender)
                {
                    case Gender.MALE:
                        return true;
                    case Gender.FEMALE:
                        return false;
                    default:
                        return true;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool booleanValue)) return null;

            if (booleanValue)
            {
                return Gender.MALE;
            }
            else
            {
                return Gender.FEMALE;
            }
        }
    }
}
