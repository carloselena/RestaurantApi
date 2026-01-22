using MediatR;
using RestaurantApi.Core.Application.Wrappers;
using System.Text.Json.Serialization;

namespace RestaurantApi.Core.Application.Features.Tables.Commands.UpdateTable
{
    public record UpdateTableCommand(
        [property: JsonIgnore] int Id,
        string Description,
        int MaxPeopleOnTable
    ) : IRequest<Response<int>>;
}
