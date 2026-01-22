using RestaurantApi.Core.Application.Enums;

namespace RestaurantApi.Core.Application.Features.Tables.Queries
{
    public class TableDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int MaxPeopleOnTable { get; set; }
        public TableStatus Status { get; set; }
    }
}
