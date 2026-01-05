using FluentValidation;
using RestaurantApi.Core.Application.DTOs.Order;

namespace RestaurantApi.Core.Application.Validators.Order
{
    public class UpdateOrderDTOValidator
        : AbstractValidator<UpdateOrderDTO>
    {
        public UpdateOrderDTOValidator()
        {
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
                .WithMessage("Los ids de los platos deben ser mayores a 0");
        }
    }
}
