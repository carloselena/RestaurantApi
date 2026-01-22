using MediatR;
using RestaurantApi.Core.Application.Wrappers;
using System.Text.Json.Serialization;

namespace RestaurantApi.Core.Application.Features.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(
        [property: JsonIgnore] int Id,
        IReadOnlyCollection<int> DishesIds
    ) : IRequest<Response<int>>;
}
