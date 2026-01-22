using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Application.Features.Orders.Queries;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Response<List<OrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<Response<List<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository
                .GetAllAsync(q => q.Include(o => o.Dishes)!
                    .ThenInclude(od => od.Dish)
                );

            var response = _mapper.Map<List<OrderDto>>(orders ?? []);
            return new Response<List<OrderDto>>(response)!;
        }
    }
}
