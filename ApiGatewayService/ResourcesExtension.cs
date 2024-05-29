using Microsoft.Extensions.Localization;

namespace ApiGatewayService;

public static class ResourcesExtension
{
    private static IStringLocalizer<ResponseDescription> _localizer { get; set; }
    public static void Configure(IStringLocalizer<ResponseDescription> localizer)
    {
        _localizer = localizer;
    }
    public static string GetResource(this string str)
    {
        return _localizer?[str];
    }
}