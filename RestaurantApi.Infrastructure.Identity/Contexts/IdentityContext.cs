using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RestaurantApi.Infrastructure.Identity.Contexts
{
    public class IdentityContext : IdentityDbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>(e => e.ToTable("Users"));
            builder.Entity<IdentityRole>(e => e.ToTable("Roles"));
            builder.Entity<IdentityUserRole<string>>(e => e.ToTable("UserRoles"));
            builder.Entity<IdentityUserLogin<string>>(e => e.ToTable("UserLogins"));
            builder.Entity<IdentityUserClaim<string>>(e => e.ToTable("UserClaims"));
            builder.Entity<IdentityRoleClaim<string>>(e => e.ToTable("RoleClaims"));
            builder.Entity<IdentityUserToken<string>>(e => e.ToTable("UserTokens"));
        }
    }
}
