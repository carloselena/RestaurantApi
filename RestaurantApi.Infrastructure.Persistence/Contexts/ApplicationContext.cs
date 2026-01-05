using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt) { }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Tables
            modelBuilder.Entity<Ingredient>()
                .ToTable("Ingredients");

            modelBuilder.Entity<Dish>()
                .ToTable("Dishes");

            modelBuilder.Entity<DishIngredients>()
                .ToTable("DishIngredients");

            modelBuilder.Entity<Table>()
                .ToTable("Tables");

            modelBuilder.Entity<Order>()
                .ToTable("Orders");

            modelBuilder.Entity<OrderDishes>()
                .ToTable("OrdersDishes");
            #endregion

            #region Primary Keys
            modelBuilder.Entity<Ingredient>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Dish>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<DishIngredients>()
                .HasKey(di => new { di.DishId, di.IngredientId });

            modelBuilder.Entity<Table>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<OrderDishes>()
                .HasKey(od => new { od.OrderId, od.DishId });
            #endregion

            #region Relationships
            modelBuilder.Entity<DishIngredients>()
                .HasOne(di => di.Dish)
                .WithMany(d => d.Ingredients)
                .HasForeignKey(di => di.DishId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DishIngredients>()
                .HasOne(di => di.Ingredient)
                .WithMany()
                .HasForeignKey(di => di.IngredientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Table>()
                .HasMany(t => t.Orders)
                .WithOne(o => o.Table)
                .HasForeignKey(o => o.TableId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderDishes>()
                .HasOne(od => od.Order)
                .WithMany(o => o.Dishes)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDishes>()
                .HasOne(od => od.Dish)
                .WithMany()
                .HasForeignKey(od => od.DishId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Properties Configuration
            modelBuilder.Entity<Ingredient>()
                .Property(i => i.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<Dish>()
                .Property(d => d.Name)
                .HasMaxLength(100);
            #endregion
        }
    }
}
