using Yarp.ReverseProxy.Forwarder;

namespace ApiGatewayService.Infrastructure;

public class CustomForwarderHttpClientFactory : ForwarderHttpClientFactory
{
    private readonly ILogger<CustomForwarderHttpClientFactory> _logger;

    public CustomForwarderHttpClientFactory(ILogger<CustomForwarderHttpClientFactory> logger)
    {
        _logger = logger;
    }

    protected override HttpMessageHandler WrapHandler(ForwarderHttpClientContext context, HttpMessageHandler handler)
    {
        // Example: Add custom logging middleware around the handler
        return new CustomLoggingHandler(handler, _logger);
    }
}