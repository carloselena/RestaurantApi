using AutoMapper;
using MediatR;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RestaurantApi.Core.Application.Features.Tables.Commands.ChangeTableStatus
{
    public class ChangeTableStatusCommandHandler : IRequestHandler<ChangeTableStatusCommand, Response<int>>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public ChangeTableStatusCommandHandler(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(ChangeTableStatusCommand command, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetByIdAsync(command.Id);
            if (table == null)
                return Response<int>.Fail($"No hay mesa con id {command.Id}");

            _mapper.Map(command, table);
            await _tableRepository.UpdateAsync(table);

            return Response<int>.Success(command.Id);
        }
    }
}
