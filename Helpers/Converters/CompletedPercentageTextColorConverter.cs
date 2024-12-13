using System.Globalization;

namespace Horus_Challenge.Helpers.Converters;

public class CompletedPercentageTextColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => (double)value >= 100
        ? Application.Current.Resources.MergedDictionaries.First()["White"]
        : Application.Current.Resources.MergedDictionaries.First()["PrimaryText"];

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
