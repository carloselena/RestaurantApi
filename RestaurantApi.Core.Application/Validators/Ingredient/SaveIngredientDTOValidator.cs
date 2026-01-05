using FluentValidation;
using RestaurantApi.Core.Application.DTOs.Ingredient;

namespace RestaurantApi.Core.Application.Validators.Ingredient
{
    public class SaveIngredientDTOValidator
        : AbstractValidator<SaveIngredientDTO>
    {
        public SaveIngredientDTOValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio")
                .MaximumLength(50)
                .WithMessage("El nombre no puede exceder los 50 caracteres");
        }
    }
}
