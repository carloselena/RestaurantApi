using RestaurantApi.Core.Application.Enums;

namespace RestaurantApi.Core.Application.DTOs.Dish
{
    public class AddDishDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int EnoughFor { get; set; }
        public DishCategories Category { get; set; }
        public List<int> IngredientsIds { get; set; }
    }
}
