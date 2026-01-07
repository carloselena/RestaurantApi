using Microsoft.AspNetCore.Identity;
using RestaurantApi.Core.Application.Enums;

namespace RestaurantApi.Infrastructure.Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByEmailAsync("adminuser@email.com");
            if (user != null) return;

            var adminUser = new IdentityUser
            {
                UserName = "adminuser",
                Email = "adminuser@email.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            await userManager.CreateAsync(adminUser, "Pa$$word!123");
            await userManager.AddToRoleAsync(adminUser, Roles.ADMIN.ToString());
        }
    }
}
