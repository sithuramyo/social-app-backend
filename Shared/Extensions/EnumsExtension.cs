using Shared.Enums;

namespace Shared.Extensions;

public static class EnumsExtension
{
    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static int GetPostAccessType(this int type)
    {
        var value = type switch
        {
            1 => (int)EnumPostAccessType.Public,
            2 => (int)EnumPostAccessType.FriendOnly,
            3 => (int)EnumPostAccessType.Restricted,
            4 => (int)EnumPostAccessType.Private,
            _ => 0
        };
        return value;
    }
}