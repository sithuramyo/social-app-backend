using AuthenticationService.Features.Logins;
using AuthenticationService.Features.Otps;
using AuthenticationService.Features.Users;
using MailService.MailSetting;
using Microsoft.Extensions.Localization;

namespace AuthenticationService;

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
