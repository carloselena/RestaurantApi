using AutoMapper;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Domain.Common;

namespace RestaurantApi.Core.Application.Services
{
    public class GenericService<Dto, AddDto, UpdateDto, Entity> : IGenericService<Dto, AddDto, UpdateDto, Entity>
        where Dto : class
        where AddDto : class
        where UpdateDto : class
        where Entity : BaseEntity
    {
        private readonly IGenericRepository<Entity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<AddDto> Add(AddDto addDto)
        {
            Entity entity = _mapper.Map<Entity>(addDto);
            entity = await _repository.AddAsync(entity);
            return _mapper.Map<AddDto>(entity);
        }

        public virtual async Task<UpdateDto> Update(int id, UpdateDto updateDto, Func<IQueryable<Entity>, IQueryable<Entity>>? includes = null)
        {
            Entity entity = await _repository.GetByIdAsync(id, includes);

            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found");

            _mapper.Map(updateDto, entity);
            entity = await _repository.UpdateAsync(entity);
            return _mapper.Map<UpdateDto>(entity);
        }

        public virtual async Task Delete(int id)
        {
            Entity entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found");

            await _repository.DeleteAsync(entity);
        }

        public virtual async Task<ICollection<Dto>> GetAll(Func<IQueryable<Entity>, IQueryable<Entity>>? includes = null)
        {
            var entities = await _repository.GetAllAsync(includes);
            return _mapper.Map<List<Dto>>(entities);
        }

        public virtual async Task<Dto> GetById(int id, Func<IQueryable<Entity>, IQueryable<Entity>>? includes = null)
        {
            Entity entity = await _repository.GetByIdAsync(id, includes);
            return _mapper.Map<Dto>(entity);
        }
    }
}
