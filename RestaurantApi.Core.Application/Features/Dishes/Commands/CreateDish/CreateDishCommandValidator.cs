using FluentValidation;

namespace RestaurantApi.Core.Application.Features.Dishes.Commands.CreateDish
{
    public class CreateDishCommandValidator
        : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres");

            RuleFor(d => d.Price)
                    .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

            RuleFor(d => d.EnoughFor)
                    .GreaterThan(0).WithMessage("La cantidad para la que da el plato debe ser mayor a 0");

            RuleFor(d => d.Category)
                    .IsInEnum();

            RuleFor(d => d.IngredientsIds)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Debe enviar al menos un id de ingrediente")
                    .Must(list => list.Distinct().Count() == list.Count)
                    .WithMessage("No se permiten ids de ingredientes duplicados")
                    .When(d => d.IngredientsIds != null && d.IngredientsIds.Any());

            RuleForEach(o => o.IngredientsIds)
                    .NotNull()
                    .NotEmpty()
                    .GreaterThan(0)
                    .WithMessage("Los ids de los ingrediente deben ser mayor a 0");
        }
    }
}
