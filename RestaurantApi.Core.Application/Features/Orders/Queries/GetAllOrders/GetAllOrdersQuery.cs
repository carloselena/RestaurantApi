using MediatR;
using Restaurant.Core.Application.Features.Orders.Queries;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Orders.Queries.GetAllOrders
{
    public record GetAllOrdersQuery : IRequest<Response<List<OrderDto>>>;
}
