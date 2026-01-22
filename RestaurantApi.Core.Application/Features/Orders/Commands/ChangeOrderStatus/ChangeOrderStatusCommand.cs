using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Orders.Commands.ChangeOrderStatus
{
    public record ChangeOrderStatusCommand(int Id) : IRequest<Response<int>>;
}
