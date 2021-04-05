using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PokerApi.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace PokerApi.App_Start
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(IServiceCollection services, string assemblyName)
        {
            services.AddSwaggerGen(opt =>
            {
                // combine V2 in /v1/swagger.json so client generation works
                opt.DocInclusionPredicate((documentName, apiDescription) => true);
                opt.SwaggerDoc("v1", new OpenApiInfo
                {

                    Title = assemblyName,
                    Version = "v1",
                    Description = Constants.OPEN_API_SWAGGER_DOC_DESCRIPTION,
                    Contact = new OpenApiContact
                    {
                        Name = Constants.OPEN_API_CONTACT_NAME,
                        Email = Constants.OPEN_API_CONTACT_EMAIL
                    }
                });

                // Enable xml comments
                var xmlFile = $"{assemblyName}.xml";
                var binDirXmlPath = Path.Combine(Directory.CreateDirectory(AppContext.BaseDirectory).Parent.FullName, xmlFile);
                opt.IncludeXmlComments(binDirXmlPath);

                // Add filters if necessary

                // Use method name as OperationId
                opt.CustomOperationIds(apiDesc =>
                {
                    return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
                });
            });
        }
    }
}
