using AutoMapper;
using MediatR;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Features.Ingredients.Commands.CreateIngredient
{
    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, Response<SaveIngredientResponse>>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public CreateIngredientCommandHandler(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }
        public async Task<Response<SaveIngredientResponse>> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = _mapper.Map<Ingredient>(request);
            ingredient = await _ingredientRepository.AddAsync(ingredient);
            var response = _mapper.Map<SaveIngredientResponse>(ingredient);
            return Response<SaveIngredientResponse>.Success(response);
        }
    }
}
