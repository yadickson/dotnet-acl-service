using Acl.Authentication.Service.Domain.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Acl.Authentication.Service.Domain.Extensions;

public static class UseCasesServiceExtension
{
    public static void UseCasesConfigure(this IServiceCollection services)
    {
        services.AddScoped<ILoginUseCase, LoginUseCase>();
    }
}