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
            var filterPipeline = context.ApiDescription
                .ActionDescriptor
                .FilterDescriptors;

            var authorizeFilters = filterPipeline
                .Select(filterInfo => filterInfo.Filter)
                .Where(filter => 
                    filter is AuthorizeFilter || filter is IAllowAnonymousFilter);

            if (authorizeFilters.Count() == 0 || 
                authorizeFilters.LastOrDefault() is IAllowAnonymousFilter)
            {
                return; // must be an anonymous method
            }

            var authorizeData = authorizeFilters
                .OfType<AuthorizeFilter>()?
                .SelectMany(authorizeFilter =>
                    authorizeFilter
                    .AuthorizeData);

            var scopes = authorizeData?.Where(data=> null != data.Roles).SelectMany(data =>
                        data?.Roles?.Split(','));

            if (operation.Security == null)
                operation.Security = new List<IDictionary<string, IEnumerable<string>>>();

            var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
            {
                {"oauth2", scopes}
            };

            operation.Security.Add(oAuthRequirements);
        }
    }
}
