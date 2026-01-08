using AutoMapper;
using MediatR;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Ingredients.Queries.GetIngredientById
{
    public class GetIngredientByIdQueryHandler : IRequestHandler<GetIngredientByIdQuery, Response<IngredientDto>>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public GetIngredientByIdQueryHandler(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }
        public async Task<Response<IngredientDto>> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(request.Id);
            if (ingredient == null)
                return Response<IngredientDto>.Fail("No hay ingrediente con id {request.Id}");

            var response = _mapper.Map<IngredientDto>(ingredient);
            return Response<IngredientDto>.Success(response);
        }
    }
}
