using DatabaseService.DataModels;
using DatabaseService.DataModels.Authentication;
using Shared.Models.Users;

namespace DatabaseService.ChangeModels;

public static class ChangeModel
{
    public static Users Change(this UsersRegisterRequestModel request)
    {
        return new Users
        {
            UserId = Guid.NewGuid().ToString(),
            Name = request.Name,
            Password = request.Password,
            Email = request.Email,
            DateOfBirth = request.DateOfBirth,
            ProfileImagePath = request.ProfileImage,
            CreatedDate = DateTime.Now
        };
    }
}