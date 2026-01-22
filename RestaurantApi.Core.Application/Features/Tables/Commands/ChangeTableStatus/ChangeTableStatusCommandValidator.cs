using FluentValidation;

namespace RestaurantApi.Core.Application.Features.Tables.Commands.ChangeTableStatus
{
    public class ChangeTableStatusCommandValidator
        : AbstractValidator<ChangeTableStatusCommand>
    {
        public ChangeTableStatusCommandValidator()
        {
            RuleFor(t => t.Status)
                .IsInEnum().WithMessage("El estado solo puede ser DISPONIBLE, EN_PROCESO o ATENDIDA");
        }
    }
}
