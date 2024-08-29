using System;
using System.Globalization;
using System.Windows.Data;

namespace SuperTicTacToe.View.Converters
{
    public class BoolToCharConverter : IValueConverter
    {
        // Convert method for bool to string
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                return MetaData.symbols[booleanValue];
            }
            if (value is null)
                return MetaData.DefualtChar;
            throw new ArgumentException($"{nameof(BoolToCharConverter)} can't handle not boolean values ");
        }

        // ConvertBack method for string to bool (optional, if needed for two-way binding)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
         throw new NotImplementedException();
        }
    }
}