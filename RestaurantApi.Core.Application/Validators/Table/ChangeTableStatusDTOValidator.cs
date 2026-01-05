using FluentValidation;
using RestaurantApi.Core.Application.DTOs.Table;

namespace RestaurantApi.Core.Application.Validators.Table
{
    public class ChangeTableStatusDTOValidator
        : AbstractValidator<ChangeTableStatusDTO>
    {
        public ChangeTableStatusDTOValidator()
        {
            RuleFor(t => t.Status)
                .IsInEnum().WithMessage("El estado solo puede ser DISPONIBLE, EN_PROCESO o ATENDIDA");
        }
    }
}
