using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Application.Exceptions;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Dishes.Commands.UpdateDish
{
    public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand, Response<int>>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public UpdateDishCommandHandler(IDishRepository dishRepository, 
                                 IIngredientRepository ingredientRepository)
        {
            _dishRepository = dishRepository;
            _ingredientRepository = ingredientRepository;
        }
        public async Task<Response<int>> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            await DishValidation.ValidateIngredients(request.IngredientsIds, _ingredientRepository);
            
            var dish = await _dishRepository.GetByIdAsync(request.Id, q => q.Include(d => d.Ingredients));

            if (dish == null)
                throw new NotFoundException($"No se encontró el plato con id {request.Id}");

            dish.Price = request.Price;

            DishValidation.SyncIngredients(dish, request.IngredientsIds);

            await _dishRepository.UpdateAsync(dish);
            return new Response<int>(dish.Id);
        }
    }
}