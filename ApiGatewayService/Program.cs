var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5016","http://0.0.0.0:8080");

#region For reverse proxy
var proxy = builder.Services.AddReverseProxy();
proxy.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
#endregion

var app = builder.Build();
app.MapReverseProxy();
app.MapGet("/", () => "Social App Backend ApiGateway");

app.Run();