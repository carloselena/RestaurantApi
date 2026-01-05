using RestaurantApi.Core.Domain.Common;

namespace RestaurantApi.Core.Domain.Entities
{
    public class DishIngredients
    {
        public int DishId { get; set; }
        public int IngredientId { get; set; }

        public Dish? Dish { get; set; }
        public Ingredient? Ingredient { get; set; }
    }
}
