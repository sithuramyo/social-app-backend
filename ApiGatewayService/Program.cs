using ApiGatewayService.Enums;
using ApiGatewayService.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Yarp.ReverseProxy.Forwarder;

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

builder.Services.AddSingleton<IForwarderHttpClientFactory, CustomForwarderHttpClientFactory>();  

#region For reverse proxy
var proxy = builder.Services.AddReverseProxy();
proxy.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
#endregion

#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
#endregion

var app = builder.Build();
#region Middleware Configuration
app.UseCors("AllowAll");
#endregion

app.MapReverseProxy();

app.MapGet("/", () => "Social App Backend ApiGateway");

app.Run();