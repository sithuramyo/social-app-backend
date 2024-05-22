using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Shared.Constants;
using Shared.Models.Tokens;

namespace AuthenticationService.Features.CommonServices;

public class TokenService
{
    public static RefreshTokenModel GetRefreshToken()
    {
        var refreshToken = new RefreshTokenModel
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddDays(10)
        };
        return refreshToken;
    }

    public static JwtTokenModel GetJwtToken(string userId)
    {
        JwtTokenModel model = new();
        var claims = new List<Claim>
        {
            new(AuthenticationConstant.UserId, userId),
        };
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(AuthenticationConstant.Key));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        model.AccessTokenExpires = DateTime.Now.AddDays(7);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: model.AccessTokenExpires,
            signingCredentials: credentials);

        model.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
        return model;
    }
}