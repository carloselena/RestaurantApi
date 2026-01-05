using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RestaurantApi.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
