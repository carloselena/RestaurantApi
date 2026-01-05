namespace RestaurantApi.Core.Application.DTOs.Dish
{
    public class UpdateDishDTO
    {
        public double Price { get; set; }
        public List<int> IngredientsIds { get; set; }
    }
}
