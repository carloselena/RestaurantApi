using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Application.Features.Ingredients.Queries;

namespace RestaurantApi.Core.Application.Features.Dishes.Queries
{
    public class DishDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int EnoughFor { get; set; }
        public DishCategories Category { get; set; }
        public List<IngredientDto>? Ingredients { get; set; }
    }
}
