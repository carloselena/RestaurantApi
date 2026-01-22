using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Core.Application.Features.Tables.Commands.CreateTable
{
    public class CreateTableCommandValidator
        : AbstractValidator<CreateTableCommand>
    {
        public CreateTableCommandValidator()
        {
            RuleFor(t => t.Description)
                .NotEmpty().WithMessage("La descripción es obligatoria");

            RuleFor(t => t.MaxPeopleOnTable)
                .GreaterThan(0).WithMessage("La cantidad de personas que puede tener la mesa debe ser al menos 1");
        }
    }
}
