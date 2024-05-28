using System.Globalization;
using System.Text;
using AuthenticationService;
using DatabaseService.AppContextModels;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Shared.Enums;
using Swashbuckle.AspNetCore.Filters;
using ResourcesExtension = AuthenticationService.ResourcesExtension;
using ResponseDescription = AuthenticationService.ResponseDescription;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Custom app setting

var stage = builder.Configuration.GetSection("Stage").Value;

#endregion

#region MyRegion

var url = "http://0.0.0.0:8081";
if ((int)EnumStageType.Local == Convert.ToInt32(stage))
{
    url = "http://localhost:5244";
}

#endregion

builder.WebHost.UseUrls(url);

#region Authentication

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"Bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

#endregion

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

#region Extension Service Configure

Startup.Configure(builder.Configuration);
ServicesInjection.Configure(builder.Configuration);
builder.Services.AddServices();

#endregion

#region Db Connection

// Use in docker 
var server = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

if ((int)EnumStageType.Local == Convert.ToInt32(stage))
{
    server = "localhost";
    dbName = "socialdb";
    dbUser = "root";
    dbPassword = "rootroot";
}

var connectionString =
    $"Server={server};port=3306;Database={dbName};User Id={dbUser};Password={dbPassword};CharSet=utf8;";

#endregion

#region Mysql Connection

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)
    )
);

#endregion

#region Health Check

builder.Services.AddHealthChecks()
    .AddMySql(connectionString);

#endregion

#region Serilog

string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/EvPortalService.log");
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    // Add this line:
    .WriteTo.File(filePath,
        rollingInterval: RollingInterval.Hour,
        fileSizeLimitBytes: 10 * 1024 * 1024,
        retainedFileCountLimit: 2,
        rollOnFileSizeLimit: true,
        shared: true,
        flushToDiskInterval: TimeSpan.FromSeconds(1))
    .CreateLogger();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
ResourcesExtension.Configure(app.Services.GetRequiredService<IStringLocalizer<ResponseDescription>>());

app.MapHealthChecks("/authapi/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", () => "Social App Backend AuthenticationApi");
app.MapControllers();
app.Run();