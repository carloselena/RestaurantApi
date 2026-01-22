using AutoMapper;
using MediatR;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<int>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IDishRepository dishRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _dishRepository = dishRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await OrderValidation.ValidateDishes(request.DishesIds, _dishRepository);

            var order = _mapper.Map<Order>(request);
            order = await _orderRepository.AddAsync(order);
            return new Response<int>(order.Id);
        }
    }
}
