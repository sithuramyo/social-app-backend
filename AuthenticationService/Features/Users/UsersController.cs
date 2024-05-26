using AuthenticationService.Base;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Users;

namespace AuthenticationService.Features.Users;

[Route("authapi/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    private readonly IUsersServices _services;

    public UsersController(IUsersServices services)
    {
        _services = services;
    }

    [HttpPost("users-register")]
    public async Task<IActionResult> UsersRegister(UsersRegisterRequestModel request,CancellationToken ct)
    {
        UsersRegisterResponseModel model = new();
        try
        {
            model = await _services.UsersRegister(request,ct);
        }
        catch (Exception ex)
        {
            return SystemError(model, ex);
        }

        return OkWithLocalize(model);
    }
}