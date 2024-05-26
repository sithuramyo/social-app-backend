var builder = WebApplication.CreateBuilder(args);


#region For reverse proxy
var proxy = builder.Services.AddReverseProxy();
proxy.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
#endregion

var app = builder.Build();
app.MapReverseProxy();
app.MapGet("/", () => "Social App Backend ApiGateway");

app.Run();