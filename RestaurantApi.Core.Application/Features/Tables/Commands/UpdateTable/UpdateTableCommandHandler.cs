using AutoMapper;
using MediatR;
using RestaurantApi.Core.Application.Exceptions;
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
        public async Task<Response<int>> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetByIdAsync(request.Id);
            if (table == null)
                throw new NotFoundException($"No se encontró la mesa con id {request.Id}");

            _mapper.Map(request, table);
            await _tableRepository.UpdateAsync(table);

            return new Response<int>(table.Id);
        }
    }
}
