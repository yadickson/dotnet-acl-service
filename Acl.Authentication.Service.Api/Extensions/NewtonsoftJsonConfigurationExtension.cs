using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Acl.Authentication.Service.Api.Extensions;

public static class NewtonsoftJsonConfigurationExtension
{
    public static void JsonConfigure(this MvcNewtonsoftJsonOptions options)
    {
        options.UseCamelCasing(true);
        options.SerializerSettings.Formatting = Formatting.Indented;
        options.SerializerSettings.Culture = CultureInfo.CurrentCulture;
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
        options.SerializerSettings.FloatFormatHandling = FloatFormatHandling.DefaultValue;
        options.SerializerSettings.FloatParseHandling = FloatParseHandling.Decimal;
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    }
}