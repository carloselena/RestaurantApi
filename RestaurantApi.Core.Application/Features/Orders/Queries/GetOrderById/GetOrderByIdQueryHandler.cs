using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Application.Features.Orders.Queries;
using RestaurantApi.Core.Application.Exceptions;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Response<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<Response<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository
                .GetByIdAsync(
                    request.Id, q => q.Include(o => o.Dishes)!
                    .ThenInclude(od => od.Dish)
                );

            if (order == null)
                throw new NotFoundException($"No se encontró la orden con id {request.Id}");

            var response = _mapper.Map<OrderDto>(order);
            return new Response<OrderDto>(response)!;
        }
    }
}
