using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Dishes.Queries.GetDishById
{
    public class GetDishByIdQuery : IRequest<Response<DishDto>>
    {
        public int Id { get; set; }
    }
}
