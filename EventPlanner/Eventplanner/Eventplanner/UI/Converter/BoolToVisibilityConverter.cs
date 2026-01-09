using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace Eventplanner.UI.Converter
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public bool Collapsed { get; set; } = true;
        public bool Inverse { get; set; }

        //if inverse is set to true, return collapsed or hidden. If inverse is set to false, return visible, default is collapsed
        private Visibility TrueState { get { return !this.Inverse ? Visibility.Visible : (this.Collapsed ? Visibility.Collapsed : Visibility.Hidden); } }

        //if inverse is set to true, return visible. If inverse is set to false, return collapsed or hidden, default is collapsed
        private Visibility FalseState { get { return this.Inverse ? Visibility.Visible : (this.Collapsed ? Visibility.Collapsed : Visibility.Hidden); } }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return this.FalseState;

            var result = false;
            if (bool.TryParse(value.ToString(), out result))
            {
                return result ? this.TrueState : this.FalseState;
            }

            return this.FalseState;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
