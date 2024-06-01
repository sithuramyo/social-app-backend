using System.Linq.Expressions;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json.Linq;
using Shared.Constants;
using Shared.Extensions;
using Shared.Response;

namespace ApiGatewayService.Infrastructure;

public class GlobalExceptionHandler 
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Capture the response
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);
        
        responseBody.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(responseBody).ReadToEndAsync();
        responseBody.Seek(0, SeekOrigin.Begin);
        
        BaseSubResponseModel responseModel = new();
        context.Response.Body = originalBodyStream;
        switch (context.Response.StatusCode)
        {
            case StatusCodes.Status401Unauthorized:
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status200OK;
                context.Response.ContentType = "application/json";
                responseModel.Response.Set(ResponseConstants.I0000);
                var jobject = JObject.Parse(responseModel.ToJson());
                jobject["Response"]["ResponseDescription"] = "Unauthorized service";
                responseModel = jobject.ToObject<BaseSubResponseModel>();
                await context.Response.WriteAsJsonAsync(responseModel);
                break;

            case StatusCodes.Status403Forbidden:
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status200OK;
                context.Response.ContentType = "application/json";
                responseModel.Response.Set(ResponseConstants.I0001); // Assume ResponseConstants.I0001 is defined for 403
                jobject = JObject.Parse(responseModel.ToJson());
                jobject["Response"]["ResponseDescription"] = "Forbidden service";
                responseModel = jobject.ToObject<BaseSubResponseModel>();
                await context.Response.WriteAsJsonAsync(responseModel);
                break;
            
            case StatusCodes.Status404NotFound:
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status200OK;
                context.Response.ContentType = "application/json";
                responseModel.Response.Set(ResponseConstants.I0002); // Assume ResponseConstants.I0002 is defined for 404
                jobject = JObject.Parse(responseModel.ToJson());
                jobject["Response"]["ResponseDescription"] = "Service is not found";
                responseModel = jobject.ToObject<BaseSubResponseModel>();
                await context.Response.WriteAsJsonAsync(responseModel);
                break;
        
            case StatusCodes.Status502BadGateway:
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status200OK;
                context.Response.ContentType = "application/json";
                responseModel.Response.Set(ResponseConstants.I0003); // Assume ResponseConstants.I0003 is defined for 502
                jobject = JObject.Parse(responseModel.ToJson());
                jobject["Response"]["ResponseDescription"] = "Service is under maintenance";
                responseModel = jobject.ToObject<BaseSubResponseModel>();
                await context.Response.WriteAsJsonAsync(responseModel);
                break;

            default:
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
                break;
        }

        context.Response.Body = originalBodyStream;
    }
}