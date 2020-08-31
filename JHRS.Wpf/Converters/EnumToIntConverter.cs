using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace JHRS.Wpf.Converters
{
    /// <summary>
    /// 枚舉轉數字
    /// </summary>
    public class EnumToIntConverter : IValueConverter
    {
        /// <summary>
        /// 枚舉轉數字
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Enum.IsDefined(value.GetType(), value))
            {
                return (int)value;
            }
            return -1;
        }

        /// <summary>
        /// 反向轉換，將數字轉爲枚舉
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Enum.TryParse(value.GetType(), value?.ToString(), out object result);
            return result;
        }
    }
}
