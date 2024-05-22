using Shared.Response;

namespace Shared.Models.Users;

public class UsersRegisterResponseModel : BaseSubResponseModel
{
    public string AccessTokenType { get; set; }
    public string AccessToken { get; set; }
    public int AccessTokenExpires { get; set; }
    public string RefreshToken { get; set; }
    public int RefreshTokenExpires { get; set; }
}