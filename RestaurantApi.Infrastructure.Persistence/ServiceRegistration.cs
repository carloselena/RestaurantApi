using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Infrastructure.Persistence.Contexts;
using RestaurantApi.Infrastructure.Persistence.Repositories;

namespace RestaurantApi.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(opt =>
                                                        opt.UseSqlServer(connectionString,
                                                        m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IIngredientRepository, IngredientRepository>();
            services.AddTransient<IDishRepository, DishRepository>();
            services.AddTransient<ITableRepository, TableRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            #endregion
        }
    }
}
