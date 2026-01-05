using FluentValidation;
using RestaurantApi.Core.Application.DTOs.Table;

namespace RestaurantApi.Core.Application.Validators.Table
{
    public class AddTableDTOValidator
        : AbstractValidator<AddTableDTO>
    {
        public AddTableDTOValidator()
        {
            RuleFor(t => t.Description)
                .NotEmpty().WithMessage("La descripción es obligatoria");

            RuleFor(t => t.MaxPeopleOnTable)
                .GreaterThan(0).WithMessage("La cantidad de personas que puede tener la mesa debe ser al menos 1");
        }
    }
}
