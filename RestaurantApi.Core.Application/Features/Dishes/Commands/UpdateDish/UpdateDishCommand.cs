using MediatR;
using RestaurantApi.Core.Application.Wrappers;
using System.Text.Json.Serialization;

namespace RestaurantApi.Core.Application.Features.Dishes.Commands.UpdateDish
{
    public record UpdateDishCommand(
        [property: JsonIgnore] int Id,
        double Price,
        IReadOnlyCollection<int> IngredientsIds
    ) : IRequest<Response<int>>;
}
