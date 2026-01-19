using AutoMapper;
using MediatR;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;
using RestaurantApi.Core.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RestaurantApi.Core.Application.Features.Tables.Commands.CreateTable
{
    public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, Response<int>>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public CreateTableCommandHandler(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateTableCommand command, CancellationToken cancellationToken)
        {
            var table = _mapper.Map<Table>(command);
            table = await _tableRepository.AddAsync(table);

            return Response<int>.Success(table.Id);
        }
    }
}
