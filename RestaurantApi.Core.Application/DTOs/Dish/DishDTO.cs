using RestaurantApi.Core.Application.DTOs.Ingredient;
using RestaurantApi.Core.Application.Enums;

namespace RestaurantApi.Core.Application.DTOs.Dish
{
    public class DishDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int EnoughFor { get; set; }
        public DishCategories Category { get; set; }
        public List<IngredientDTO>? Ingredients { get; set; }
    }
}
