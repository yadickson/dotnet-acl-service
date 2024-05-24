using Microsoft.Extensions.DependencyInjection;

namespace Acl.Authentication.Service.Api.Extensions;

public static class ControllersServiceExtension
{
    public static void ControllersConfigure(this IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson(options => options.JsonConfigure());
    }
}