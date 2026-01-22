using FluentValidation;

namespace RestaurantApi.Core.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryValidator
        : AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdQueryValidator()
        {
            RuleFor(o => o.Id)
                .GreaterThan(0)
                .WithMessage("El Id debe ser mayor a 0");
        }
    }
}
