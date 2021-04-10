using AP8POSecretary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace AP8POSecretary.Converter
{
    public class ComboboxItemToLanguageEnum : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ComboBoxItem item = value as ComboBoxItem;
            switch (item.Content)
            {
                case "Czech":
                    return SubjectLanguage.CZECH;
                case "English":
                    return SubjectLanguage.ENGLISH;
                default:
                    return null;
            }
        }
    }
}
