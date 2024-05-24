using Acl.Authentication.Service.Api.Extensions;
using Acl.Authentication.Service.Domain.Extensions;
using Acl.Authentication.Service.Infrastructure.Extensions;

namespace Acl.Authentication.Service.Extensions;

public static class ServiceExtension
{
    public static void AppConfigure(this IServiceCollection services)
    {
        services.ApiConfigure();
        services.DomainConfigure();
        services.InfrastructureConfigure();
    }
}