using RestaurantApi.Core.Domain.Common;

namespace RestaurantApi.Core.Application.Interfaces.Services
{
    public interface IGenericService<Dto, AddDto, UpdateDto, Entity>
        where Dto : class
        where AddDto : class
        where UpdateDto : class
        where Entity : BaseEntity
    {
        Task<AddDto> Add(AddDto addDto);
        Task<UpdateDto> Update(int id, UpdateDto updateDto, Func<IQueryable<Entity>, IQueryable<Entity>>? includes = null);
        Task<ICollection<Dto>> GetAll(Func<IQueryable<Entity>, IQueryable<Entity>>? includes = null);
        Task<Dto> GetById(int id, Func<IQueryable<Entity>, IQueryable<Entity>>? includes = null);
        Task Delete(int id);
    }
}
