using Newtonsoft.Json;
using Shared.Models.Otps;

namespace Shared.Extensions;

public static class StringsExtension
{
    public static T? ToObject<T>(this string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public static bool IsNullOrEmpty(this string? str)
    {
        return str == null || string.IsNullOrEmpty(str);
    }

    public static bool IsNotNullOrEmpty(this string? str)
    {
        return str != null && !string.IsNullOrEmpty(str);
    }

    public static string? TrimAndLower(this string? str)
    {
        return str?.Trim().ToLower();
    }

    public static string? TrimAndUpper(this string? str)
    {
        return str?.Trim().ToUpper();
    }

    public static bool Includes(this string? str, string? searchValue)
    {
        return str != null
               && searchValue != null
               && str.Trim().ToLower().Contains(searchValue.Trim().ToLower());
    }

    public static bool EqualIgnoreCase(this string? str1, string? str2)
    {
        return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
    }
    
    public static GenerateOtpModel GenerateOtp()
    {
        var random = new Random();
        const string chars = "0123456789";
        var otp = new char[4];
        for (var i = 0; i < 4; i++)
        {
            otp[i] = chars[random.Next(chars.Length)];
        }

        return new GenerateOtpModel
        {
            OtpCode = new string(otp),
            OptExpires = 5
        };
    }
    
    public static string GetImageName()
    {
        var imageName = $"{Guid.NewGuid()}.jpg";
        return imageName;
    }
}