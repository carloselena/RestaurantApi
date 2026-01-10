using FluentValidation;

namespace RestaurantApi.Core.Application.Features.Ingredients.Commands.UpdateIngredient
{
    public class UpdateIngredientCommandValidator
        : AbstractValidator<UpdateIngredientCommand>
    {
        public UpdateIngredientCommandValidator()
        {
            RuleFor(i => i.Id)
                .GreaterThan(0)
                .WithMessage("El Id debe ser mayor a cero");

            RuleFor(i => i.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("El nombre es obligatorio");
        }
    }
}
