using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Tables.Queries.GetTableById
{
    public record GetTableByIdQuery(int Id) : IRequest<Response<TableDto>>;
}
