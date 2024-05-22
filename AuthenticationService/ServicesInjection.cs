using AuthenticationService.Features.Users;

namespace AuthenticationService;

public static class ServicesInjection
{
    private static IConfiguration _configuration { get; set; }

    public static void Configure(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersServices, UsersServices>();
    }
}