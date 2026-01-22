using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Ingredients.Commands.CreateIngredient
{
    public record CreateIngredientCommand(string Name) : IRequest<Response<SaveIngredientResponse>>;
}
