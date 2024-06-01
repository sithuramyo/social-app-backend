namespace SocialMediaService;

public class Startup
{
    private static IConfiguration _configuration { get; set; }

    public static void Configure(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}