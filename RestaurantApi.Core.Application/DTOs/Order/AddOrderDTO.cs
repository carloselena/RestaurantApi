using RestaurantApi.Core.Application.Enums;

namespace RestaurantApi.Core.Application.DTOs.Order
{
    public class AddOrderDTO
    {
        public int TableId { get; set; }
        public List<int> DishesIds { get; set; }
        public double SubTotal { get; set; }

        public OrderStatus Status = OrderStatus.EN_PROCESO;
    }
}
