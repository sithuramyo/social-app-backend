using System.Globalization;
using System.Text;
using AuthenticationService;
using DatabaseService.AppContextModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Shared.Extensions;
using Shared.Response;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:8081");
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
const string defaultCulture = "en-US";

var supportedCultures = new[]
{
    new CultureInfo(defaultCulture),
    new CultureInfo("my-MM")
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

var server = "localhost";
var dbName = "socialdb";
var dbUser = "root";
var dbPassword = "rootroot";
// Use in docker 
// var server = Environment.GetEnvironmentVariable("DB_HOST") ;
// var dbName = Environment.GetEnvironmentVariable("DB_NAME");
// var dbUser = Environment.GetEnvironmentVariable("DB_USER");
// var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

var connectionString =
    $"Server={server};port=3306;Database={dbName};User Id={dbUser};Password={dbPassword};CharSet=utf8;";

#endregion

#region Mysql Connection

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql("server=social-backend-db;port=3307;database=socialdb;User=socialuser;password=socialpassword;", ServerVersion.AutoDetect("server=socialbackenddb;port=3307;database=socialdb;User=socialuser;password=socialpassword;")
    )
);

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
ResourcesExtension.Configure(app.Services.GetRequiredService<IStringLocalizer<ResponseDescription>>());


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", () => "Social App Backend AuthenticationApi");
app.MapControllers();
app.Run();