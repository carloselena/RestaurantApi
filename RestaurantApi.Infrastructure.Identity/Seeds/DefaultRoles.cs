using Microsoft.AspNetCore.Identity;
using RestaurantApi.Core.Application.Enums;

namespace RestaurantApi.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.MESERO.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.ADMIN.ToString()));
            await roleManager.CreateAsync(new IdentityRole("SUPERADMIN"));
        }
    }
}
