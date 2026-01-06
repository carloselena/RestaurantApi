using Microsoft.AspNetCore.Identity;
using RestaurantApi.Core.Application.Enums;

namespace RestaurantApi.Infrastructure.Identity.Seeds
{
    public static class DefaultMeseroUser
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByEmailAsync("meserouser@email.com");
            if (user != null) return;

            var meseroUser = new IdentityUser
            {
                UserName = "meserouser",
                Email = "meserouser@email.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            await userManager.CreateAsync(meseroUser, "Pa$$word!123");
            await userManager.AddToRoleAsync(meseroUser, Roles.MESERO.ToString());
        }
    }
}
