using AP8POSecretary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace AP8POSecretary.Converter
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string EnumString;
            try
            {
                EnumString = Enum.GetName((value.GetType()), value);
                return EnumString;
            }
            catch
            {
                return string.Empty;
            }
        }

        // No need to implement converting back on a one-way binding 
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value)
            {
                case "EXAM":
                    return CompletionType.EXAM;
                case "CLASSIFIED":
                    return CompletionType.CLASSIFIED;
                case "DAILY":
                    return StudyType.DAILY;
                case "DISTANCE":
                    return StudyType.DISTANCE;
                case "SPRING":
                    return SemesterType.SPRING;
                case "WINTER":
                    return SemesterType.WINTER;
                default:
                    return new NotImplementedException();
            }
        }
    }
}
