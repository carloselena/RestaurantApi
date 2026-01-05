namespace RestaurantApi.Core.Application.DTOs.Order
{
    public class TableOrdersDTO
    {
        public int TableId { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }
}
