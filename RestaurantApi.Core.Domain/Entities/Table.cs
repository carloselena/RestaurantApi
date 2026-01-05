using RestaurantApi.Core.Domain.Common;

namespace RestaurantApi.Core.Domain.Entities
{
    public class Table : BaseEntity
    {
        public string Description { get; set; }
        public int MaxPeopleOnTable { get; set; }
        public string Status { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
