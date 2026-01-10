using FluentValidation;

namespace RestaurantApi.Core.Application.Features.Dishes.Commands.UpdateDish
{
    public class UpdateDishCommandValidator
        : AbstractValidator<UpdateDishCommand>
    {
        public UpdateDishCommandValidator()
        {
            RuleFor(d => d.Id)
                .GreaterThan(0)
                .WithMessage("El Id debe ser mayor a 0");

            RuleFor(d => d.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

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