using FluentValidation;

namespace RestaurantApi.Core.Application.Features.Orders.Queries.GetAllTableOrders
{
    public class GetAllTableOrdersQueryValidator
        : AbstractValidator<GetAllTableOrdersQuery>
    {
        public GetAllTableOrdersQueryValidator()
        {
            RuleFor(o => o.TableId)
                .GreaterThan(0)
                .WithMessage("El id de la mesa debe ser mayor a 0");
        }
    }
}
