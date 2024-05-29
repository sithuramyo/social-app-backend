using System.Globalization;
using ApiGatewayService;
using ApiGatewayService.Enums;
using ApiGatewayService.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

#region CustomSetting
var stage = builder.Configuration.GetSection("Stage").Value;
var url = "http://0.0.0.0:8080";
if ((int)EnumStageType.Local == Convert.ToInt32(stage))
{
    url = "http://localhost:5016";
}
string settingFileName = "appsettings";
settingFileName = Convert.ToInt32(stage) switch
{
    1 => settingFileName + ".docker.json",
    _ => settingFileName + ".local.json"
};

builder.Configuration.AddJsonFile(settingFileName, optional: true, reloadOnChange: true);

#endregion

builder.WebHost.UseUrls(url);
#region Localization

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
const string defaultCulture = "en-us";

var supportedCultures = new[]
{
    new CultureInfo(defaultCulture),
    new CultureInfo("my-mm")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(defaultCulture);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
#endregion
#region For reverse proxy
var proxy = builder.Services.AddReverseProxy();
proxy.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
#endregion
#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b =>
        {
            b.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
#endregion


var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandler>();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
ResourcesExtension.Configure(app.Services.GetRequiredService<IStringLocalizer<ResponseDescription>>());

#region Middleware Configuration
app.UseCors("AllowAll");
#endregion


app.MapReverseProxy();
app.MapGet("/", () => "Social App Backend ApiGateway");

app.Run();