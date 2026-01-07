using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Application.Wrappers;
using RestaurantApi.Core.Domain.Settings;
using RestaurantApi.Infrastructure.Identity.Contexts;
using RestaurantApi.Infrastructure.Identity.Seeds;
using RestaurantApi.Infrastructure.Identity.Services;
using System.Text;

namespace RestaurantApi.Infrastructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration config, IHostEnvironment env)
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

            services.Configure<JWTSettings>(config.GetSection("JWTSettings"));

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = !env.IsDevelopment();
                opt.SaveToken = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = config["JWTSettings:Issuer"],
                    ValidAudience = config["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTSettings:Key"]!))
                };

                opt.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = c =>
                    {
                        var logger = c.HttpContext.RequestServices
                            .GetRequiredService<ILogger<IdentityLogging>>();
                        logger.LogWarning(c.Exception, "JWT inválido recibido");
                        return Task.CompletedTask;
                    },
                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(AccountResponse.Fail("Usted no tiene autorización"));
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(AccountResponse.Fail("Usted no tiene autorización para acceder a este recurso"));
                        return c.Response.WriteAsync(result);
                    }
                };
            });
            #endregion

            #region Services
            services.AddTransient<IAccountService, AccountService>();
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

    /// <summary>
    /// Clase solo para categorizar logs de JWT/Identity
    /// </summary>
    public class IdentityLogging { }
}
