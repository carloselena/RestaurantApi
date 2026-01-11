using MediatR;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Dishes.Commands.CreateDish
{
    public class CreateDishCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int EnoughFor { get; set; }
        public DishCategories Category { get; set; }
        public List<int> IngredientsIds { get; set; }
    }
}
