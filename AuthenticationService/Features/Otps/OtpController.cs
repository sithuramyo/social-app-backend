using Microsoft.AspNetCore.Mvc;
using Shared.Models.Otps;

namespace AuthenticationService.Features.Otps;

[Route("authapi/[controller]")]
[ApiController]
public class OtpController : BaseController
{
    private readonly IOtpService _service;

    public OtpController(IOtpService service)
    {
        _service = service;
    }

    [HttpPost("get-otp")]
    public async Task<IActionResult> GetOtp(OtpRequestModel request, CancellationToken ct)
    {
        OtpResponseModel model = new();
        try
        {
            model = await _service.GetOtp(request, ct);
        }
        catch (Exception ex)
        {
            return SystemError(model, ex);
        }

        return OkWithLocalize(model);
    }

    [HttpPost("validate-otp")]
    public async Task<IActionResult> ValidateOtp(ValidateOtpRequestModel request, CancellationToken ct)
    {
        ValidateOtpResponseModel model = new();
        try
        {
            model = await _service.ValidateOtp(request, ct);
        }
        catch (Exception ex)
        {
            return SystemError(model, ex);
        }

        return OkWithLocalize(model);
    }
}