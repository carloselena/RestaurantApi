using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Ingredients.Commands.CreateIngredient
{
    public class CreateIngredientCommand : IRequest<Response<SaveIngredientResponse>>
    {
        public string Name { get; set; }
    }
}
