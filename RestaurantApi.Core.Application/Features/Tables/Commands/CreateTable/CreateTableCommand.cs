using MediatR;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Tables.Commands.CreateTable
{
    public class CreateTableCommand : IRequest<Response<int>>
    {
        public string Description { get; set; }
        public int MaxPeopleOnTable { get; set; }

        public TableStatus Status = TableStatus.DISPONIBLE;
    }
}
