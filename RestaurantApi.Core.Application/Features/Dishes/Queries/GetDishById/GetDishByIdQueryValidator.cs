using FluentValidation;

namespace RestaurantApi.Core.Application.Features.Dishes.Queries.GetDishById
{
    public class GetDishByIdQueryValidator
        : AbstractValidator<GetDishByIdQuery>
    {
        public GetDishByIdQueryValidator()
        {
            RuleFor(d => d.Id)
                .GreaterThan(0)
                .WithMessage("El Id debe ser mayor a 0");
        }
    }
}
