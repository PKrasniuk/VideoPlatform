using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VideoPlatform.Common.Infrastructure.Filters
{
    /// <summary>
    /// FormFileSwaggerFilter
    /// </summary>
    public class FormFileSwaggerFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (!string.Equals(context.ApiDescription.HttpMethod, HttpMethods.Post, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            var isFormFileFound = false;

            foreach (var parameter in operation.Parameters)
            {
                if (parameter is NonBodyParameter nonBodyParameter)
                {
                    var methodParameter =
                        context.ApiDescription.ParameterDescriptions.FirstOrDefault(x => x.Name == parameter.Name);
                    if (methodParameter != null)
                    {
                        if (typeof(IFormFile).IsAssignableFrom(methodParameter.Type))
                        {
                            nonBodyParameter.Type = "file";
                            nonBodyParameter.In = "formData";
                            isFormFileFound = true;
                        }
                        else if (typeof(IEnumerable<IFormFile>).IsAssignableFrom(methodParameter.Type))
                        {
                            nonBodyParameter.Items.Type = "file";
                            nonBodyParameter.In = "formData";
                            isFormFileFound = true;
                        }
                    }

                    parameter.Name = char.ToLowerInvariant(parameter.Name[0]) + parameter.Name.Substring(1);
                }
            }

            var formFileParameters = context.ApiDescription.ActionDescriptor.Parameters
                .Where(x => x.ParameterType == typeof(IFormFile)).ToList();
            foreach (var apiParameterDescription in formFileParameters)
            {
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = apiParameterDescription.Name,
                    In = "formData",
                    Description = "Upload File",
                    Required = true,
                    Type = "file"
                });
            }

            if (formFileParameters.Any())
            {
                foreach (var propertyInfo in typeof(IFormFile).GetProperties())
                {
                    var parametersWithTheSameName = operation.Parameters.Where(x => x.Name == propertyInfo.Name);
                    foreach (var parameterWithTheSameName in parametersWithTheSameName)
                    {
                        operation.Parameters.Remove(parameterWithTheSameName);
                    }
                }
            }

            if (isFormFileFound)
            {
                operation.Consumes.Add("multipart/form-data");
            }
        }
    }
}