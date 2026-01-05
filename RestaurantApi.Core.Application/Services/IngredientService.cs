using AutoMapper;
using RestaurantApi.Core.Application.DTOs.Ingredient;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Services
{
    public class IngredientService : GenericService<IngredientDTO, SaveIngredientDTO, SaveIngredientDTO, Ingredient>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public IngredientService(IIngredientRepository ingredientRepository, IMapper mapper) : base(ingredientRepository, mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }
    }
}
