using MediatR;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Dishes.Commands.CreateDish
{
    public record CreateDishCommand(
        string Name,
        double Price,
        int EnoughFor,
        DishCategories Category,
        IReadOnlyCollection<int> IngredientsIds
    ) : IRequest<Response<int>>;
}
