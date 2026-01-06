using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantApi.Infrastructure.Identity.Contexts;

namespace RestaurantApi.Infrastructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            #region Contexts
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<IdentityContext>(opt => 
                                                        opt.UseSqlServer(connectionString,
                                                        m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
            #endregion
        }
    }
}
