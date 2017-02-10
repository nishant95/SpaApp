using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;

namespace SpaApi.Swashbuckle
{
    public class SwaggerAuthFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            var isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter);
            var allowAnonymous = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);

            if (!isAuthorized)
                return; // must be an anonymous method

            var scopes = context.ApiDescription.ActionDescriptor.FilterDescriptors
                .Select(filterInfo => filterInfo.Filter)
                .OfType<AuthorizeAttribute>()
                .SelectMany(attr => attr.Roles.Split(','))
                .Distinct();

            if (operation.Security == null)
                operation.Security = new List<IDictionary<string, IEnumerable<string>>>();

            var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
            {
                {"oauth2", new List<string> {"spa"}}
            };

            operation.Security.Add(oAuthRequirements);
        }
    }
}
