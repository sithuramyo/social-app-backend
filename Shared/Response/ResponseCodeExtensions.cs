using Shared.Enums;

namespace Shared.Response;

public static class ResponseCodeExtensions
{
    public static EnumResponseType GetRespType(this string code)
    {
        char sign = code.ElementAt(0);
        var respType = sign switch
        {
            'S' => EnumResponseType.Success,
            'I' => EnumResponseType.Information,
            'W' => EnumResponseType.Warning,
            'E' => EnumResponseType.Error,
            _ => throw new Exception("There is no response type.")
        };
        return respType;
    }
}