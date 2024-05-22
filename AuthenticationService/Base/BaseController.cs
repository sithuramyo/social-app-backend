using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Serilog;
using Shared.Constants;
using Shared.Extensions;
using Shared.Response;

namespace AuthenticationService.Base;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected IActionResult OkWithLocalize<T>(T obj)
    {
        var jobject = JObject.Parse(obj.ToJson());
        var respDesp = jobject["Response"]["ResponseDescription"]
            .ToString().GetResource();

        jobject["Response"]["ResponseDescription"] = respDesp;

        var model = jobject.ToString().ToObject<T>();

        return Ok(model);
    }
    
    protected IActionResult SystemError<T>(T model, Exception ex) where T : BaseSubResponseModel
    {
        LogException(ex);
        model.Response.Set(ResponseConstants.E0000);
        return Ok(model);
    }
    
    [NonAction]
    private static void LogException(Exception ex)
    {
        Log.Error($"Error : {ex.Message}, \n {ex.StackTrace}");
    }
}