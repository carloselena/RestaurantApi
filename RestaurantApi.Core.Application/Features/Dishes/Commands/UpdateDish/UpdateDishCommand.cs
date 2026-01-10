using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Dishes.Commands.UpdateDish
{
    public class UpdateDishCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public List<int> IngredientsIds { get; set; }
    }
}
