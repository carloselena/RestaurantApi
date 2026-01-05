using RestaurantApi.Core.Application.DTOs.Dish;
using RestaurantApi.Core.Application.Enums;

namespace RestaurantApi.Core.Application.DTOs.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public List<DishDTO>? Dishes { get; set; }
        public double SubTotal { get; set; }
        public OrderStatus Status { get; set; }
    }
}
