using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Response<int>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IDishRepository dishRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _dishRepository = dishRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            await OrderValidation.ValidateDishes(request.DishesIds, _dishRepository);

            var order = await _orderRepository.GetByIdAsync(request.Id, q => q.Include(o => o.Dishes));

            OrderValidation.SyncDishes(order, request.DishesIds);

            await _orderRepository.UpdateAsync(order);
            return new Response<int>(order.Id);
        }
    }
}
