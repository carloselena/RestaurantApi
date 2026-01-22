using Restaurant.Core.Application.Features.Orders.Queries;

namespace RestaurantApi.Core.Application.Features.Orders.Queries.GetAllTableOrders
{
    public class GetAllTableOrdersResponse
    {
        public int TableId { get; set; }
        public List<OrderDto> Orders { get; set; } = [];
    }
}
