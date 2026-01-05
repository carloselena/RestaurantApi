using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Application.DTOs.Dish;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Services
{
    public class DishService : GenericService<DishDTO, AddDishDTO, UpdateDishDTO, Dish>, IDishService
    {
        private readonly IDishRepository _dishRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public DishService(IDishRepository dishRepository, IIngredientRepository ingredientRepository, IMapper mapper) : base(dishRepository, mapper)
        {
            _dishRepository = dishRepository;
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public override async Task<AddDishDTO> Add(AddDishDTO addDishDTO)
        {
            await ValidateIngredients(addDishDTO.IngredientsIds);

            return await base.Add(addDishDTO);
        }

        public override async Task<UpdateDishDTO> Update(int id, UpdateDishDTO updateDishDTO, Func<IQueryable<Dish>, IQueryable<Dish>>? includes = null)
        {
            await ValidateIngredients(updateDishDTO.IngredientsIds);

            var dish = await _dishRepository.GetByIdAsync(id, q => q.Include(d => d.Ingredients));

            if (dish == null)
                throw new KeyNotFoundException($"No hay plato con id {id}");

            dish.Price = updateDishDTO.Price;

            SyncIngredients(dish, updateDishDTO.IngredientsIds);

            await _dishRepository.UpdateAsync(dish);

            return _mapper.Map<UpdateDishDTO>(dish);
        }

        private async Task ValidateIngredients(List<int> ingredientsIds)
        {
            var ingredientsDB = await _ingredientRepository.GetAllAsync();
            ingredientsDB = ingredientsDB.Where(i => ingredientsIds.Contains(i.Id)).ToList();

            if (ingredientsDB.Count != ingredientsIds.Count)
                throw new Exception("Debe asegurarse de que los ingredientes existan");
        }

        private static void SyncIngredients(Dish dish, List<int> newIngredientsIds)
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
