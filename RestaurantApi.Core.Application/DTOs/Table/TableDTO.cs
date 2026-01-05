using RestaurantApi.Core.Application.Enums;

namespace RestaurantApi.Core.Application.DTOs.Table
{
    public class TableDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int MaxPeopleOnTable { get; set; }
        public TableStatus Status { get; set; }
    }
}
