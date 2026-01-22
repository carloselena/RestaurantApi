using MediatR;
using Restaurant.Core.Application.Features.Orders.Queries;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Orders.Queries.GetOrderById
{
    public record GetOrderByIdQuery(int Id) : IRequest<Response<OrderDto>>;
}
