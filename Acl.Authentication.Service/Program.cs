using Acl.Authentication.Service.Extensions;

// [assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;
var logging = builder.Logging;

logging.AddLog4Net();
services.AppConfigure();

var application = builder.Build();

application.UsePathBase(configuration.GetValue<string>("PrefixApiPathBase"));
application.AppConfigure();

application.Run();