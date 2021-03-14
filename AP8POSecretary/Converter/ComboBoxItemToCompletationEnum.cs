using AP8POSecretary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace AP8POSecretary.Converter
{
    class ComboBoxItemToCompletationEnum : IValueConverter
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
                case "Exam":
                    return CompletionType.EXAM;
                case "Classified":
                    return CompletionType.CLASSIFIED;
                default:
                    return null;
            }
        }

        private string getCompletationType(string str, char ch) => str.Split(' ').Last();

    }
}
