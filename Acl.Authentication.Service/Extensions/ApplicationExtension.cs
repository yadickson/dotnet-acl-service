namespace Acl.Authentication.Service.Extensions;

public static class ApplicationExtension
{
    public static void AppConfigure(this IApplicationBuilder application)
    {
        application
            .UseRouting()
            .UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
    }
}