using RestaurantApi.Core.Domain.Common;

namespace RestaurantApi.Core.Domain.Entities
{
    public class Dish : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int MaxPeopleQuantity { get; set; }
        public string Category { get; set; }
        public ICollection<DishIngredients>? Ingredients { get; set; }
    }
}
