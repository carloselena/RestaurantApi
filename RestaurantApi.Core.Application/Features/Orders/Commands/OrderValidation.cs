using RestaurantApi.Core.Application.Exceptions;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Domain.Entities;
using System.Net;

namespace RestaurantApi.Core.Application.Features.Orders.Commands
{
    public static class OrderValidation
    {
        public static async Task ValidateDishes(IReadOnlyCollection<int> dishesIds, IDishRepository dishRepository)
        {
            var dishesDB = await dishRepository.GetAllAsync();
            dishesDB = dishesDB.Where(d => dishesIds.Contains(d.Id)).ToList();

            if (dishesDB.Count != dishesIds.Count)
                throw new ValidationErrorException("Debe asegurarse de que los platos existan");
        }

        public static void SyncDishes(Order order, IReadOnlyCollection<int> newDishesIds)
        {
            var currentIds = order.Dishes.Select(d => d.DishId).ToList();

            var toAdd = newDishesIds.Except(currentIds);
            foreach (var id in toAdd)
            {
                order.Dishes.Add(new OrderDishes
                {
                    DishId = id,
                    OrderId = order.Id
                });
            }

            var toRemove = order.Dishes.Where(d => !newDishesIds.Contains(d.DishId)).ToList();
            foreach (var d in toRemove)
                order.Dishes.Remove(d);
        }
    }
}
