using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Domain.Entities;
using RestaurantApi.Infrastructure.Persistence.Contexts;

namespace RestaurantApi.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ApplicationContext _dbContext;

        public OrderRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Order>> GetAllTableOrdersAsync(int tableId)
        {
            return await _dbContext.Orders.Include(o => o.Dishes)
                            .ThenInclude(od => od.Dish)
                            .Where(o => o.TableId == tableId)
                            .ToListAsync();
        }
    }
}
