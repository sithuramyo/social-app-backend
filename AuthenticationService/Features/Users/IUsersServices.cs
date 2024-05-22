using Shared.Models.Users;

namespace AuthenticationService.Features.Users;

public interface IUsersServices
{
    Task<UsersRegisterResponseModel> UsersRegister(UsersRegisterRequestModel request,CancellationToken ct);
}