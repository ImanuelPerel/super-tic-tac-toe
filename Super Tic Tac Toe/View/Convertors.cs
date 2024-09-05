using System;
using System.Globalization;
using System.Windows.Data;

namespace SuperTicTacToe.View.Converters;

public class WhosTurnIsThis : IValueConverter
{
    // Convert method for bool to string
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return $"It is {MetaData.SymbolConvertor(value)}'s turn:";
    }

    // ConvertBack method for string to bool (optional, if needed for two-way binding)
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
     throw new NotImplementedException();
    }
}