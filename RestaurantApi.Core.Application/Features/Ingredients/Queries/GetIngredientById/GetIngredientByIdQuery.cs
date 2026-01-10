using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Ingredients.Queries.GetIngredientById
{
    public class GetIngredientByIdQuery : IRequest<Response<IngredientDto>>
    {
        public int Id { get; set; }
    }
}
