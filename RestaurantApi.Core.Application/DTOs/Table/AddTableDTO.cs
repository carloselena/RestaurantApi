using RestaurantApi.Core.Application.Enums;

namespace RestaurantApi.Core.Application.DTOs.Table
{
    public class AddTableDTO
    {
        public string Description { get; set; }
        public int MaxPeopleOnTable { get; set; }

        public TableStatus Status = TableStatus.DISPONIBLE;
    }
}
