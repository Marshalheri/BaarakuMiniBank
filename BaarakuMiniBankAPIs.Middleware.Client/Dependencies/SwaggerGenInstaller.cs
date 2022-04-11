using BaarakuMiniBankAPIs.Middleware.Client.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace BaarakuMiniBankAPIs.Middleware.Client.Dependencies
{
    public static class SwaggerGenInstaller
    {
        public static void AddSwaggerGenHandler(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Baaraku Mini Bank APIs", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.OperationFilter<SwaggerHeaderFilter>();
            });
        }
    }
}
