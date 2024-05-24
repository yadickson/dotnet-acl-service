using Acl.Authentication.Service.Api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Acl.Authentication.Service.Api.Extensions;

public static class ServicesServiceExtension
{
    public static void ServicesConfigure(this IServiceCollection services)
    {
        services.AddScoped<ILoginService, LoginService>();
    }
}