using MediatR;
using RestaurantApi.Core.Application.Wrappers;
using System.Text.Json.Serialization;

namespace RestaurantApi.Core.Application.Features.Ingredients.Commands.UpdateIngredient
{
    public record UpdateIngredientCommand(
        [property: JsonIgnore] int Id,
        string Name
    ) : IRequest<Response<SaveIngredientResponse>>;
}
