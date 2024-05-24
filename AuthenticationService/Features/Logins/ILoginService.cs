using Shared.Models.Logins;

namespace AuthenticationService.Features.Logins;

public interface ILoginService
{
    Task<LoginResponseModel> Login(LoginRequestModel request,CancellationToken ct);
}