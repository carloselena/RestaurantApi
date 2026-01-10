using FluentValidation;

namespace RestaurantApi.Core.Application.Features.Ingredients.Commands.CreateIngredient
{
    public class CreateIngredientCommandValidator
        : AbstractValidator<CreateIngredientCommand>
    {
        public CreateIngredientCommandValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("El nombre es obligatorio");
        }
    }
}
