using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Domain.Entities;
using RestaurantApi.Infrastructure.Persistence.Contexts;

namespace RestaurantApi.Infrastructure.Persistence.Repositories
{
    public class TableRepository : GenericRepository<Table>, ITableRepository
    {
        private readonly ApplicationContext _dbContext;

        public TableRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
