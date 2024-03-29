﻿using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Azure.Mobile.Server.Swagger;
using Swashbuckle.Application;
using tupapiService.App_Start;

namespace tupapiService
{
    public partial class Startup
    {
        public static void ConfigureSwagger(HttpConfiguration config)
        {
            // Use the custom ApiExplorer that applies constraints. This prevents
            // duplicate routes on /api and /tables from showing in the Swagger doc.
            config.Services.Replace(typeof (IApiExplorer), new MobileAppApiExplorer(config));
            config
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "tupapi");

                    // Tells the Swagger doc that any MobileAppController needs a
                    // ZUMO-API-VERSION header with default 2.0.0
                    c.OperationFilter<CustomMobileAppHeaderFilter>();

                    // Looks at attributes on properties to decide whether they are readOnly.
                    // Right now, this only applies to the DatabaseGeneratedAttribute.
                    c.SchemaFilter<MobileAppSchemaFilter>();
                })
                .EnableSwaggerUi();
        }
    }
}