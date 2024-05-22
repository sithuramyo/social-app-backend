namespace Shared.Models.Tokens;

public class RefreshTokenModel
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
}