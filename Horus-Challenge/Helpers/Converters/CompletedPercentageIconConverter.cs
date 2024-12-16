
using System.Globalization;

namespace Horus_Challenge.Helpers.Converters;

public class CompletedPercentageIconConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => (double)value >= 100
        ? "arrow_right_w"
        : "arrow_right_g";

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
