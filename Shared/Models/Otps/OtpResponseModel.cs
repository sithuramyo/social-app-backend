using Shared.Response;

namespace Shared.Models.Otps;

public class OtpResponseModel : BaseSubResponseModel
{
    public int OtpExpires { get; set; }
    public string ExpireType { get; set; }
}