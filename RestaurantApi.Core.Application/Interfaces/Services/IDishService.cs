using RestaurantApi.Core.Application.DTOs.Dish;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Interfaces.Services
{
    public interface IDishService : IGenericService<DishDTO, AddDishDTO, UpdateDishDTO, Dish>
    {
    }
}
