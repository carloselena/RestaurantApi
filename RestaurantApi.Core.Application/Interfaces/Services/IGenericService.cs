using RestaurantApi.Core.Domain.Common;

namespace RestaurantApi.Core.Application.Interfaces.Services
{
    public interface IGenericService<DTO, AddDTO, UpdateDTO, Entity>
        where DTO : class
        where AddDTO : class
        where UpdateDTO : class
        where Entity : BaseEntity
    {
        Task<AddDTO> Add(AddDTO addDTO);
        Task<UpdateDTO> Update(int id, UpdateDTO updateDTO, Func<IQueryable<Entity>, IQueryable<Entity>>? includes = null);
        Task<ICollection<DTO>> GetAll(Func<IQueryable<Entity>, IQueryable<Entity>>? includes = null);
        Task<DTO> GetById(int id, Func<IQueryable<Entity>, IQueryable<Entity>>? includes = null);
        Task Delete(int id);
    }
}
