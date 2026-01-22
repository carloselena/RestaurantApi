using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Orders.Queries.GetAllTableOrders
{
    public record GetAllTableOrdersQuery(int TableId) : IRequest<Response<GetAllTableOrdersResponse>>;
}
