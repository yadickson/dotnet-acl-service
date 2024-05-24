using Microsoft.Extensions.DependencyInjection;

namespace Acl.Authentication.Service.Api.Extensions;

public static class ServiceExtension
{
    public static void ApiConfigure(this IServiceCollection services)
    {
        services.ControllersConfigure();
        services.ServicesConfigure();
        services.MappersConfigure();
    }
}