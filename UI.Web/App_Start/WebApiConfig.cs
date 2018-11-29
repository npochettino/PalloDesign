using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace UI.Web
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("API Default", "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });

            // configure json formatter
            //JsonMediaTypeFormatter jsonFormatter = configuration.Formatters.JsonFormatter;

            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var json = configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);
        }
    }
}