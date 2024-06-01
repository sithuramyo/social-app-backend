using Google.Apis.Drive.v3;
using File = Google.Apis.Drive.v3.Data.File;
namespace GoogleDriveService.GoogleDriveServices.FileServices;

public class Upload
{
    private readonly DriveService _service;

    public Upload(DriveService service)
    {
        _service = service;
    }
    
    public async Task<string> UploadFile(string folderId, byte[] fileBytes,string fileName)
    {
        try
        {
            var fileMetadata = new File
            {
                Name = fileName,
                Parents = new List<string> { folderId },
                MimeType = "application/octet-stream"
            };
            using var stream = new MemoryStream(fileBytes);
            var request = _service.Files.Create(fileMetadata, stream, fileMetadata.MimeType);
            request.Fields = "id";
            await request.UploadAsync();
            var file = request.ResponseBody;
            return file.Id;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }
}