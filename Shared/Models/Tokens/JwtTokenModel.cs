namespace Shared.Models.Tokens;

public class JwtTokenModel
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpires { get; set; }
    public int ExpireCount { get; set; }
}