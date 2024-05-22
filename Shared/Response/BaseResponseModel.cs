using Shared.Enums;

namespace Shared.Response;

public class BaseResponseModel
{
    public string? ResponseCode { get; set; }
    public string? ResponseDescription { get; set; }
    public EnumResponseType ResponseType { get; set; }
    private bool IsSuccess => ResponseType == EnumResponseType.Success;
    public bool IsError => !IsSuccess;
    public void Set(string code)
    {
        ResponseCode = code;
        ResponseDescription = code;
        ResponseType = code.GetRespType();
    }
}