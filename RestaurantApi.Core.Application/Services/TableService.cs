using AutoMapper;
using RestaurantApi.Core.Application.DTOs.Table;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Services
{
    public class TableService : GenericService<TableDTO, AddTableDTO, UpdateTableDTO, Table>, ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public TableService(ITableRepository tableRepository, IMapper mapper) : base(tableRepository, mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        public async Task ChangeStatus(int id, ChangeTableStatusDTO tableStatusDTO)
        {
            Table table = await _tableRepository.GetByIdAsync(id);
            if (table == null)
                throw new KeyNotFoundException();

            _mapper.Map(tableStatusDTO, table);
            await _tableRepository.UpdateAsync(table);
        }
    }
}
