using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestaurantApi.Infrastructure.Identity.Contexts;
using RestaurantApi.Infrastructure.Identity.Seeds;

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

            #region Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
            #endregion
        }

        public static async Task RunIdentitySeeds(this IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                    await DefaultRoles.SeedAsync(roleManager);
                    await DefaultMeseroUser.SeedAsync(userManager);
                    await DefaultAdminUser.SeedAsync(userManager);
                    await DefaultSuperAdminUser.SeedAsync(userManager);
                }
                catch (Exception ex) { }
            }
        }
    }
}
