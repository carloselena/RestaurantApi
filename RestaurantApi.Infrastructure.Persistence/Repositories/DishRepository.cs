using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Domain.Entities;
using RestaurantApi.Infrastructure.Persistence.Contexts;

namespace RestaurantApi.Infrastructure.Persistence.Repositories
{
    public class DishRepository : GenericRepository<Dish>, IDishRepository
    {
        private readonly ApplicationContext _dbContext;

        public DishRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<Dish> UpdateAsync(Dish dish)
        {
            var dishDB = await _dbContext.Dishes.Include(d => d.Ingredients).FirstOrDefaultAsync(d => d.Id == dish.Id);
            _dbContext.Entry(dishDB).CurrentValues.SetValues(dish);
            dishDB.Ingredients = dish.Ingredients;
            await _dbContext.SaveChangesAsync();
            return dish;
        }
    }
}
