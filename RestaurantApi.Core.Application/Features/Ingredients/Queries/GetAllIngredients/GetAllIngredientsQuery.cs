using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Ingredients.Queries.GetAllIngredients
{
    public class GetAllIngredientsQuery : IRequest<Response<IList<IngredientDto>>>
    {
    }
}
