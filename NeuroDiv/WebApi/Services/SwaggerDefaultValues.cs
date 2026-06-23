using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace WebApi.Services
{
    public class SwaggerDefaultValues : IOperationFilter
    {
        /// <summary>
        /// Applies the filter to the specified operation using the given context.
        /// </summary>
        /// <param name="operation">The operation to apply the filter to.</param>
        /// <param name="context">The current operation filter context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated |= apiDescription.IsDeprecated();

            if (operation.Parameters == null)
            {
                return;
            }

            for (var i = 0; i < operation.Parameters.Count; i++)
            {
                var parameter = operation.Parameters[i];
                var description = apiDescription.ParameterDescriptions
                    .First(p => p.Name == parameter.Name);

                var mutableParameter = new OpenApiParameter
                {
                    Name = parameter.Name,
                    In = parameter.In,
                    Description = parameter.Description ?? description.ModelMetadata?.Description,
                    Required = description.IsRequired,
                    Schema = parameter.Schema,
                    Style = parameter.Style,
                    Explode = parameter.Explode,
                    AllowEmptyValue = parameter.AllowEmptyValue,
                    Deprecated = parameter.Deprecated,
                };

                operation.Parameters[i] = mutableParameter;
            }

            const string captureName = "routeParameter";

            var httpMethodAttributes = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<Microsoft.AspNetCore.Mvc.Routing.HttpMethodAttribute>();

            var httpMethodWithOptional = httpMethodAttributes?.FirstOrDefault(m => m.Template?.Contains("?") ?? false);
            if (httpMethodWithOptional == null)
                return;

            string regex = $"{{(?<{captureName}>\\w+)\\?}}";

            var matches = System.Text.RegularExpressions.Regex.Matches(httpMethodWithOptional.Template, regex);

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                var name = match.Groups[captureName].Value;

                var index = operation.Parameters
                    .Select((p, idx) => (p, idx))
                    .FirstOrDefault(x => x.p.In == ParameterLocation.Path && x.p.Name == name)
                    .idx;

                if (operation.Parameters[index] is OpenApiParameter routeParam)
                {
                    routeParam.Required = false;
                }
            }
        }
    }
}