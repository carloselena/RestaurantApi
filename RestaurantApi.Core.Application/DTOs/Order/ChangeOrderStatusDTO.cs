using RestaurantApi.Core.Application.Enums;

namespace RestaurantApi.Core.Application.DTOs.Order
{
    public class ChangeOrderStatusDTO
    {
        public OrderStatus Status = OrderStatus.COMPLETADA;
    }
}
