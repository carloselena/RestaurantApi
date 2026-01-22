using MediatR;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Tables.Commands.CreateTable
{
    public record CreateTableCommand(
        string Description,
        int MaxPeopleOnTable
    ) : IRequest<Response<int>>;
}
