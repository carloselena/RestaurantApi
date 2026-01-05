using RestaurantApi.Core.Application.DTOs.Ingredient;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Interfaces.Services
{
    public interface IIngredientService : IGenericService<IngredientDTO, SaveIngredientDTO, SaveIngredientDTO, Ingredient>
    {
    }
}
