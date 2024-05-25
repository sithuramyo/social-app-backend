namespace Shared.Extensions;

public static class EnumsExtension
{
    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
}