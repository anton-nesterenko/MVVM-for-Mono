using System;
using System.Globalization;

namespace Ordo.Android.Mvvm.Iteration1.Binding.Converter
{
    public interface IValueConverter
    {
        object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
