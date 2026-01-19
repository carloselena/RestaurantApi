using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Tables.Queries.GetTableById
{
    public class GetTableByIdQuery : IRequest<Response<TableDto>>
    {
        public int Id { get; set; }
    }
}
