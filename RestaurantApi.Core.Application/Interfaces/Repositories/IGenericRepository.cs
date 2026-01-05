using RestaurantApi.Core.Domain.Common;

namespace RestaurantApi.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity> where Entity : BaseEntity
    {
        Task<Entity> AddAsync(Entity entity);
        Task<Entity> UpdateAsync(Entity entity);
        Task DeleteAsync(Entity entity);
        Task<ICollection<Entity>> GetAllAsync(Func<IQueryable<Entity>, IQueryable<Entity>>? includes = null);
        Task<Entity> GetByIdAsync(int id, Func<IQueryable<Entity>, IQueryable<Entity>>? includes = null);        
    }
}
