using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Ingredients.Queries.GetIngredientById
{
    public record GetIngredientByIdQuery(int Id) : IRequest<Response<IngredientDto>>;
}
