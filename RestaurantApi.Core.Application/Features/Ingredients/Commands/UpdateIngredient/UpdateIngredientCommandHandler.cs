using AutoMapper;
using MediatR;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Ingredients.Commands.UpdateIngredient
{
    public class UpdateIngredientCommandHandler : IRequestHandler<UpdateIngredientCommand, Response<SaveIngredientResponse>>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public UpdateIngredientCommandHandler(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }
        public async Task<Response<SaveIngredientResponse>> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(request.Id);
            if (ingredient == null)
                return Response<SaveIngredientResponse>.Fail($"No hay ingrediente con id {request.Id}");

            _mapper.Map(request, ingredient);
            await _ingredientRepository.UpdateAsync(ingredient);
            var response = _mapper.Map<SaveIngredientResponse>(ingredient);
            return Response<SaveIngredientResponse>.Success(response);
        }
    }
}
