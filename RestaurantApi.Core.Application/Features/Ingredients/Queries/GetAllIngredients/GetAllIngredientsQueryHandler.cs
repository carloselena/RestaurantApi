using AutoMapper;
using MediatR;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Ingredients.Queries.GetAllIngredients
{
    public class GetAllIngredientsQueryHandler : IRequestHandler<GetAllIngredientsQuery, Response<IList<IngredientDto>>>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public GetAllIngredientsQueryHandler(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public async Task<Response<IList<IngredientDto>>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)
        {
            var ingredients = await _ingredientRepository.GetAllAsync();
            if (ingredients == null || ingredients.Count == 0)
                return Response<IList<IngredientDto>>.Fail("No hay ingredientes");

            var response = _mapper.Map<List<IngredientDto>>(ingredients);
            return Response<IList<IngredientDto>>.Success(response);
        }
    }
}
