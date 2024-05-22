using Shared.Enums;

namespace Shared.Response;

public class BaseSubResponseModel
{
    public BaseResponseModel Response { get; set; } = new(){ ResponseType = EnumResponseType.Default };
}