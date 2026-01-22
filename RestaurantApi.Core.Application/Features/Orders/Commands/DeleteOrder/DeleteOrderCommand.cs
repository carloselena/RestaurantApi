using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(int Id) : IRequest<Response<int>>;
}
