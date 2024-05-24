using AuthenticationService.Base;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Logins;

namespace AuthenticationService.Features.Logins;

[Route("authapi/[controller]")]
[ApiController]
public class LoginController : BaseController
{
    private readonly ILoginService _service;

    public LoginController(ILoginService service)
    {
        _service = service;
    }

    [HttpPost("users-login")]
    public async Task<IActionResult> Login(LoginRequestModel request, CancellationToken ct)
    {
        LoginResponseModel model = new();
        try
        {
            model = await _service.Login(request, ct);
        }
        catch (Exception ex)
        {
            return SystemError(model, ex);
        }

        return OkWithLocalize(model);
    }
}