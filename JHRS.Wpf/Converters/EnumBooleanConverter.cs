using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace JHRS.Wpf.Converters
{
	public class EnumBooleanConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string paramString = parameter as string;
			if (paramString == null)
			{
				return DependencyProperty.UnsetValue;
			}
			if (!Enum.IsDefined(value.GetType(), value))
			{
				return DependencyProperty.UnsetValue;
			}
			object paramValue = Enum.Parse(value.GetType(), paramString);
			return paramValue.Equals(value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string paramString = parameter as string;
			if (paramString == null)
			{
				return DependencyProperty.UnsetValue;
			}
			return Enum.Parse(targetType, paramString);
		}
	}
}
