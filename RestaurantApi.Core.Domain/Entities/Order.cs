using RestaurantApi.Core.Domain.Common;

namespace RestaurantApi.Core.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int TableId { get; set; }
        public double SubTotal { get; set; }
        public string Status { get; set; }

        public Table? Table { get; set; }
        public ICollection<OrderDishes>? Dishes { get; set; }
    }
}
