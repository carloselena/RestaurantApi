using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Application.Exceptions;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Orders.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusCommandHandler : IRequestHandler<ChangeOrderStatusCommand, Response<int>>
    {
        private readonly IOrderRepository _orderRepository;

        public ChangeOrderStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Response<int>> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id, 
                                                            q => q.Include(o => o.Dishes)
                                                            .ThenInclude(od => od.Dish));
            if (order == null)
                throw new NotFoundException($"No se encontró la orden con id {request.Id}");

            order.Status = OrderStatus.COMPLETADA.ToString();
            await _orderRepository.UpdateAsync(order);
            return new Response<int>(order.Id);
        }
    }
}
