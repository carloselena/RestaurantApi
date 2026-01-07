using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Domain.Settings;
using RestaurantApi.Infrastructure.Shared.Services;

namespace RestaurantApi.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MailSettings>(config.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
