namespace Shared.Models.Users;

public class UsersRegisterRequestModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string ProfileImage { get; set; }
}