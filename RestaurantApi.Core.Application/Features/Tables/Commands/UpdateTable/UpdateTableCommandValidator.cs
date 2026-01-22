using FluentValidation;

namespace RestaurantApi.Core.Application.Features.Tables.Commands.UpdateTable
{
    public class UpdateTableCommandValidator
        : AbstractValidator<UpdateTableCommand>
    {
        public UpdateTableCommandValidator()
        {
            RuleFor(t => t.Description)
                .NotEmpty().WithMessage("La descripción no puede estar vacía");

            RuleFor(t => t.MaxPeopleOnTable)
                .GreaterThan(0).WithMessage("La cantidad de personas que puede tener la mesa debe ser al menos 1");
        }
    }
}
