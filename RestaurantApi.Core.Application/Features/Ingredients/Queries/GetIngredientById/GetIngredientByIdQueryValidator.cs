using FluentValidation;

namespace RestaurantApi.Core.Application.Features.Ingredients.Queries.GetIngredientById
{
    public class GetIngredientByIdQueryValidator
        : AbstractValidator<GetIngredientByIdQuery>
    {
        public GetIngredientByIdQueryValidator()
        {
            RuleFor(i => i.Id)
                .GreaterThan(0)
                .WithMessage("El Id debe ser mayor a cero");
        }
    }
}
