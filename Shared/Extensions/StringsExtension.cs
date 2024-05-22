using Newtonsoft.Json;

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
    
    public static string GetImageName()
    {
        var imageName = $"{Guid.NewGuid()}.jpg";
        return imageName;
    }
}