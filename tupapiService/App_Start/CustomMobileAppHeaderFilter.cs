using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;
using Microsoft.Azure.Mobile.Server.Config;
using Swashbuckle.Swagger;

namespace tupapiService.App_Start
{
    public class CustomMobileAppHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if (apiDescription == null)
            {
                throw new ArgumentNullException(nameof(apiDescription));
            }

            var filters = apiDescription.ActionDescriptor.ControllerDescriptor.GetFilters();
            var mobileAppFilter = filters.Where(f => f is MobileAppControllerAttribute);

            if (mobileAppFilter.Any())
            {
                if (operation.parameters == null)
                {
                    operation.parameters = new List<Parameter>();
                }

                operation.parameters.Add(new Parameter
                {
                    name = "ZUMO-API-VERSION",
                    @in = "header",
                    type = "string",
                    required = true,
                    @default = "2.0.0"
                });

                operation.parameters.Add(new Parameter
                {
                    name = "X-ZUMO-AUTH",
                    @in = "header",
                    type = "string",
                    required = false,
                    @default = null
                });
            }
        }
    }
}