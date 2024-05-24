using Acl.Authentication.Service.Api.Services;
using Acl.Authentication.Service.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MyTested.AspNetCore.Mvc;

namespace Acl.Authentication.Service.Api.Tests;

internal sealed class TestStartup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AppConfigure();
        services.ReplaceSingleton<ILoginService>(Mock.Of<ILoginService>());
    }

    public static void Configure(IApplicationBuilder application)
    {
        application.AppConfigure();
    }
}