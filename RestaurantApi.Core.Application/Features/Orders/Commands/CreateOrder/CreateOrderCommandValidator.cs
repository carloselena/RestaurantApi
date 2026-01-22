using FluentValidation;

namespace RestaurantApi.Core.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator
        : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(o => o.TableId)
                .GreaterThan(0).WithMessage("El TableId debe ser mayor a cero");

            RuleFor(o => o.SubTotal)
                .GreaterThan(0).WithMessage("El SubTotal debe ser mayor a cero");

            RuleFor(o => o.DishesIds)
                .NotNull()
                .NotEmpty()
                .WithMessage("Debe enviar al menos un id de plato")
                .Must(list => list.Distinct().Count() == list.Count)
                .WithMessage("No se permiten ids de platos duplicados")
                .When(o => o.DishesIds != null && o.DishesIds.Any());

            RuleForEach(o => o.DishesIds)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Los ids de los platos deben ser mayor a 0");
        }
    }
}
