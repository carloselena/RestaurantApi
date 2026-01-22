using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Application.Features.Dishes.Queries;

namespace Restaurant.Core.Application.Features.Orders.Queries
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public List<DishDto>? Dishes { get; set; }
        public double SubTotal { get; set; }
        public OrderStatus Status { get; set; }
    }
}
