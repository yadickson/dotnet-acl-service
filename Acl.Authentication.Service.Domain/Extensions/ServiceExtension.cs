using Microsoft.Extensions.DependencyInjection;

namespace Acl.Authentication.Service.Domain.Extensions;

public static class ServiceExtension
{
    public static void DomainConfigure(this IServiceCollection services)
    {
        services.UseCasesConfigure();
    }
}