using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Ordo.Android.Mvvm.Iteration1.Binding.Converter;

namespace Android.Mvvm.Tests
{
    [ValueConversionAttribute(typeof(string),typeof(string))]
	public class TestValueConverter : IValueConverter 
	{
	    #region Implementation of IValueConverter

	    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	    {
	        throw new NotImplementedException();
	    }

	    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	    {
	        throw new NotImplementedException();
	    }

	    #endregion
	}
}

