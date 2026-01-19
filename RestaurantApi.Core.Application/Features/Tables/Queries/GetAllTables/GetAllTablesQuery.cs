using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Tables.Queries.GetAllTables
{
    public class GetAllTablesQuery : IRequest<Response<List<TableDto>>>
    {
    }
}
