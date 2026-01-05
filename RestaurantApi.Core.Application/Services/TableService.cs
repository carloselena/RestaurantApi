using AutoMapper;
using RestaurantApi.Core.Application.DTOs.Table;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Services
{
    public class TableService : GenericService<TableDTO, AddTableDTO, UpdateTableDTO, Table>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public TableService(ITableRepository tableRepository, IMapper mapper) : base(tableRepository, mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }
    }
}
