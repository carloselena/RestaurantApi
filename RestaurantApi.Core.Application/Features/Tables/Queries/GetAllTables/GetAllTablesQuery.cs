using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Tables.Queries.GetAllTables
{
    public record GetAllTablesQuery : IRequest<Response<List<TableDto>>>;
}
