using System.Globalization;
using System.Text.RegularExpressions;

namespace Shared.Extensions;

public static partial class BoolsExtension
{
    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            return false;
        }
        catch (ArgumentException e)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public static bool IsValidBase64(this string str)
    {
        // Check if the string length is a multiple of 4
        if (str.Length % 4 != 0)
        {
            return false;
        }

        // Check if the string matches the Base64 format using a regular expression
        if (!MyRegex().IsMatch(str))
        {
            return false;
        }

        // Try to decode the string to see if it is valid Base64
        try
        {
            Convert.FromBase64String(str);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    [GeneratedRegex(@"^[a-zA-Z0-9\+/]*={0,2}$")]
    private static partial Regex MyRegex();
}