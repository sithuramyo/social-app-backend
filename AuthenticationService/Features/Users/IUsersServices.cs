using Shared.Models.ForgetPassword;
using Shared.Models.Users;

namespace AuthenticationService.Features.Users;

public interface IUsersServices
{
    Task<UsersRegisterResponseModel> UsersRegister(UsersRegisterRequestModel request,CancellationToken ct);
    Task<ForgetPasswordResponseModel> ForgetPassword(ForgetPasswordRequestModel request,CancellationToken ct);
}