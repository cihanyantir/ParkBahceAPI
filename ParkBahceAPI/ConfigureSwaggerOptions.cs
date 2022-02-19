using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ParkBahceAPI
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var desc in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                  desc.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo()
                  {
                      Title = $"ParkBahce API {desc.ApiVersion}",
                      Version = desc.ApiVersion.ToString()

                  });

            }
            ///////////////////SWAGGER BEARER//////////////////////////////
           

            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Description = "JWT AUTH Header using the Bearer Scheme. \r\n\r\n Enter Bearer",
                Name = "Authorization",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });


            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
            {
                {
                new OpenApiSecurityScheme   //By OpenApiSecurity Class
                {
                    Reference=new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    },
                    Scheme="oauth2",
                    Name="Bearer",
                    In=ParameterLocation.Header,
                },
                new List<string>() }

                });
        





            
                
                
                
                


            var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
            options.IncludeXmlComments(cmlCommentsFullPath); //addxml
            options.CustomSchemaIds(type => type.ToString());
        }

       
    }
}
