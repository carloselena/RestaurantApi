using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Features.Dishes.Commands
{
    public static class DishValidation
    {
        public static async Task<bool> ValidateIngredients(List<int> ingredientsIds, IIngredientRepository ingredientRepository)
        {
            var ingredientsDB = await ingredientRepository.GetAllAsync();
            ingredientsDB = ingredientsDB.Where(i => ingredientsIds.Contains(i.Id)).ToList();

            if (ingredientsDB.Count != ingredientsIds.Count)
                return false;
                //throw new ApiException("Debe asegurarse de que los ingredientes existan", (int)HttpStatusCode.BadRequest);

            return true;
        }

        public static void SyncIngredients(Dish dish, List<int> newIngredientsIds)
        {
            var currentIds = dish.Ingredients.Select(di => di.IngredientId).ToList();

            var toAdd = newIngredientsIds.Except(currentIds);
            foreach (var id in toAdd)
            {
                dish.Ingredients.Add(new DishIngredients
                {
                    IngredientId = id,
                    DishId = dish.Id
                });
            }

            var toRemove = dish.Ingredients.Where(di => !newIngredientsIds.Contains(di.IngredientId)).ToList();
            foreach (var di in toRemove)
                dish.Ingredients.Remove(di);
        }
    }
}
