using MediatR;
using RestaurantApi.Core.Application.Exceptions;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Tables.Commands.ChangeTableStatus
{
    public class ChangeTableStatusCommandHandler : IRequestHandler<ChangeTableStatusCommand, Response<int>>
    {
        private readonly ITableRepository _tableRepository;

        public ChangeTableStatusCommandHandler(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }
        public async Task<Response<int>> Handle(ChangeTableStatusCommand request, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetByIdAsync(request.Id);
            if (table == null)
                throw new NotFoundException($"No se encontró la mesa con id {request.Id}");

            table.Status = request.Status.ToString();
            await _tableRepository.UpdateAsync(table);

            return new Response<int>(request.Id);
        }
    }
}
