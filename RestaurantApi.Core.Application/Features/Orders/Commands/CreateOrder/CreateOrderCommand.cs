using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(
        int TableId,
        IReadOnlyCollection<int> DishesIds,
        double SubTotal
    ) : IRequest<Response<int>>;
}
