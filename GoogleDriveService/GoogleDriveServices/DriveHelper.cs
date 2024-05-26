using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace GoogleDriveService.GoogleDriveServices;

public class DriveHelper
{
    public DriveService GetDriveService(string credentials)
    {
        try
        {
            GoogleCredential credential;
            using (var stream = new FileStream(credentials, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(DriveService.ScopeConstants.Drive);
            }
                
            var driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Social App",
            });

            return driveService;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}