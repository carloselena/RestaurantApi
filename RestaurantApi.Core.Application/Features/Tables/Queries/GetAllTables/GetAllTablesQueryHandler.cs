using AutoMapper;
using MediatR;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Tables.Queries.GetAllTables
{
    public class GetAllTablesQueryHandler : IRequestHandler<GetAllTablesQuery, Response<List<TableDto>>>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public GetAllTablesQueryHandler(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }
        public async Task<Response<List<TableDto>>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
        {
            var tables = await _tableRepository.GetAllAsync();
            if (tables == null || tables.Count == 0)
                return Response<List<TableDto>>.Fail("No hay mesas");

            var response = _mapper.Map<List<TableDto>>(tables);
            return Response<List<TableDto>>.Success(response);
        }
    }
}
