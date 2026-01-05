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

        public override async Task<Order> UpdateAsync(Order order)
        {
            var orderDB = await _dbContext.Orders.Include(o => o.Dishes).FirstOrDefaultAsync(o => o.Id == order.Id);
            _dbContext.Entry(orderDB).CurrentValues.SetValues(order);
            orderDB.Dishes = order.Dishes;
            await _dbContext.SaveChangesAsync();
            return order;
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
