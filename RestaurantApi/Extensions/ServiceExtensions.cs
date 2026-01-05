using Asp.Versioning;
using Microsoft.OpenApi.Models;

namespace RestaurantApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", searchOption: SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => opt.IncludeXmlComments(xmlFile));

                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Restaurant API",
                    Description = "This Api will be responsible of all data distribution",
                    Contact = new OpenApiContact
                    {
                        Name = "Carlos Elena",
                        Email = "carloselenadev@gmail.com",
                        Url = new Uri("https://github.com/carloselena")
                    }
                });

                opt.DescribeAllParametersInCamelCase();
                opt.EnableAnnotations();
            });
        }

        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            })
            .AddApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
