using Acl.Authentication.Service.Api.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Acl.Authentication.Service.Api.Extensions;

public static class MappersServiceExtension
{
    public static void MappersConfigure(this IServiceCollection services)
    {
        services.AddScoped<ILoginRequestMapper, LoginRequestMapper>();
        services.AddScoped<ILoginResponseMapper, LoginResponseMapper>();
    }
}