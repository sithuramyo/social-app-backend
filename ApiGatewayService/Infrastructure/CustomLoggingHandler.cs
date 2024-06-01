using System.Net;
using Newtonsoft.Json.Linq;
using Shared.Constants;
using Shared.Extensions;
using Shared.Response;

namespace ApiGatewayService.Infrastructure;

public class CustomLoggingHandler : DelegatingHandler
{
    private readonly ILogger _logger;

    public CustomLoggingHandler(HttpMessageHandler innerHandler, ILogger logger) : base(innerHandler)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        HttpResponseMessage response = new();
        BaseSubResponseModel responseModel = new();
        JObject jobject = null;
        try
        {
            _logger.LogInformation("Sending request to {Url}", request.RequestUri);
            response = await base.SendAsync(request, cancellationToken);
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    responseModel.Response.Set(ResponseConstants.I0000);
                    jobject = JObject.Parse(responseModel.ToJson());
                    jobject["Response"]["ResponseDescription"] = "Unauthorized service";
                    responseModel = jobject.ToObject<BaseSubResponseModel>();
                    response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(responseModel.ToJson(), System.Text.Encoding.UTF8, "application/json")
                    };
                    break;
                case HttpStatusCode.Forbidden:
                    responseModel.Response.Set(ResponseConstants.I0001);
                    jobject = JObject.Parse(responseModel.ToJson());
                    jobject["Response"]["ResponseDescription"] = "Forbidden service";
                    responseModel = jobject.ToObject<BaseSubResponseModel>();
                    response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(responseModel.ToJson(), System.Text.Encoding.UTF8, "application/json")
                    };
                    break;
                case HttpStatusCode.NotFound:
                    responseModel.Response.Set(ResponseConstants.I0002);
                    jobject = JObject.Parse(responseModel.ToJson());
                    jobject["Response"]["ResponseDescription"] = "Not found";
                    responseModel = jobject.ToObject<BaseSubResponseModel>();
                    response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(responseModel.ToJson(), System.Text.Encoding.UTF8, "application/json")
                    };
                    break;
                case HttpStatusCode.MethodNotAllowed:
                    responseModel.Response.Set(ResponseConstants.I0003);
                    jobject = JObject.Parse(responseModel.ToJson());
                    jobject["Response"]["ResponseDescription"] = "Method Not Allowed";
                    responseModel = jobject.ToObject<BaseSubResponseModel>();
                    response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed)
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(responseModel.ToJson(), System.Text.Encoding.UTF8, "application/json")
                    };
                    break;
                case HttpStatusCode.BadGateway:
                    responseModel.Response.Set(ResponseConstants.I0004);
                    jobject = JObject.Parse(responseModel.ToJson());
                    jobject["Response"]["ResponseDescription"] = "Bad Gateway";
                    responseModel = jobject.ToObject<BaseSubResponseModel>();
                    response = new HttpResponseMessage(HttpStatusCode.BadGateway)
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(responseModel.ToJson(), System.Text.Encoding.UTF8, "application/json")
                    };
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    responseModel.Response.Set(ResponseConstants.I0005);
                    jobject = JObject.Parse(responseModel.ToJson());
                    jobject["Response"]["ResponseDescription"] = "Unavailable service";
                    responseModel = jobject.ToObject<BaseSubResponseModel>();
                    response = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(responseModel.ToJson(), System.Text.Encoding.UTF8, "application/json")
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _logger.LogInformation("Received response with status code {StatusCode}", response.StatusCode);
        }
        catch (Exception e)
        {
            _logger.LogInformation("Exception with status code {StatusCode}", response.StatusCode);
            responseModel.Response.Set(ResponseConstants.I0006);
            var errorJObject = JObject.Parse(responseModel.ToJson());
            errorJObject["Response"]["ResponseDescription"] = "Internal server error";
            responseModel = errorJObject.ToObject<BaseSubResponseModel>();

            response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseModel.ToJson(), System.Text.Encoding.UTF8, "application/json")
            };
        }

        return response;
    }
}