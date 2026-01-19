using AutoMapper;
using MediatR;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RestaurantApi.Core.Application.Features.Tables.Queries.GetTableById
{
    public class GetTableByIdQueryHandler : IRequestHandler<GetTableByIdQuery, Response<TableDto>>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public GetTableByIdQueryHandler(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }
        public async Task<Response<TableDto>> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetByIdAsync(request.Id);
            if (table == null)
                return Response<TableDto>.Fail($"No hay mesa con id {request.Id}");

            var response = _mapper.Map<TableDto>(table);
            return Response<TableDto>.Success(response);
        }
    }
}
