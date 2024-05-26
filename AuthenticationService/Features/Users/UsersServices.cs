using AuthenticationService.Features.CommonServices;
using DatabaseService.AppContextModels;
using DatabaseService.ChangeModels;
using DatabaseService.DataModels.Authentication;
using Shared.Constants;
using Shared.Extensions;
using Shared.Models.Users;
using Shared.Response;

namespace AuthenticationService.Features.Users;

public class UsersServices : IUsersServices
{
    private readonly AppDbContext _context;

    public UsersServices(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UsersRegisterResponseModel> UsersRegister(UsersRegisterRequestModel request,CancellationToken ct)
    {
        UsersRegisterResponseModel model = new();
        if (request.Email.IsNullOrEmpty())
        {
            model.Response.Set(ResponseConstants.W0000);
            return model;
        }

        request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var users = request.Change();

        await _context.Users.AddAsync(users,ct);
        var usersSaveResult = await _context.SaveChangesAsync(ct);
        if (usersSaveResult <= 0)
        {
            model.Response.Set(ResponseConstants.E0000);
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