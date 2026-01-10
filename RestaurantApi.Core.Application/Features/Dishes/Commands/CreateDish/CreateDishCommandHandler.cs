using AutoMapper;
using MediatR;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Features.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, Response<int>>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public CreateDishCommandHandler(IDishRepository dishRepository, IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            var ingredientsCorrect = await DishValidation.ValidateIngredients(request.IngredientsIds, _ingredientRepository);
            if (!ingredientsCorrect)
                return Response<int>.Fail("Debe asegurarse de que todos los ingredientes existan");

            var dish = _mapper.Map<Dish>(request);
            dish = await _dishRepository.AddAsync(dish);
            return Response<int>.Success(dish.Id);
        }
    }
}
