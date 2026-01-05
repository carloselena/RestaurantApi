using FluentValidation;
using RestaurantApi.Core.Application.DTOs.Table;

namespace RestaurantApi.Core.Application.Validators.Table
{
    public class UpdateTableDTOValidator
        : AbstractValidator<UpdateTableDTO>
    {
        public UpdateTableDTOValidator()
        {
            RuleFor(t => t.Description)
                .NotEmpty().WithMessage("La descripción no puede estar vacía");

            RuleFor(t => t.MaxPeopleOnTable)
                .GreaterThan(0).WithMessage("La cantidad de personas que puede tener la mesa debe ser al menos 1");
        }
    }
}
