using FluentValidation;

namespace RestaurantApi.Core.Application.Features.Tables.Queries.GetTableById
{
    public class GetTableByIdQueryValidator
        : AbstractValidator<GetTableByIdQuery>
    {
        public GetTableByIdQueryValidator()
        {
            RuleFor(t => t.Id)
                .GreaterThan(0)
                .WithMessage("El Id debe ser mayor a 0");
        }
    }
}
