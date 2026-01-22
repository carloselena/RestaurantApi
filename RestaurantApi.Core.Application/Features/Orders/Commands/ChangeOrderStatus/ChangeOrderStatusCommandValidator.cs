using FluentValidation;

namespace RestaurantApi.Core.Application.Features.Orders.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusCommandValidator
        : AbstractValidator<ChangeOrderStatusCommand>
    {
        public ChangeOrderStatusCommandValidator()
        {
            RuleFor(o => o.Id)
                .GreaterThan(0)
                .WithMessage("El Id debe ser mayor a 0");
        }
    }
}
