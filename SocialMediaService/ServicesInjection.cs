using AuthenticationService.Features.CommonServices;
using GoogleDriveService.GoogleDriveServices;
using SocialMediaService.Features.Friends;
using SocialMediaService.Features.Posts;

namespace SocialMediaService;

public static class ServicesInjection
{
    private static IConfiguration _configuration { get; set; }

    public static void Configure(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFriendShipsService, FriendShipsService>();
        services.AddScoped<IPostService, PostService>();
        
        //Google Drive
        services.AddTransient<DriveHelper>();
        services.AddScoped<UploadService>();
    }
}