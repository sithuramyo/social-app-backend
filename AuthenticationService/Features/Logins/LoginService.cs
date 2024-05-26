using AuthenticationService.Features.CommonServices;
using DatabaseService.AppContextModels;
using DatabaseService.DataModels.Authentication;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Extensions;
using Shared.Models.Logins;

namespace AuthenticationService.Features.Logins;

public class LoginService : ILoginService
{
    private readonly AppDbContext _context;

    public LoginService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LoginResponseModel> Login(LoginRequestModel request,CancellationToken ct)
    {
        LoginResponseModel model = new();
        if (request.Email.IsNullOrEmpty() || request.Password.IsNullOrEmpty())
        {
            model.Response.Set(ResponseConstants.W0000);
            return model;
        }

        var users = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email && !x.IsDeleted,ct);
        if (users is null)
        {
            model.Response.Set(ResponseConstants.W0002);
            return model;
        }

        var isValid = BCrypt.Net.BCrypt.Verify(request.Password, users.Password);
        
        if (!isValid)
        {
            model.Response.Set(ResponseConstants.W0003);
            return model;
        }
        
        var accessTokenModel = TokenService.GetJwtToken(users.UserId);
        var refreshTokenModel = TokenService.GetRefreshToken();

        Login login = new()
        {
            UserId = users.UserId,
            AccessToken = accessTokenModel.AccessToken,
            RefreshToken = refreshTokenModel.Token,
            RefreshTokenExpires = refreshTokenModel.Expires
        };
        
        await _context.Login.AddAsync(login,ct);
        var loginResult = await _context.SaveChangesAsync(ct);
        
        if (loginResult <= 0)
        {
            model.Response.Set(ResponseConstants.E0000);
            return model;
        }

        model.AccessTokenType = AuthenticationConstant.AccessTokenType;
        model.AccessToken = accessTokenModel.AccessToken;
        model.AccessTokenExpires = accessTokenModel.ExpireCount * 24;
        model.RefreshToken = refreshTokenModel.Token;
        model.RefreshTokenExpires = refreshTokenModel.ExpireCount * 24;
        model.Response.Set(ResponseConstants.S0000);
        return model;
    }
}