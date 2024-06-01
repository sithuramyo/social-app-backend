using System.Text.Json;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3.Data;
using GoogleDriveService.GoogleDriveServices;
using GoogleDriveService.GoogleDriveServices.FileServices;
using GoogleDriveService.GoogleDriveServices.FolderServices;
using Newtonsoft.Json.Linq;
using Shared.Constants;
using Shared.Extensions;
using Shared.Models.Credentials;

namespace AuthenticationService.Features.CommonServices;

public class UploadService
{
    private readonly DriveHelper _driveHelper;

    public UploadService(DriveHelper driveHelper)
    {
        _driveHelper = driveHelper;
    }

    public async Task<string> SaveProfileImage(string image,string userName)
    {
        var driveService = _driveHelper.GetDriveService(GoogleDriveConstants.GoogleDriveCredential);
        var createFolderService = new Create(driveService);
        var fileByte = Convert.FromBase64String(image);
        var fileName = StringsExtension.GetImageName();
        var folderId = await createFolderService.CreateFolder(userName,GoogleDriveConstants.UsersImagesFolderId);
        var uploadFileService = new Upload(driveService);
        var fileId = await uploadFileService.UploadFile(folderId,fileByte,fileName);
        await MakeFileToPublic(fileId);
        var imageFilePath = fileId.GetGoogleDriveFileUrl();
        return imageFilePath;
    }

    public async Task<string> SavePostsMedia(string fileValue, string folderName)
    {
        var driveService = _driveHelper.GetDriveService(GoogleDriveConstants.GoogleDriveCredential);
        var createFolderService = new Create(driveService);
        var fileByte = Convert.FromBase64String(fileValue);
        var fileName = StringsExtension.GetImageName();
        var folderId = await createFolderService.CreateFolder(folderName,GoogleDriveConstants.PostsMediaFolderId);
        var uploadFileService = new Upload(driveService);
        var fileId = await uploadFileService.UploadFile(folderId,fileByte,fileName);
        await MakeFileToPublic(fileId);
        var imageFilePath = fileId.GetGoogleDriveFileUrl();
        return imageFilePath;
    }

    private async Task MakeFileToPublic(string fileId)
    {
        try
        {
            var driveService = _driveHelper.GetDriveService(GoogleDriveConstants.GoogleDriveCredential);
            // Create the permission object
            Permission permission = new()
            {
                Role = "reader",
                Type = "anyone"
            };

            // Add the permission to the file
            await driveService.Permissions.Create(permission, fileId).ExecuteAsync();
        }
        catch (Exception ex)
        {
           throw new Exception(ex.Message);
        }
    }
}