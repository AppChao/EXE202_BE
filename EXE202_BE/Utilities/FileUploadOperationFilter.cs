using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EXE202_BE.Utilities
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var isFileUploadOperation = context.MethodInfo
                .GetParameters()
                .Any(p => p.ParameterType == typeof(IFormFile));

            if (isFileUploadOperation)
            {
                // Clear existing parameters to redefine them as form-data
                operation.Parameters.Clear();

                // Define the multipart/form-data request body
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["upId"] = new OpenApiSchema { Type = "integer", Format = "int32" },
                                    ["image"] = new OpenApiSchema { Type = "string", Format = "binary" }
                                },
                                Required = new HashSet<string> { "upId", "image" }
                            }
                        }
                    }
                };
            }
        }
    }
}