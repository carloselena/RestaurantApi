using FluentValidation;

namespace RestaurantApi.Core.Application.Features.Ingredients.Commands.UpdateIngredient
{
    public class UpdateIngredientCommandValidator
        : AbstractValidator<UpdateIngredientCommand>
    {
        public UpdateIngredientCommandValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("El nombre es obligatorio");
        }
    }
}
