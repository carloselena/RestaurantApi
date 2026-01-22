using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Ingredients.Queries.GetAllIngredients
{
    public record GetAllIngredientsQuery : IRequest<Response<List<IngredientDto>>>;
}
