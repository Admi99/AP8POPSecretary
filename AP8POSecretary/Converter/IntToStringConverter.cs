using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace AP8POSecretary.Converter
{
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string resValue = string.Empty;
            try
            {
                resValue = value.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return resValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int res = 0;
            if (int.TryParse(value as String, out res))
            {
                return res;
            }
            else return res;
        }
    }
}
