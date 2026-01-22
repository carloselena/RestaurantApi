using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Dishes.Queries.GetDishById
{
    public record GetDishByIdQuery(int Id) : IRequest<Response<DishDto>>;
}
