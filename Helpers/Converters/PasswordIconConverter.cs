using System.Globalization;

namespace Horus_Challenge.Helpers.Converters;

public class PasswordIconConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var showPassword = (bool)value;

        return showPassword ? "eye_b" : "eye_none_b";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
