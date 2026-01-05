using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Domain.Common;
using RestaurantApi.Infrastructure.Persistence.Contexts;

namespace RestaurantApi.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : BaseEntity
    {
        private readonly ApplicationContext _dbContext;

        public GenericRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<Entity> UpdateAsync(Entity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            _dbContext.Set<Entity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<ICollection<Entity>> GetAllAsync(Func<IQueryable<Entity>, IQueryable<Entity>>? includes = null)
        {
            IQueryable<Entity> query = _dbContext.Set<Entity>().AsNoTracking();

            if (includes is not null)
                query = includes(query);

            return await query.ToListAsync();
        }

        public virtual async Task<Entity> GetByIdAsync(int id, Func<IQueryable<Entity>, IQueryable<Entity>>? includes = null)
        {
            IQueryable<Entity> query = _dbContext.Set<Entity>().AsNoTracking();

            if (includes is not null)
                query = includes(query);

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
