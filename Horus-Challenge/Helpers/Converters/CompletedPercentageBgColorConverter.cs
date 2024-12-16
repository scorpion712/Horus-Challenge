using System.Globalization;

namespace Horus_Challenge.Helpers.Converters;

public class CompletedPercentageBgColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => (double)value >= 100 
        ? Application.Current.Resources.MergedDictionaries.First()["Primary"] 
        : Application.Current.Resources.MergedDictionaries.First()["White"];

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
