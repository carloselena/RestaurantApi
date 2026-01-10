using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Ingredients.Commands.UpdateIngredient
{
    public class UpdateIngredientCommand : IRequest<Response<SaveIngredientResponse>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
