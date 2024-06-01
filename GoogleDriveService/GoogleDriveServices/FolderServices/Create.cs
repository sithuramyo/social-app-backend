using Google.Apis.Drive.v3;

namespace GoogleDriveService.GoogleDriveServices.FolderServices;

public class Create
{
    private readonly DriveService _service;

    public Create(DriveService service)
    {
        _service = service;
    }
    
    public async Task<string> CreateFolder(string folderName, string parentFolderId)
    {
        try
        {
            var folderMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new List<string> { parentFolderId },
            };

            var request = _service.Files.Create(folderMetadata);
            request.Fields = "id";

            var folder = await request.ExecuteAsync();
            return folder.Id;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}