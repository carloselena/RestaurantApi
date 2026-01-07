using Microsoft.AspNetCore.Identity;
using RestaurantApi.Core.Application.Enums;

namespace RestaurantApi.Infrastructure.Identity.Seeds
{
    public static class DefaultSuperAdminUser
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByEmailAsync("superadminuser@email.com");
            if (user != null) return;

            var superAdminUser = new IdentityUser
            {
                UserName = "superadminuser",
                Email = "superadminuser@email.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            await userManager.CreateAsync(superAdminUser, "Pa$$word!123");
            await userManager.AddToRoleAsync(superAdminUser, "SUPERADMIN");
            await userManager.AddToRoleAsync(superAdminUser, Roles.ADMIN.ToString());
            await userManager.AddToRoleAsync(superAdminUser, Roles.MESERO.ToString());
        }
    }
}
