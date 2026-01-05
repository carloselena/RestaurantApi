using AutoMapper;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Domain.Common;

namespace RestaurantApi.Core.Application.Services
{
    public class GenericService<DTO, AddDTO, UpdateDTO, Entity> : IGenericService<DTO, AddDTO, UpdateDTO, Entity>
        where DTO : class
        where AddDTO : class
        where UpdateDTO : class
        where Entity : BaseEntity
    {
        private readonly IGenericRepository<Entity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<AddDTO> Add(AddDTO addDTO)
        {
            Entity entity = _mapper.Map<Entity>(addDTO);
            entity = await _repository.AddAsync(entity);
            return _mapper.Map<AddDTO>(entity);
        }

        public virtual async Task<UpdateDTO> Update(int id, UpdateDTO updateDTO, Func<IQueryable<Entity>, IQueryable<Entity>>? includes = null)
        {
            Entity entity = await _repository.GetByIdAsync(id, includes);

            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found");

            _mapper.Map(updateDTO, entity);
            entity = await _repository.UpdateAsync(entity);
            return _mapper.Map<UpdateDTO>(entity);
        }

        public virtual async Task Delete(int id)
        {
            Entity entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found");

            await _repository.DeleteAsync(entity);
        }

        public virtual async Task<ICollection<DTO>> GetAll(Func<IQueryable<Entity>, IQueryable<Entity>>? includes = null)
        {
            var entities = await _repository.GetAllAsync(includes);
            return _mapper.Map<List<DTO>>(entities);
        }

        public virtual async Task<DTO> GetById(int id, Func<IQueryable<Entity>, IQueryable<Entity>>? includes = null)
        {
            Entity entity = await _repository.GetByIdAsync(id, includes);
            return _mapper.Map<DTO>(entity);
        }
    }
}
