using AutoMapper;
using MediatR;
using Restaurant.Core.Application.Features.Orders.Queries;
using RestaurantApi.Core.Application.Exceptions;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Orders.Queries.GetAllTableOrders
{
    public class GetAllTableOrdersQueryHandler : IRequestHandler<GetAllTableOrdersQuery, Response<GetAllTableOrdersResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetAllTableOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<Response<GetAllTableOrdersResponse>> Handle(GetAllTableOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllTableOrdersAsync(request.TableId);
            if (orders == null || orders.Count == 0)
                throw new NotFoundException($"No hay órdenes para la mesa con id {request.TableId}");

            GetAllTableOrdersResponse response = new()
            {
                TableId = request.TableId,
                Orders = _mapper.Map<List<OrderDto>>(orders)
            };

            return new Response<GetAllTableOrdersResponse>(response)!;
        }
    }
}
