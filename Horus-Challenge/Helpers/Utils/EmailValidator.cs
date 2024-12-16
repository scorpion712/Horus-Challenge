using System.Text.RegularExpressions;

namespace Horus_Challenge.Helpers.Utils;

public static partial class EmailValidator
{

    [GeneratedRegex(@"\b[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}\b")]
    private static partial Regex EmailFormat();

    public static bool IsEmailValid(string email)
        => EmailFormat().IsMatch(email);
}
