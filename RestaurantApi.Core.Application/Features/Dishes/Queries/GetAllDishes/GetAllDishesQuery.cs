using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Dishes.Queries.GetAllDishes
{
    public class GetAllDishesQuery : IRequest<Response<List<DishDto>>>
    {
    }
}
