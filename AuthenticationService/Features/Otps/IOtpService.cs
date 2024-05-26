using Shared.Models.Otps;

namespace AuthenticationService.Features.Otps;

public interface IOtpService
{
    Task<OtpResponseModel> GetOtp(OtpRequestModel request,CancellationToken ct);
    Task<ValidateOtpResponseModel> ValidateOtp(ValidateOtpRequestModel request, CancellationToken ct);
}