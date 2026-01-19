using AutoMapper;
using MediatR;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Tables.Commands.UpdateTable
{
    public class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand, Response<int>>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public UpdateTableCommandHandler(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(UpdateTableCommand command, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetByIdAsync(command.Id);
            if (table == null)
                return Response<int>.Fail($"No hay mesa con id {command.Id}");

            _mapper.Map(command, table);
            table = await _tableRepository.UpdateAsync(table);

            return Response<int>.Success(table.Id);
        }
    }
}
